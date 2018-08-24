using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProject.Models
{

    [MetadataType(typeof(Manager_PositionMetaData))]
    public partial class Manager_Position
    {
    }

    public class Manager_PositionMetaData
    {
        public int ID { get; set; }

        [Display(Name = "Manager")]
        public int ManagerID { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }
    }
}