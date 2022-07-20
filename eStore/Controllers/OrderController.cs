using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderRepository;
        // GET: OrderController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            orderRepository = new OrderRepository();
            var model = orderRepository.GetOrdersList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Statistic(DateTime? start, DateTime? end)
        {
            orderRepository = new OrderRepository();
            List<TblOrder> orders = orderRepository.MakeReportStatistic(start.Value, end.Value).ToList();
            var order = from c in orders
                        group c by c.OrderDate into grp
                        where grp.Count() > 0
                        select new { OrderDate = grp.Key, Total = grp.Count() };
            var statistic = from o in order
                            orderby o.Total descending
                            select o;
            List<String> orderDates = new List<string>();
            List<int> totals = new List<int>();
            foreach (var s in statistic)
            {
                orderDates.Add(s.OrderDate.ToShortDateString());
                totals.Add(s.Total);
            }
            ViewBag.orderDates = orderDates;
            ViewBag.totals = totals;
            return View();
        }

        // GET: OrderController/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            orderRepository = new OrderRepository();
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: OrderController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            IMemberRepository memberRepository = new MemberRepository();
            ViewBag.MemberId = memberRepository.GetMembersList().Select(x => new SelectListItem()
            {
                Text = x.Email,
                Value = x.MemberId.ToString()
            });
            return View();
        }

        // POST: OrderController/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblOrder order)
        {
            IMemberRepository memberRepository = new MemberRepository();
            orderRepository = new OrderRepository();
            try
            {
                if (ModelState.IsValid)
                {
                    orderRepository.AddNew(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.MemberId = memberRepository.GetMembersList().Select(x => new SelectListItem()
                {
                    Text = x.Email,
                    Value = x.MemberId.ToString()
                });
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: OrderController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            orderRepository = new OrderRepository();
            IMemberRepository memberRepository = new MemberRepository();
            ViewBag.MemberId = memberRepository.GetMembersList().Select(x => new SelectListItem()
            {
                Text = x.Email,
                Value = x.MemberId.ToString()
            });
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int OrderId, TblOrder order)
        {
            IMemberRepository memberRepository = new MemberRepository();
            orderRepository = new OrderRepository();
            try
            {
                if (OrderId != order.OrderId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    orderRepository.Update(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.MemberId = memberRepository.GetMembersList().Select(x => new SelectListItem()
                {
                    Text = x.Email,
                    Value = x.MemberId.ToString()
                });
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: OrderController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            orderRepository = new OrderRepository();
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int OrderId)
        {
            orderRepository = new OrderRepository();
            try
            {
                orderRepository.Remove(OrderId);
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
