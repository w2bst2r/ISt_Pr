using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(ManagersMetaData))]
    public partial class Managers
    {
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, Surname); }
        }
    }

    public class ManagersMetaData
    {
        public int ID { get; set; }

        [NotMapped]
        [Display(Name = "Manager")]
        public string FullName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Phone No")]
        [Phone]
        public string PhoneNo { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}