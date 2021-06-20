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

        public ActionResult Submissions(int pageNum=1)
        {
            var movieSubmissions = _context.MovieSubmissions.Include(s => s.Director).Include(s => s.Genres).ToList();
            var directorSubmissions = _context.DirectorSubmissions.ToList();
            var AlldirSubCount = directorSubmissions.Count;
            var count = movieSubmissions.Count + AlldirSubCount;
            var pageCount = (count % defPageSize == 0) ? count / defPageSize : count / defPageSize + 1;
            
            directorSubmissions = directorSubmissions.Skip((pageNum - 1) * defPageSize).Take(defPageSize).ToList();
            movieSubmissions = movieSubmissions.Skip((pageNum - 1) * defPageSize - AlldirSubCount).Take(defPageSize - directorSubmissions.Count).ToList();

            //var pageCount = (movieSub.Count % defPageSize == 0) ? movieSub.Count / defPageSize : movieSub.Count / defPageSize + 1;
            //movieSub = movieSub.Skip((pageNum-1)* defPageSize).Take(defPageSize).ToList();

            var model = new SubmissionsViewModel
            {
                DirectorSubmissions = directorSubmissions,
                MovieSubmissions = movieSubmissions,
                PageCount = pageCount
            }; 
            return View(model);
        }
    }
}