using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ADClassLibrary
{
    //add a class called datenoinfutureattribute and have it extend validation class
    public class ADDateNotInFutureAttribute : ValidationAttribute
    {
        public  ADDateNotInFutureAttribute()
        {
            ErrorMessage = "{0} cannot be in the future.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = Convert.ToDateTime(value);
            if (date > DateTime.Now)
                return new ValidationResult(
                    String.Format(ErrorMessage, validationContext.DisplayName));
            else
                return ValidationResult.Success;
        }

    }
}
