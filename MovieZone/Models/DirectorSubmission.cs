using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MovieZone.Models
{
    public class DirectorSubmission
    {
        public DirectorSubmission() { }
        public DirectorSubmission(Director director)
        {
            DirectorId = director.Id;
            Name = director.Name;
            Bio = director.Bio;
        }


        [Key]
        public int SubmissionId { get; set; }
        public int DirectorId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Bio { get; set; }
    }
}