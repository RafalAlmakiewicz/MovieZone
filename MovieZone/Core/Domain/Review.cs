using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieZone.Core.Domain
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }
        public DateTime DateAdded { get; set; }
    }
}