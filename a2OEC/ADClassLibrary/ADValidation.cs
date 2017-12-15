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
        public static string ADCapitalize(string strInput)
        {
            //3ai if the string is null, return it unchanged

            if (strInput == null)
            {
                return strInput;
            }
            else
            {
                //uppercase first letter of every word in the string
                string[] words = strInput.Split(' ');
                foreach (var item in words)
                {
                    strInput = char.ToUpper(item[0]) + item.Substring(1) + ' ';
                }

                return strInput;
            }
        }

        public static bool ADPostalCodeValidation(ref string postalCode)
        {

            postalCode = Regex.Replace(postalCode, @"[^\w\s]", "");
            Regex pattern = new Regex(@"[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d", RegexOptions.IgnoreCase);
            if(postalCode != null || postalCode.Trim() !="")
            {
                if (!pattern.IsMatch(postalCode))
                {
                    return false;
                }
                else
                {
                    postalCode = postalCode.ToUpper();
                    postalCode = Regex.Replace(postalCode, @"^(...)(...)", "$1 $2");
                }
            }

            return true;
        }

        public static bool ADZipCodeValidation(ref string zipCode)
        {
            if (zipCode != null || zipCode.Trim() != "")
            {
                zipCode = Regex.Replace(zipCode, @"[^\w\s]", ""); //remove punctuation
                Regex pattern = new Regex(@"\d{5}(-\d{4})?", RegexOptions.IgnoreCase);

                if (!pattern.IsMatch(zipCode))
                {
                    return false;
                }
            }
            
            return true;
        }

    }
}
