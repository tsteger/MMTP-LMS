using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Display(Name = "Course Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        public ICollection<Document> Documents { get; set; }
        public ICollection<Module> Modules { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
