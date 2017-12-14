using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//namespace containing modelmetadatatype attribute
using Microsoft.AspNetCore.Mvc;
// the using below is to enable the addition of annotations
using System.ComponentModel.DataAnnotations;
using ADClassLibrary;

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
        private readonly OECContext _context;

        //public ADFarmMetadata(OECContext context)
        //{
        //    _context = context;
        //}

        //1dii creates a validate method
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ADClassLibrary.ADValidation adValidation = new ADClassLibrary.ADValidation();

            //trimm all strings of leading and trailing spaces
            if (Name != null)
            {
                Name = Name.Trim();
                Name = adValidation.ADCapitalize(Name);
            }
            if (Address != null)
            {
                Address = Address.Trim();
                Address = adValidation.ADCapitalize(Address);
            }
            if(Town != null)
            {
                Town = Town.Trim();
                Town = adValidation.ADCapitalize(Town);
            }
            if(County != null)
            {
                County = County.Trim();
                County = adValidation.ADCapitalize(County);
            }
            if(ProvinceCode != null)
            {
                ProvinceCode = ProvinceCode.Trim();
            }
            if(PostalCode != null)
            {
                PostalCode = PostalCode.Trim();
            }
            if (HomePhone != null)
            {
                HomePhone = HomePhone.Trim();
            }
            if(CellPhone != null)
            {
                CellPhone = CellPhone.Trim();
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
                yield return new ValidationResult("You must fill in either Town and/or County fields");
            }

            if(String.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult("You must provide an address and postal code.");
            }

            //if postal code provided, validate and format it using postalcodevalidation or zipcode validation, depending which country
            if (!String.IsNullOrWhiteSpace(PostalCode))
            {
                var provinceCode = _context.Province.SingleOrDefault(p => p.ProvinceCode == ProvinceCode);
                var countryCode = _context.Province.SingleOrDefault

                if 
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
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]\d[A-Za-z] ?\d[A-Za-z]\d$|^(\d{5}|\d{5}\-?\d{4})$", ErrorMessage ="Incorrect postal code format.")] //3bi/ii
        //[RegularExpression(@"^(\d{5}|\d{5}\-\d{4})$", ErrorMessage = "Incorrect zip code format.")] //3ci
        public string PostalCode { get; set; }
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Directions { get; set; }
        [Display(Name = "Date Joined")]
        public DateTime? DateJoined { get; set; }
        [Display(Name = "Last Contact")]
        public DateTime? LastContactDate { get; set; }
        [Display(Name = "Province Code")]
        public Province ProvinceCodeNavigation { get; set; }
        public ICollection<Plot> Plot { get; set; }
    }

}
