using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(PositionsMetaData))]
    public partial class Positions
    {
    }

    public class PositionsMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Position")]
        public string Name { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }
    }
}