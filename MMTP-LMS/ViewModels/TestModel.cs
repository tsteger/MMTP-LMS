using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class TestModel
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


        public ICollection<Course> Courses { get; set; }
    }
}
