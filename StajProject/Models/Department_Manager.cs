//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StajProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department_Manager
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int ManagerID { get; set; }
    
        public virtual Departments Departments { get; set; }
        public virtual Managers Managers { get; set; }
    }
}
