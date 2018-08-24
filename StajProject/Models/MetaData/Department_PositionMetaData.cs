using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Department_PositionMetaData))]
    public partial class Department_Position
    {
    }

    public class Department_PositionMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }

        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
    }
}