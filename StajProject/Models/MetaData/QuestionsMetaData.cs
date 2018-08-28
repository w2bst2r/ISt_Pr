using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(QuestionsMetaData))]
    public partial class Questions
    {
    }

    public class QuestionsMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Question")]
        public string Question { get; set; }
    }
}