using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieZone.Models;

namespace MovieZone.ViewModels
{
    public class SubmissionsViewModel
    {
        public IEnumerable<MovieSubmission> MovieSubmissions { get; set; }


        public int PageCount { get; set; }

        public string SubmissionType(int id) => (id == 0) ? "New" : "Update";
        

    }
}