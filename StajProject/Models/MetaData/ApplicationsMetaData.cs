using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Application_MetaData))]
    public partial class Applications
    {
    }

    public class Application_MetaData
    {
        public int ID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Display(Name = "Candidate")]
        public int CandidateID { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }

        [Display(Name = "Grade")]
        public int GradeID { get; set; }

        [Display(Name = "Email Sent?")]
        public bool IsSent { get; set; }
    }
}