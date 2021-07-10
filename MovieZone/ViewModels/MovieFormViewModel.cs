using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieZone.Core.Domain;
using MovieZone.Persistance;
using System.ComponentModel.DataAnnotations;

namespace MovieZone.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormViewModel(Movie movie)
        {
            MovieSubmission = new MovieSubmission
            {
                MovieId = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                DirectorId = movie.DirectorId,
                Director = movie.Director,
                DurationInMinutes = movie.DurationInMinutes,
                Description = movie.Description,
                Genres = movie.Genres
            };
            SetGenresAnddirectors();
            var n = _genreMaxCount - MovieSubmission.Genres.Count;
            for (var i=0; i<n; i++)
                MovieSubmission.Genres.Add(Genres[0]);           
        }

        public MovieFormViewModel(MovieSubmission movieSubmission)
        {
            MovieSubmission = movieSubmission;
            SetGenresAnddirectors();
            var n = _genreMaxCount - MovieSubmission.Genres.Count;
            for (var i = 0; i < n; i++)
                MovieSubmission.Genres.Add(Genres[0]);

        }

        

        //public MovieFormViewModel()
        //{
        //    MovieSubmission = new MovieSubmission { Genres = new List<Genre>()};           
        //    SetGenresAnddirectors();
        //    for (var i = 0; i < _genreMaxCount; i++)
        //        MovieSubmission.Genres.Add(Genres[0]);
        //}

        public MovieFormViewModel()
        {
            MovieSubmission = new MovieSubmission { Genres = new List<Genre>()};          
            SetGenresAnddirectors();
            for (var i = 0; i < _genreMaxCount; i++)
                MovieSubmission.Genres.Add(Genres[0]);
        }

        public const int _genreMaxCount = 5;

        public MovieSubmission MovieSubmission { get; set; }
        public List<Genre> Genres { get; set; }
        public IEnumerable<Director> Directors { get; set; }

        public void SetGenresAnddirectors()
        {
            var _context = new ApplicationDbContext();
            Genres = new List<Genre> { new Genre { Id = 0, Name = "None" } };
            Genres.AddRange( _context.Genres.ToList());
            Directors = _context.Directors.ToList();
        }

        public void RemoveSubmittedGenresThatAreRepeatsOrNull()
        {
            var submitted = MovieSubmission.Genres;
            submitted.RemoveAll(g => g == null);
            MovieSubmission.Genres = submitted.Distinct().ToList();
        }
    }
}