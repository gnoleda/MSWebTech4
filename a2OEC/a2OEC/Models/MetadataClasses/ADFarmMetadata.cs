using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//namespace containing modelmetadatatype attribute
using Microsoft.AspNetCore.Mvc;
// the using below is to enable the addition of annotations
using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        [Display(Name = "Province")]
        public string ProvinceCode { get; set; }
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]\d[A-Za-z] ?\d[A-Za-z]\d$|^(\d{5}|\d{5}\-?\d{4})$", ErrorMessage ="Incorrect postal code format.")] //3bi/ii
        //[RegularExpression(@"^(\d{5}|\d{5}\-\d{4})$", ErrorMessage = "Incorrect zip code format.")] //3ci
        public string PostalCode { get; set; }
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
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
