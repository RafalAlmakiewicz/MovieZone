using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieZone.Core.Domain
{
    public class MovieSubmission
    {
        [Key]
        public int SubmissionId { get; set; }


        public int MovieId { get; set; }

       
        [MaxLength(255), Required]
        public string Title { get; set; }


        [Display(Name = "Release Year"),RegularExpression(@"\d{4}",ErrorMessage = "Year must be 4-digit")]
        public int ReleaseYear { get; set; }


        public Director Director { get; set; }
        [Display(Name = "Director"),Required(ErrorMessage ="Select director from a list above")]
        public int DirectorId { get; set; }


        [Display(Name = "Duration in minutes")]
        public int DurationInMinutes { get; set; }


        [AtLeastOneGenre]
        public List<Genre> Genres { get; set; }


        public string Description { get; set; }
    }
}