using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class ModuleViewModel
    {
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }
        [Display(Name = "Description")]
        public string ModuleDescription { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime ModuleStartDate { get; set; } = DateTime.Now;
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime ModuleEndDate { get; set; } = DateTime.Now.AddYears(1);
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public ICollection<Module> Modules { get; set; }
    }
}
