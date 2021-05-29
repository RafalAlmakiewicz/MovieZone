using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieZone.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieZone.ViewModels
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Rating> RatingsWithReview { get; set; }

        [Display(Name ="Your Rating")]
        public int UserRatingValue { get; set; }
    }
}