using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using MMTP_LMS.Models;

namespace MMTP_LMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;

        public StudentController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Student()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;  // null chek
            var userName = _userManager.GetUserName(currentUser);
            var testEmail = _context.Person.Select(e => e.Email).ToArray();

            var studentSet = _context
                .Person;

            var students = studentSet
                .Where(p => p.Email.ToLower().Trim() == userName.ToLower().Trim());

            var student_course_id = students
                .Select(p => p.CourseId)
                .First();



            var student_course_n = _context.Person.Select(o => o.Email.ToLower().Trim() == userName.ToLower().Trim());



            //var student_email = _context.UserLogins.Select(c => c.UserId).ToArray();
            //var student_course_id = _context.Person.Select(s => s.Course.Id).ToArray();
            //  var student_course_module = _context.Person.Select(s=>s.Email).Where

            var org = _context.Person.Include(p => p.Course);

          //  var ret = Mapper.Map<ViewModels.StudentViewModel>(org);

            return View();
        }
    }
}
