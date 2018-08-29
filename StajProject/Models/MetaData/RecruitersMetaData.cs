using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(RecruitersMetaData))]
    public partial class Recruiters
    {
        public string FullName
        {
            get { return string.Format("{0} {1}", Name, Surname); }
        }
    }

    public class RecruitersMetaData
    {

        [Display(Name = "Recruiter")]
        [NotMapped]
        public string FullName { get; set; }

        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
    }
}