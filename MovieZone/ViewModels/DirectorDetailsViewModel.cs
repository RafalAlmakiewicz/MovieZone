using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieZone.Models;

namespace MovieZone.ViewModels
{
    public class DirectorDetailsViewModel
    {
        public Director Director { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public double AvgRating
        {
            get
            {
                if (Movies.Count() == 0)
                    return 0;
                double sum = 0;
                foreach (var movie in Movies)
                {
                    if (movie.RatingAvg == 0)
                        continue;
                    sum += movie.RatingAvg;
                }
                return sum / Movies.Count();
            }            
         } 
        
    }
}