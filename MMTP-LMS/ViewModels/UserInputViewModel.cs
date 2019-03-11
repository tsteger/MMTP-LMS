using Microsoft.AspNetCore.Mvc.Rendering;
using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class UserInputViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Course Course { get; set; }
        public string[] CourseName { get; set; }
        public int? CourseId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=.*\d)(?=.*[\W_]).{6,}", ErrorMessage = "Characters are not allowed.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

       // public ICollection<Course> Courses { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
