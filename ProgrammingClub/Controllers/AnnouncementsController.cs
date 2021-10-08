using ProgrammingClub.Models;
using ProgrammingClub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingClub.Controllers
{
    public class AnnouncementsController : Controller
    {
        private AnnouncementRepository announcementRepository = new AnnouncementRepository();
        // GET: Announcements
        public ActionResult Index()
        {
            List<AnnouncementModel> announcements = announcementRepository.GetAllAnnouncements();

            return View("Index", announcements);
        }

        // GET: Announcements/Details/5
        public ActionResult Details(Guid id)
        {
            AnnouncementModel announcementModel = announcementRepository.GetAnnouncementByID(id);
            return View("Details", announcementModel);
        }

        // GET: Announcements/Create
        public ActionResult Create()
        {
            return View("CreateAnnouncement");
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Announcements/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                AnnouncementModel announcementModel = new AnnouncementModel();
                UpdateModel(announcementModel); //preiau datele din from collection si le punem in announcementModel
                announcementRepository.InsertAnnouncement(announcementModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateAnnouncement");
            }
        }

        // GET: Announcements/Edit/5
        public ActionResult Edit(Guid id)
        {
            AnnouncementModel announcementModel = announcementRepository.GetAnnouncementByID(id);
            return View("EditAnnouncement", announcementModel);
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Announcements/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                AnnouncementModel announcementModel = new AnnouncementModel();
                UpdateModel(announcementModel);
                announcementRepository.UpdateAnnouncement(announcementModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditAnnouncement");
            }
        }

        // GET: Announcements/Delete/5
        public ActionResult Delete(Guid id)
        {
            AnnouncementModel announcementModel = announcementRepository.GetAnnouncementByID(id);
            return View("Delete", announcementModel);

        }

        [Authorize(Roles = "Admin")]
        // POST: Announcements/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                announcementRepository.DeleteAnnouncement(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
