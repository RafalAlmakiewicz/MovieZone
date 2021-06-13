using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieZone.Models
{
    public class AtLeastOneGenre : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movieSubmission = (MovieSubmission)validationContext.ObjectInstance;

            foreach(var genre in movieSubmission.Genres)
            {
                if (genre != null && genre.Id != 0)
                    return ValidationResult.Success;
            }
            return new ValidationResult("Select at least 1 genre");
        }
    }
}