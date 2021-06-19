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
    public class ReviewsController : Controller
    {
        private ApplicationDbContext _context;
        public ReviewsController()
        {
            _context = new ApplicationDbContext();
        }


        [Authorize]
        public ActionResult New(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var rating = _context.Ratings.Include(r => r.Review).SingleOrDefault(r => r.MovieId == movie.Id && r.ApplicationUserId == userId);

            if (rating == null)
                return View("ReviewForm", new Rating { Review = new Review(), MovieId = movie.Id, ApplicationUserId = userId });

            return View("ReviewForm", rating);
        }

        [HttpPost]
        public ActionResult Save(Rating rating)
        {
            if (!ModelState.IsValid)
                return View("ReviewForm", rating);

            rating.Review.DateAdded = DateTime.Now;
            var ratingInDb = _context.Ratings.SingleOrDefault(r => r.MovieId == rating.MovieId && r.ApplicationUserId == rating.ApplicationUserId);
            MoviesController.UpdateRatingAvg(ratingInDb, rating.MovieId, rating.Value);

            if (ratingInDb == null)
            {
                _context.Reviews.Add(rating.Review);
                rating.ReviewId = rating.Review.Id;
                _context.Ratings.Add(rating);
            }
            else if (ratingInDb.ReviewId == null)
            {
                ratingInDb.Value = rating.Value;
                _context.Reviews.Add(rating.Review);
                ratingInDb.Review = rating.Review;
                ratingInDb.ReviewId = rating.Review.Id;
            }
            else
            {
                ratingInDb.Value = rating.Value;
                var reviewInDb = _context.Reviews.Single(r => r.Id == ratingInDb.ReviewId);
                reviewInDb.Body = rating.Review.Body;
                reviewInDb.DateAdded = rating.Review.DateAdded;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}