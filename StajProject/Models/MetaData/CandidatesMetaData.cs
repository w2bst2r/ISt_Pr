using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(CandidatesMetaData))]
    public partial class Candidates
    {
    }

    public class CandidatesMetaData
    {

        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "PhoneNo")]
        [Required]
        [Phone]
        public string PhoneNo { get; set; }

        [MaxLength(2)]
        [Required]
        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }
    }

    public enum State
    {
        Active, Inactive
    }
}