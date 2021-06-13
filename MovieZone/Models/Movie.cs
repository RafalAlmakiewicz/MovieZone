using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieZone.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public Director Director { get; set; }
        public int DirectorId { get; set; }

        [Display(Name ="Duration in minutes")]
        public int DurationInMinutes { get; set; }
        public List<Genre> Genres { get; set; }
        public double RatingAvg
        {
            get
            {
                if (NumberOfRatings == 0)
                    return 0;
                return (double)SumOfRatings / NumberOfRatings;
            }
        }
        
        public int NumberOfRatings { get; set; }
        public int SumOfRatings { get; set; }
        public string Description { get; set; }


    }
}