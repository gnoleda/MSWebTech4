using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//namespace containing modelmetadatatype attribute
using Microsoft.AspNetCore.Mvc;
// the using below is to enable the addition of annotations
using System.ComponentModel.DataAnnotations;
using ADClassLibrary;
using System.Text.RegularExpressions;

//1b.

//took out Metadata at the end of Models.
namespace a2OEC.Models
{
    //1d.i.
    //apply the ADFArmmetadata class to this partial class
    [ModelMetadataTypeAttribute(typeof(ADFarmMetadata))]

    //1d. add a separate partial class called farm
    //1d.ii turn the farm partial class into a self 
    //validating model by implementing the Ivariableobject interface
    public partial class Farm : IValidatableObject
    {
        //1dii creates a validate method
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            OECContext _context = OEC_Singleton.Context();

            //trimm all strings of leading and trailing spaces
            if (Name != null)
            {
                Name = Name.ToLower().Trim();
                Name = ADValidation.ADCapitalize(Name);
            }
            if (Address != null)
            {
                Address = Address.ToLower().Trim();
                Address = ADValidation.ADCapitalize(Address);
            }
            if(Town != null)
            {
                Town = Town.ToLower().Trim();
                Town = ADValidation.ADCapitalize(Town);
            }
            if(County != null)
            {
                County = County.ToLower().Trim();
                County = ADValidation.ADCapitalize(County);
            }
            if(ProvinceCode != null)
            {
                ProvinceCode = ProvinceCode.Trim();
                ProvinceCode = ProvinceCode.ToUpper();
            }
            if(PostalCode != null)
            {
                PostalCode = PostalCode.Trim();
            }
            if (Email != null)
            {
                Email = Email.Trim();
            }
            if (Directions != null)
            {
                Directions = Directions.Trim();
            }
            
            //either the town or country must be provided
            if(String.IsNullOrWhiteSpace(Town) && String.IsNullOrWhiteSpace(County))
            {
                yield return new ValidationResult("At least one town or country must be provided.");
            }

            if(String.IsNullOrWhiteSpace(Email))
            {
                if(String.IsNullOrWhiteSpace(Address) || String.IsNullOrWhiteSpace(PostalCode))
                {
                    yield return new ValidationResult("If no email, you must provide an address and postal code.");
                }                
            }

            //if postal code provided, validate and format it using postalcodevalidation or zipcode validation, depending which country
            if (!String.IsNullOrWhiteSpace(PostalCode))
            {
                if(!String.IsNullOrWhiteSpace(ProvinceCode))
                {
                    var countryCode = "";

                    if (ProvinceCode.Length == 2)
                    {
                        countryCode = _context.Province.SingleOrDefault(p => p.ProvinceCode == ProvinceCode).CountryCode;
                    }
                    else
                    {
                        countryCode =_context.Province.SingleOrDefault(p => p.Name == ProvinceCode).CountryCode;
                    }
                    
                    string postalCode = PostalCode;
                    if (countryCode == "CA")
                    {
                        if (!ADValidation.ADPostalCodeValidation(ref postalCode))
                        {
                            yield return new ValidationResult("Postal Code is not a valid pattern (N5G 6Z6)", new string[] { nameof(PostalCode) });
                        }
                        else
                        {
                            PostalCode = postalCode;
                        }
                    }
                    if (countryCode == "US")
                    {
                        if(!ADValidation.ADZipCodeValidation(ref postalCode))
                        {
                            yield return new ValidationResult("Zip Code is not a valid pattern (12345 or 12345-1234)", new string[] { nameof(PostalCode) });
                        }
                        PostalCode = postalCode;
                    }
                }
               
            }

            //either home phone or cellphone must be provided
            if(String.IsNullOrWhiteSpace(HomePhone) && String.IsNullOrWhiteSpace(CellPhone))
            {
                yield return new ValidationResult("You must provide either your cell or home phone number.");
            }
            else
            {
                Regex pattern = new Regex(@"\d{10}", RegexOptions.IgnoreCase);

                if (!String.IsNullOrWhiteSpace(HomePhone))
                {
                    //get rid of all punctuation and trim and space
                    HomePhone = Regex.Replace(HomePhone, @"[^\w\s]", "").Trim();
                    HomePhone = Regex.Replace(HomePhone, "[^0-9]", "");

                    if(!pattern.IsMatch(HomePhone))
                    {
                        yield return new ValidationResult("Home phone is incorrect pattern: ", new string[] { nameof(HomePhone) });
                    }
                    else
                    {
                        //insert dashes into phone number
                        HomePhone = Regex.Replace(HomePhone, @"^(...)(...)(....)$", "$1-$2-$3");
                    }
                }

                if (!String.IsNullOrWhiteSpace(CellPhone))
                {
                    //get rid of all punctuation and trim and space
                    CellPhone = Regex.Replace(CellPhone, @"[^\w\s]", "").Trim();
                    CellPhone = Regex.Replace(CellPhone, "[^0-9]", "");

                    if (!pattern.IsMatch(CellPhone))
                    {
                        yield return new ValidationResult("Cell phone is incorrect pattern: ", new string[] { nameof(CellPhone) });
                    }
                    else
                    {
                        //insert dashes into phone number
                        CellPhone = Regex.Replace(CellPhone, @"^(...)(...)(....)$", "$1-$2-$3");
                    }
                }
            }

            //lastcontact date cantbe provided unless datejoined 
            if (LastContactDate != null && DateJoined == null)
            {
                yield return new ValidationResult("You must also provide Date Joined.", new string[] { nameof(DateJoined) });
            }
            //last contact date cant be before date joined
            if (DateJoined > LastContactDate)
            {
                yield return new ValidationResult("Last contact date can not be prior to date joined.");
            }
            
            //1diii replace the throw statement 
            yield return ValidationResult.Success;
            //throw new NotImplementedException();
        }
    }

    public class ADFarmMetadata
    {
        //1c.
        //copy all of the physical property declarations from the farm model
        public int FarmId { get; set; }
        [Display(Name = "Farm Name")]
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        [Display(Name = "Province")]
        [Required]
        [Remote("ProvinceCodeValidation","ADRemote")] //use remote annon 2.h
        public string ProvinceCode { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Home Phone")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Home phone is an incorrect pattern: 123-123-1234")]
        public string HomePhone { get; set; }
        [Display(Name = "Cell Phone")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Cell phone is an incorrect pattern: 123-123-1234")]
        public string CellPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Directions { get; set; }
        [Display(Name = "Date Joined")]
        [ADDateNotInFuture]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString ="{0:dd MMM yyy}")]
        public DateTime? DateJoined { get; set; }
        [Display(Name = "Last Contact")]
        [ADDateNotInFuture]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyy}")]
        public DateTime? LastContactDate { get; set; }
        [Display(Name = "Province Name")]
        public Province ProvinceCodeNavigation { get; set; }
        public ICollection<Plot> Plot { get; set; }
    }

}
