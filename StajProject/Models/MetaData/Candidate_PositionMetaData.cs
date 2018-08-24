using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Candidate_PositionMetaData))]
    public partial class Candidate_Position
    {
    }

    public class Candidate_PositionMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Candidate")]
        public int CandidateID { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }
    }
}