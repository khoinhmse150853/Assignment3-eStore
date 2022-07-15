using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository productRepository;

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
            productRepository = new ProductRepository();
            var model = productRepository.GetProductsList(GetConnectionString());
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
        {
            productRepository = new ProductRepository();
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductsById(GetConnectionString(), id.Value);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public ActionResult Search(string? op, string? search, int? from, int? to)
        {
            productRepository = new ProductRepository();
            if (search == null || from == null || to == null)
            {
                return NotFound();
            }
            else
            {
                if (op.Equals("Search"))
                {
                    var product = productRepository.SearchProducts(GetConnectionString(), search, from.Value, to.Value);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.Search = search;
                        ViewBag.From = from;
                        ViewBag.To = to;
                        return View("Index", product);
                    }
                }
                else
                {
                    var product = productRepository.GetProductsList(GetConnectionString());
                    ViewBag.Search = "";
                    ViewBag.From = "";
                    ViewBag.To = "";
                    return View("Index", product);
                }
            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblProduct product)
        {
            productRepository = new ProductRepository();
            try
            {
                if (ModelState.IsValid)
                {
                    productRepository.AddNew(GetConnectionString(), product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            productRepository = new ProductRepository();
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductsById(GetConnectionString(), id.Value);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ProductId, TblProduct product)
        {
            productRepository = new ProductRepository();
            try
            {
                if(ProductId != product.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    productRepository.Update(GetConnectionString(), product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            productRepository = new ProductRepository();
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductsById(GetConnectionString(), id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ProductId)
        {
            productRepository = new ProductRepository();
            try
            {
                productRepository.Remove(GetConnectionString(), ProductId);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
