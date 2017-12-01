using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//namespace containing modelmetadatatype attribute
using Microsoft.AspNetCore.Mvc;

//1b.
//took out Metadata at the end of Models.
namespace a2OEC.Models
{
    //1d.i.
    //apply the ADFArmmetadata class to this partial class
    [ModelMetadataTypeAttribute(typeof(ADFarmMetadata))]
    
    //1d. add a separate partial class called farm
    public partial class Farm
    {
        //1d.ii turn the farm partial class into a self 
        //validating model by implementing the Ivariableobject interface

        //??
    }
    
    public class ADFarmMetadata
    {
        //1c.
        //copy all of the physical property declarations from the farm model
        public int FarmId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string ProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Directions { get; set; }
        public DateTime? DateJoined { get; set; }
        public DateTime? LastContactDate { get; set; }

        public Province ProvinceCodeNavigation { get; set; }
        public ICollection<Plot> Plot { get; set; }
    }

}
