using BusinessObject;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class LoginController : Controller
    {
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();
            var strConn = config["ConnectionStrings:SalesManagementDB"];
            return strConn;
        }

        // GET: LoginController
        public ActionResult Index(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password, string returnUrl)
        {
            String Email, Password;
            string cs = GetConnectionString();
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                Email = config["account:defaultAccount:email"];
                Password = config["account:defaultAccount:password"];
            }
            if (email.Equals(Email) && password.Equals(Password))
            {
                var db = new SaleManagementContext(cs);
                TblMember user = new TblMember(email, password);
                HttpContext.Session.SetString("account", JsonConvert.SerializeObject(user));
                HttpContext.Session.SetString("Role", "AD");
                var claims = new List<Claim>();
                claims.Add(new Claim("username", user.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Email));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                using (var db = new SaleManagementContext(cs))
                {
                    var user = db.TblMembers.SingleOrDefault(ac => ac.Email.Equals(email) && ac.MemberPassword.Equals(password));
                    if (user == null)
                    {
                        ViewBag.Message = "Login failed";
                        @ViewBag.Email = email;
                        return View();
                    }
                    else
                    {
                        HttpContext.Session.SetString("account", JsonConvert.SerializeObject(user));
                        HttpContext.Session.SetString("Role", "US");
                        var claims = new List<Claim>();
                        claims.Add(new Claim("username", user.Email));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Email));
                        claims.Add(new Claim(ClaimTypes.Role, "User"));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
