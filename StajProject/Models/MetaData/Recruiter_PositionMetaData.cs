using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Recruiter_PositionMetaData))]
    public partial class Recruiter_Position
    {
    }

    public class Recruiter_PositionMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Recruiter")]
        public int RecruiterID { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }
    }
}