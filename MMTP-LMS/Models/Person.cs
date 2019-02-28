using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Models
{
    public class Person   : IdentityUser
    {
        //public string Id { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Display(Name = "E-Mail")]
        //[DataType(DataType.EmailAddress)]
        //[Required]
        //public string Email { get; set; }

        public ICollection<Document> Documents { get; set; }

        public Course Course { get; set; }
        public int? CourseId { get; set; }
    }
}
