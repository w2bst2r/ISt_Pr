using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Department_ManagerMetaData))]
    public partial class Department_Manager
    {
    }

    public class Department_ManagerMetaData
    {

        public int ID { get; set; }

        [Display(Name = "Department")]
        public int DepartmentID { get; set; }

        [Display(Name = "Manager")]
        public int ManagerID { get; set; }
    }
}