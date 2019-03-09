using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Models
{
    public class LmsActivity
    {
        public int Id { get; set; }
        [Display(Name = "Activity Name")]
        public string Name { get; set; } 
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        [Display(Name = "Activity Type")]
        public LmsActivityType LmsActivityType { get; set; }
        public int? LmsActivityTypeId { get; set; }
        
        public int ModuleId { get; set; }
        [Display(Name = "Module Name")]
        public Module Module { get; set; }

        [Display(Name = "Documents")]
        public ICollection<Document> Documents { get; set; }


    }
}
