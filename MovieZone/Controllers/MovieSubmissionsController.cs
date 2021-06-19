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
    public class MovieSubmissionsController : Controller
    {
        private ApplicationDbContext _context;
        public MovieSubmissionsController()
        {
            _context = new ApplicationDbContext();
        }



        public ActionResult New()
        {
            return View("MovieForm", new MovieFormViewModel());
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genres).Include(m => m.Director).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View("MovieForm", new MovieFormViewModel(movie));
        }

        [HttpPost]
        public ActionResult Submit(MovieFormViewModel movieFormViewModel)//[Bind(Include = "description")]
        {
            if (!ModelState.IsValid)
                return View("MovieForm", movieFormViewModel);

            var id = 0;
            for (var i = 0; i < movieFormViewModel.MovieSubmission.Genres.Count; i++)
            {
                id = movieFormViewModel.MovieSubmission.Genres[i].Id;
                movieFormViewModel.MovieSubmission.Genres[i] = _context.Genres.SingleOrDefault(g => g.Id == id);
            }
            movieFormViewModel.RemoveSubmittedGenresThatAreRepeatsOrNull();

            _context.MovieSubmissions.Add(movieFormViewModel.MovieSubmission);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Accept(int submissionId)
        {
            var submission = _context.MovieSubmissions.Include(ms => ms.Genres).SingleOrDefault(ms => ms.SubmissionId == submissionId);

            if (submission == null)
                return HttpNotFound();

            var movie = (submission.MovieId == 0) ? new Movie() : _context.Movies.Single(m => m.Id == submission.MovieId);

            movie.Title = submission.Title;
            movie.ReleaseYear = submission.ReleaseYear;
            movie.DirectorId = submission.DirectorId;
            movie.DurationInMinutes = submission.DurationInMinutes;
            movie.Description = submission.Description;            
            movie.Genres = new List<Genre>();
            var genreId = 0;
            for (var i = 0; i < submission.Genres.Count; i++)
            {
                genreId = submission.Genres[i].Id;
                movie.Genres.Add(_context.Genres.Single(g => g.Id == genreId));             
            }

            if (submission.MovieId == 0)
                _context.Movies.Add(movie);

            _context.MovieSubmissions.Remove(submission);
            _context.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = movie.Id});
        }

        public ActionResult Reject(int submissionId)
        {
            var submission = _context.MovieSubmissions.SingleOrDefault(ms => ms.SubmissionId == submissionId);
            if (submission == null)
                return HttpNotFound();
            _context.MovieSubmissions.Remove(submission);
            _context.SaveChanges();
            return RedirectToAction("Submissions", "User");
        }
    }
}