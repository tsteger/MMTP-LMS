using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class AdminViewModel
    {
        public List<Person> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Module> Modules { get; set; }
        public List<LmsActivity> LmsActivities { get; set; }
        public List<Document> Documents { get; set; }
        public List<Person> Admins { get; set; }
        public List<LmsActivityType> LmsActivityTypes { get; set; }
        
    }
}
