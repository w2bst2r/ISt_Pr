using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Application_ManagerMetaData))]
    public partial class Application_Manager
    {
    }

    public class Application_ManagerMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Application No")]
        public int ApplicationID { get; set; }

        [Display(Name = "Manager")]
        public int ManagerID { get; set; }
    }
}