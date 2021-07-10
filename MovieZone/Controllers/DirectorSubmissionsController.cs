using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieZone.Persistance;
using MovieZone.Core.Domain;
using MovieZone.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using static MovieZone.ViewModels.MoviesListViewModel;

namespace MovieZone.Controllers
{
    public class DirectorSubmissionsController : Controller
    {
        private ApplicationDbContext _context;
        public DirectorSubmissionsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult New()
        {
            return View("DirectorForm", new DirectorSubmission());
        }

        public ActionResult Edit(int id)
        {
            var director = _context.Directors.SingleOrDefault(m => m.Id == id);
            if (director == null)
                return HttpNotFound();
            return View("DirectorForm", new DirectorSubmission(director));
        }

        [HttpPost]
        public ActionResult Submit(DirectorSubmission directorSubmission)
        {
            if (!ModelState.IsValid)
                return View("DirectorForm", directorSubmission);
            _context.DirectorSubmissions.Add(directorSubmission);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Accept(int submissionId)
        {
            var submission = _context.DirectorSubmissions.SingleOrDefault(ds => ds.SubmissionId == submissionId);
            if (submission == null)
                return HttpNotFound();

            var director = (submission.DirectorId == 0) ? new Director() : _context.Directors.Single(d => d.Id == submission.DirectorId);
            director.Id = submission.DirectorId;
            director.Name = submission.Name;
            director.Bio = submission.Bio;
            
            if (submission.DirectorId == 0)
                _context.Directors.Add(director);
            _context.DirectorSubmissions.Remove(submission);
            _context.SaveChanges();
            return RedirectToAction("Details", "Directors", new { id = director.Id });
        }

        public ActionResult Reject(int submissionId)
        {
            var submission = _context.DirectorSubmissions.SingleOrDefault(ms => ms.SubmissionId == submissionId);
            if (submission == null)
                return HttpNotFound();
            _context.DirectorSubmissions.Remove(submission);
            _context.SaveChanges();
            return RedirectToAction("Submissions", "User");
        }
    }
}