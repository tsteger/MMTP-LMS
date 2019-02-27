using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name="Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public ICollection<Document> Documents { get; set; }
        [Display(Name = "Activities")]
        public ICollection<LmsActivity> LmsActivities { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }


    }
}
