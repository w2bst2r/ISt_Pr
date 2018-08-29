using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{
    public class SendEmailViewModel
    {
        public int ApplicationID { get; set; }

        public string CandidateFullName { get; set; }

        public string ManagerFullName { get; set; }

        public string CandidateEmail { get; set; }

        public string ManagerEmail { get; set; }

    }
}