﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectEntities : DbContext
    {
        public ProjectEntities()
            : base("name=ProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Application_Manager> Application_Manager { get; set; }
        public virtual DbSet<Application_Recruiter> Application_Recruiter { get; set; }
        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<Candidates> Candidates { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Recruiters> Recruiters { get; set; }
        public virtual DbSet<Registrations> Registrations { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
