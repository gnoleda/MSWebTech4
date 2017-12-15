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
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    if (value != null && (value.ToString().Split(' ').Length > MaxWords ||
        //                          value.ToString().Split(' ').Length < MinWords))
        //        return new ValidationResult(
        //            String.Format(ErrorMessage, validationContext.DisplayName, MaxWords, MinWords));
        //    else
        //        return ValidationResult.Success;
        //}

    }
}
