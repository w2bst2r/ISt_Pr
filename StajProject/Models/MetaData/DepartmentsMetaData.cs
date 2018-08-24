using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(DepartmentsMetaData))]
    public partial class Departments
    {
    }

    public class DepartmentsMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Department")]
        public string Name { get; set; }
    }
}