using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(ManagersMetaData))]
    public partial class Managers
    {
    }

    public class ManagersMetaData
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Phone No")]
        [Phone]
        public string PhoneNo { get; set; }
    }
}