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
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(bool ascending=false, int genreFilter = 0, SortBy sort = SortBy.Popularity )
        {
            var movies = _context.Movies.Include(m => m.Director).Include(m => m.Genre).ToList();

            switch (sort)
            {
                case SortBy.Popularity:
                    movies = movies.OrderByDescending(m => m.NumberOfRatings).ToList();
                    break;
                case SortBy.Rating:
                    movies = movies.OrderByDescending(m => m.RatingAvg).ToList();
                    break;
                case SortBy.Title:
                    movies = movies.OrderByDescending(m => m.Title).ToList();
                    break;
                case SortBy.Year:
                    movies = movies.OrderByDescending(m => m.ReleaseYear).ToList();
                    break;
            }

            if (ascending)
                movies.Reverse();

            if (genreFilter != 0)
                movies = movies.Where(m => m.GenreId == genreFilter).ToList();

            var genres = new List<Genre> { new Genre { Id=0, Name="All"} };
            genres.AddRange(_context.Genres.ToList());


            var model = new MoviesListViewModel()
            {
                Movies = movies,
                Genres = genres,
                GenreFilter = genreFilter,
                Sort = sort,
                Ascending = ascending
            };
            return View(model);
        }

        public ActionResult Details(int id, bool showAllReviews=false)
        {
            var movie = _context.Movies.Include(m => m.Genre).Include(m => m.Director).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            //movie.Genre = _context.Genres.Single(g => g.Id == movie.GenreId);
            //movie.Director = _context.Directors.Single(d => d.Id == movie.DirectorId);
            var userId = User.Identity.GetUserId();
            var ratingsWithReview = _context.Ratings.Where(r => r.MovieId == id && r.ReviewId != null).Include(r => r.Review).Include(r => r.ApplicationUser).OrderByDescending(m => m.Review.DateAdded);
            var userRating = _context.Ratings.SingleOrDefault(r => r.MovieId == id && r.ApplicationUserId == userId);


            var model = new MovieDetailsViewModel
            {
                Movie = movie,
                RatingsWithReview = (showAllReviews) ? ratingsWithReview.ToList() : new List<Rating> { ratingsWithReview.FirstOrDefault() },
                UserRatingValue = (userRating == null) ? 0 : userRating.Value
            };
            return View(model);
        }

        [Authorize]
        public ActionResult NewReview(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var rating = _context.Ratings.Include(r => r.Review).SingleOrDefault(r => r.MovieId == movie.Id && r.ApplicationUserId == userId);

            if(rating == null)              
                return View("ReviewForm", new Rating { Review = new Review(), MovieId = movie.Id, ApplicationUserId = userId });
                    
            return View("ReviewForm", rating);           
        }

        [HttpPost]
        public ActionResult SaveReview(Rating rating)
        {
            if (!ModelState.IsValid)
                return View("ReviewForm", rating);

            rating.Review.DateAdded = DateTime.Now;
            var ratingInDb = _context.Ratings.SingleOrDefault(r => r.MovieId == rating.MovieId && r.ApplicationUserId == rating.ApplicationUserId);
            UpdateRatingAvg(ratingInDb, rating.MovieId, rating.Value);

            if (ratingInDb==null)
            {
                _context.Reviews.Add(rating.Review);
                rating.ReviewId = rating.Review.Id;
                _context.Ratings.Add(rating);       
            }
            else if(ratingInDb.ReviewId==null)
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

        public void UpdateRatingAvg(Rating ratingInDb, int movieId, int newValue)
        {
            var movie = _context.Movies.Single(m => m.Id == movieId);
            var x = "";
            if(ratingInDb==null)
            {
                movie.SumOfRatings += newValue;
                movie.NumberOfRatings++;
            }
            else            
                movie.SumOfRatings += (-ratingInDb.Value) + newValue;
            var y = "";
            _context.SaveChanges();
        }

        [Authorize]
        public ActionResult Rate(int id, int UserRatingValue)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var rating = _context.Ratings.SingleOrDefault(r => r.MovieId == id && r.ApplicationUserId == userId);
            UpdateRatingAvg(rating, id, UserRatingValue);

            if (rating == null)
            {
                rating = new Rating { MovieId = id, ApplicationUserId = userId, Value = UserRatingValue };
                _context.Ratings.Add(rating);
            }
            else         
                rating.Value = UserRatingValue;
            var s = "";
            _context.SaveChanges();
            return RedirectToAction("Details","Movies", new { id });
        }



    }
}