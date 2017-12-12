using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
                strInput.ToLower().Trim();
                strInput.Substring(1);

                //uppercase first letter of every word in the string
                strInput = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strInput);
            }

            return strInput;
        }

        public bool ADPostalCodeValidation(string pcInput)
        {
            bool isPCValid = true;

            if (pcInput == null || pcInput == "")
            {
                pcInput.ToUpper();//3bv.
            }
            else if (pcInput != null)
            {
                //3bv (insert space)
                for (int i = 3; i < pcInput.Length; i += 3)
                {
                    pcInput = pcInput.Insert(i, " ");
                }
            }          
            else
            {
                isPCValid = false;
            }

            return isPCValid;
        }

        public bool ADZipCodeValidation(string zipInput)
        {
            bool isZipValid = true;

            if (zipInput.Length == 5)
            {
                
            }
            else if(zipInput.Length == 9)
            {

            }
            else
            {
                isZipValid = false;
            }

            return isZipValid;
        }
    }
}
