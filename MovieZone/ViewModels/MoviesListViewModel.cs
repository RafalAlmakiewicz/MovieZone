using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieZone.Core.Domain;

namespace MovieZone.ViewModels
{
    public class MoviesListViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

       
        public int GenreFilter { get; set; } = 0;
        //public string SortBy { get; set; } = "Rating";

        public enum SortBy{Popularity, Rating, Title, Year }
        public SortBy Sort { get; set; }

        //public string[] SortBy = { "Rating", "Title", "ReleaseYear" };
        //public string Sort = "Rating";

        //public Dictionary<bool, string> order = new Dictionary<bool, string>
        //{ 
        //    {true,"descending"},
        //    {false,"ascending"}
        //};
        public bool Ascending { get; set; }

        public int PageCount { get; set; }
        public int PageSize { get; set; }

        //var sortingOrder = new[] { new { val = true, text = "descending" }, new { val = false, text = "ascending" } };

    }
}