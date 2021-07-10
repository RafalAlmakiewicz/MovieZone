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
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("movies/browse/{pageNum:int:min(1)=1}")]//:int:min(1)=1
        public ActionResult Index(int pageNum, int pageSize = 10, bool ascending=false, int genreFilter = 0, SortBy sort = SortBy.Popularity )
        {
            var movies = _context.Movies.Include(m => m.Director).Include(m => m.Genres).ToList();

            var genres = new List<Genre> { new Genre { Id = 0, Name = "All" } };
            genres.AddRange(_context.Genres.ToList());

            if (genreFilter != 0)
                  movies = movies.Where(m => m.Genres.Contains(genres.Single(g => g.Id == genreFilter))).ToList();

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

            var pageCount = (movies.Count % pageSize == 0) ? movies.Count / pageSize : movies.Count / pageSize + 1;

            movies = movies.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            

            var model = new MoviesListViewModel()
            {
                Movies = movies,
                Genres = genres,
                GenreFilter = genreFilter,
                Sort = sort,
                Ascending = ascending,
                PageCount = pageCount,
                PageSize = pageSize
            };
            return View(model);
        }

        public ActionResult Details(int id, bool showAllReviews=false)
        {
            var movie = _context.Movies.Include(m => m.Genres).Include(m => m.Director).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            
            var userId = User.Identity.GetUserId();
            var ratingsWithReview = _context.Ratings.Where(r => r.MovieId == id && r.ReviewId != null).Include(r => r.Review).Include(r => r.ApplicationUser).OrderByDescending(m => m.Review.DateAdded);
            var userRating = _context.Ratings.SingleOrDefault(r => r.MovieId == id && r.ApplicationUserId == userId);

            var model = new MovieDetailsViewModel
            {
                Movie = movie,
                RatingsWithReview = (showAllReviews) ? ratingsWithReview.ToList() : (ratingsWithReview.FirstOrDefault() != null) ? new List<Rating> { ratingsWithReview.FirstOrDefault() } : new List<Rating>(),
                UserRatingValue = (userRating == null) ? 0 : userRating.Value
            };
            return View(model);
        }

        

        public static void UpdateRatingAvg(Rating ratingInDb, int movieId, int newValue)
        {
            var _context = new ApplicationDbContext();
            var movie = _context.Movies.Single(m => m.Id == movieId);
            
            if(ratingInDb==null)
            {
                movie.SumOfRatings += newValue;
                movie.NumberOfRatings++;
            }
            else            
                movie.SumOfRatings += (-ratingInDb.Value) + newValue;
            
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
            
            _context.SaveChanges();
            return RedirectToAction("Details","Movies", new { id });
        }

        
    }
}