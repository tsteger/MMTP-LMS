using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Models
{
    public class Document
    {
        public int Id { get; set; }
        [Display(Name = "Document Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Date Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HHMM}")]
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Document Link")]
        public string Url { get; set; }
        [Display(Name = "Owner")]
        public string PersonId { get; set; }
        public string UserName { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? LmsActivityId { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
