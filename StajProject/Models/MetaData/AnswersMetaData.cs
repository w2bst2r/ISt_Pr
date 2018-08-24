using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(AnswersMetaData))]
    public partial class Answers
    {
    }

    public class AnswersMetaData
    {
        [Display(Name = "Question")]
        public int QuestionID { get; set; }

        [Display(Name = "Application No")]
        public int ApplicationID { get; set;}

        [Display(Name = "Candidate Answer")]
        public string CandidateAnswer { get; set; }

        [Display(Name = "Manager Answer")]
        public string ManagerAnswer { get; set; }
    }
}