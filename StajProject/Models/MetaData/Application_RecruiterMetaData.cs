using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Application_RecruiterMetaData))]
    public partial class Application_Recruiter
    {
    }

    public class Application_RecruiterMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Application No")]
        public int ApplicationID { get; set; }

        [Display(Name = "Recruiter")]
        public int RecruiterID { get; set; }
    }
}