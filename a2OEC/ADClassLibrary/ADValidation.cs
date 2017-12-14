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
                //strInput.Substring(1);

                //uppercase first letter of every word in the string
                string[] words = strInput.Split(' ');
                foreach (var item in words)
                {
                    strInput = char.ToUpper(item[0]) + item.Substring(1) + ' ';
                }

                return strInput;
            }

        }

        public bool ADPostalCodeValidation(ref string pcInput)
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

        public bool ADZipCodeValidation(ref string zipInput)
        {
            bool isZipValid = true;

            //if it contains 5 digits, return true, alogn with 5 digits without punctuation
            if (zipInput.Length == 5)
            {

            }
            else if (zipInput.Length == 9 || zipInput.Length == 10)//10 to account for the dash
            {
                //3c.iv along with the digits in the format 12345-1234
                if (zipInput.IndexOf("-") != 5)
                {
                    zipInput = zipInput.Insert(5, "-");
                }
            }
            else
            {
                isZipValid = false;
            }

            return isZipValid;
        }
    }
}
