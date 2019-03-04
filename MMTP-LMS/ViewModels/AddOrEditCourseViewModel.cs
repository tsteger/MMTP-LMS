using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class AddOrEditCourseViewModel
    {
        public List<Course> Course { get; set; }
        public List<Module> Modules { get; set; }
        public List<LmsActivity> LmsActivities { get; set; }
        public List<Document> Documents { get; set; }
        public List<Person> Students { get; set; }
    }
}
