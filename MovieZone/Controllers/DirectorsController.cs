using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieZone.ViewModels;
using System.Data.Entity;
using MovieZone.Models;
using Microsoft.AspNet.Identity;

namespace MovieZone.Controllers
{
    public class DirectorsController : Controller
    {
        private ApplicationDbContext _context;
        public DirectorsController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var director = _context.Directors.SingleOrDefault(d => d.Id == id);
            if (director == null)
                return HttpNotFound();

            var model = new DirectorDetailsViewModel
            {
                Director = director,
                Movies = _context.Movies.Include(m => m.Genres).Where(m => m.DirectorId == id).OrderByDescending(m => m.ReleaseYear).ThenBy(m => m.Title)
            };
            return View(model);
        }

        public ActionResult test()
        {
            //string s = "";
            //var id = User.Identity.GetUserId();
            //if (id == null)
            //    s = "null";
            //else if (id == "")
            //    s = "empty";
            //else
            //    s = "cos innego";

            var l = new List<Rating> { null, null };

            return Content($"{l.Count}, {l[0]}, {l[1]}");
        }
    }
}