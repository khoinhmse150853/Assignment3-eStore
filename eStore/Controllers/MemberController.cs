using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        IMemberRepository memberRepository;

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();
            var strConn = config["ConnectionStrings:SalesManagementDB"];
            return strConn;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            memberRepository = new MemberRepository();
            var model = memberRepository.GetMembersList(GetConnectionString());
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
        {
            memberRepository = new MemberRepository();
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMembersById(GetConnectionString(), id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblMember member)
        {
            memberRepository = new MemberRepository();
            try
            {
                if (ModelState.IsValid)
                {
                    memberRepository.AddNew(GetConnectionString(), member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            memberRepository = new MemberRepository();
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMembersById(GetConnectionString(), id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int MemberId, TblMember member)
        {
            memberRepository = new MemberRepository();
            try
            {
                if (MemberId != member.MemberId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    memberRepository.Update(GetConnectionString(), member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            memberRepository = new MemberRepository();
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMembersById(GetConnectionString(), id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int MemberId)
        {
            memberRepository = new MemberRepository();
            try
            {
                memberRepository.Remove(GetConnectionString(), MemberId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}

