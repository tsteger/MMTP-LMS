using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Models;

namespace MMTP_LMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       


        public DbSet<MMTP_LMS.Models.Person> Person { get; set; }
        public DbSet<MMTP_LMS.Models.Document> Document { get; set; }
        public DbSet<MMTP_LMS.Models.LmsActivity> LmsActivity { get; set; }
        public DbSet<MMTP_LMS.Models.Course> Course { get; set; }
        public DbSet<MMTP_LMS.Models.Module> Module { get; set; }

    }
}
