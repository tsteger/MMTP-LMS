using Microsoft.AspNetCore.Http;
using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class TeacherViewModel
    {
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime CourseStartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime CourseEndDate { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<Module> Modules { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
