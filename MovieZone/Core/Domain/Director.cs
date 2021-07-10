using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieZone.Core.Domain
{
    public class Director
    {       
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Bio { get; set; }

    }
}