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
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        public LmsActivityType LmsActivityType { get; set; }

        public int AntalDagar { get; set; }

        public ICollection<Document> Documents { get; set; }
        public string[] UserDocuments { get; set; }
        static public double Nav_date { get; set; }

        public List<Person> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Module> Modules { get; set; }
        public List<LmsActivity> LmsActivities { get; set; }
        public List<Person> Admins { get; set; }

        public IFormFile FileToUpload { get; set; }
    }
}
