using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ADClassLibrary
{
    public class ADValidation : ValidationAttribute
    {
        public string ADCapitalize(string strInput)
        {
            //3ai if the string is null, return it unchanged
            if (strInput == null)
            {
                return strInput;
            }
            else
            {
                //change input string to lower case
                //remove leading/trailing spaces
                //uppercase the first letter
                strInput.ToLower().Trim();
                strInput.Substring(1);
            }

            return strInput;
        }

        public bool ADPostalCodeValidation(string pcInput)
        {
            bool isPCValid = true;

            if (pcInput == null)
            {
                isPCValid = true;
            }
            else
            {

            }

            return isPCValid;
        }

        public bool ADZipCodeValidation(string zipInput)
        {
            bool isZipValid = true;

            return isZipValid;
        }
    }
}
