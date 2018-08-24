using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(GradesMetaData))]
    public partial class Grades
    {
    }

    public class GradesMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Grade")]
        public string Name { get; set; }
    }
}