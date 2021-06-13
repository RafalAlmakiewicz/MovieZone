using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieZone.Models;
using MovieZone.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using static MovieZone.ViewModels.MoviesListViewModel;

namespace MovieZone.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        private const int defPageSize = 10;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submissions(int submissionId, int pageNum=1)
        {
            var submissions = _context.MovieSubmissions.Include(s => s.Director).Include(s => s.Genres).ToList();

            var pageCount = (submissions.Count % defPageSize == 0) ? submissions.Count / defPageSize : submissions.Count / defPageSize + 1;

            submissions = submissions.Skip((pageNum-1)* defPageSize).Take(defPageSize).ToList();

            var model = new SubmissionsViewModel
            {
                MovieSubmissions = submissions,
                PageCount = pageCount
            };
            
            return View(model);
        }
    }
}