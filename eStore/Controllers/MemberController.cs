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
    public class MemberController : Controller
    {
        IMemberRepository memberRepository;
        // GET: MemberController
        public ActionResult Index()
        {
            memberRepository = new MemberRepository();
            string role = HttpContext.Session.GetString("Role");
            if (role.Equals("AD"))
            {
                var model = memberRepository.GetMembersList();
                return View(model);
            }
            else
            {
                TblMember member = HttpContext.Session.GetComplexData<TblMember>("account");
                var model = memberRepository.GetMembersListByUser(member.MemberId);
                return View(model);
            }

        }

        // GET: MemberController/Details/5
        public ActionResult Details(int? id)
        {
            memberRepository = new MemberRepository();
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMembersById(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [Authorize(Roles = "Admin")]
        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblMember member)
        {
            memberRepository = new MemberRepository();
            try
            {
                if (ModelState.IsValid)
                {
                    memberRepository.AddNew(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int? id)
        {
            memberRepository = new MemberRepository();
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMembersById(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Edit/5
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
                    memberRepository.Update(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MemberController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            memberRepository = new MemberRepository();
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMembersById(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int MemberId)
        {
            memberRepository = new MemberRepository();
            try
            {
                memberRepository.Remove(MemberId);
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

