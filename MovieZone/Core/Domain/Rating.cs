using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieZone.Core.Domain
{
    public class Rating
    {
        public Movie Movie { get; set; }
        [Key]
        [Column(Order=1)]
        public int MovieId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }


        [Range(1,10, ErrorMessage = "Rate movie from 1 to 10")]
        public int Value { get; set; }

        public Review Review { get; set; }
        public int? ReviewId { get; set; }
    }
}