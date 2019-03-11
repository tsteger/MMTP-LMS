using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class LmsActivityViewModel
    {
        [Display(Name = "Activity Name")]
        public string LmsActivityName { get; set; }
        public string LmsActivityDescription { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime LmsActivityStartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime LmsActivityEndTime { get; set; }
        [Display(Name = "Activity Type")]
        public LmsActivityType LmsActivityType { get; set; }
        public int LmsActivityTypeId { get; set; }
        public int LmsActivityId { get; set; }
        public int ModuleId { get; set; }
        [Display(Name = "Module Name")]
        public Module Module { get; set; }
       
        [Display(Name = "Documents")]
        public ICollection<Document> Documents { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<LmsActivity> LmsActivities { get; set; }
    }
}
