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
    public class OrderController: Controller
    {
        
        IOrderRepository re;

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
            re = new OrderRepository();
            var model = re.GetOrdersList(GetConnectionString());
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblOrder t)
        {
            re = new OrderRepository();
            try
            {
                if (ModelState.IsValid)
                {
                    re.AddNew(GetConnectionString(), t);
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
            re = new OrderRepository();
            if (id == null)
            {
                return NotFound();
            }
            var order = re.GetOrdersById(GetConnectionString(), id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int OrderId, TblOrder order)
        {
            re = new OrderRepository();
            try
            {
                if (OrderId != order.OrderId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    re.Update(GetConnectionString(), order);
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
            re = new OrderRepository();
            if (id == null)
            {
                return NotFound();
            }
            var order = re.GetOrdersById(GetConnectionString(), id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int OrderId)
        {
            re = new OrderRepository();
            try
            {
                re.Remove(GetConnectionString(), OrderId);
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
