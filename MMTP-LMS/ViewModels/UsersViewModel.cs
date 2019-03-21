using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class UsersViewModel
    {
        public string  Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string myCourse { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public Course Course { get; set; }
     
        public int? CourseId { get; set; }
        //public List<Course> Courses { get; set; }
    }
}
