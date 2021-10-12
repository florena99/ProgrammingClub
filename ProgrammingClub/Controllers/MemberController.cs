using ProgrammingClub.Models;
using ProgrammingClub.Models.ViewModels;
using ProgrammingClub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingClub.Controllers
{
    public class MemberController : Controller
    {
        private MemberRepository memberRepository=new MemberRepository();

        // GET: Member
        public ActionResult Index()
        {
            List<MemberModel> members = memberRepository.GetAllMembers();
            return View("Index", members);
        }

        // GET: Member/Details/5
        public ActionResult Details(Guid id)
        {
            
            MemberModel memberModel = memberRepository.GetMemberById(id);
            return View("Details", memberModel);
        }

        public ActionResult DetailsViewModel(Guid id)
        {
            MemberCodeSnippetsViewModel viewModel = memberRepository.GetMemberCodeSnippets(id);
            return View("DetailsViewModel", viewModel);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View("CreateMember");
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                MemberModel memberModel = new MemberModel();
                UpdateModel(memberModel);
                memberRepository.InsertMember(memberModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMember");
            }
        }


        // GET: Member/Edit/5
        public ActionResult Edit(Guid id)
        {
            MemberModel memberModel = memberRepository.GetMemberById(id);
            return View("EditMember", memberModel);
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Member/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                MemberModel memberModel = new MemberModel();
                UpdateModel(memberModel);
                memberRepository.UpdateMember(memberModel);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditMember");
            }
        }

        // GET: Member/Delete/5
        public ActionResult Delete(Guid id)
        {
            MemberModel memberModel = memberRepository.GetMemberById(id);
            return View("Delete", memberModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Member/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                memberRepository.DeleteMember(id);
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
