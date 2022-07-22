using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
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

        // GET: ProductController
        public ActionResult Index()
        {
            productRepository = new ProductRepository();
            var model = productRepository.GetProductsList();
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
            var product = productRepository.GetProductsById(id.Value);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public ActionResult Search(string? op, string? search, int? from, int? to)
        {
            productRepository = new ProductRepository();
            if (search == null)
            {
                return NotFound();
            }
            else
            {
                if (op.Equals("Search"))
                {
                    if(from != null && to != null)
                    {
                        var product = productRepository.SearchProductsByUnitPrice(search, from.Value, to.Value);
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
                        var product = productRepository.SearchProductsByProductName(search);
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
                }
                else
                {
                    var product = productRepository.GetProductsList();
                    ViewBag.Search = "";
                    ViewBag.From = "";
                    ViewBag.To = "";
                    return View("Index", product);
                }
            }
        }

        [Authorize(Roles = "Admin")]
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
                    productRepository.AddNew(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            productRepository = new ProductRepository();
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductsById(id.Value);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
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
                    productRepository.Update(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }


        [Authorize(Roles = "Admin")]
        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            productRepository = new ProductRepository();
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductsById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ProductId)
        {
            productRepository = new ProductRepository();
            try
            {
                productRepository.Remove(ProductId);
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
