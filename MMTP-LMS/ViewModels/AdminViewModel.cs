using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class AdminViewModel
    {
        public ICollection<Person> People { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<LmsActivity> LmsActivities { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
