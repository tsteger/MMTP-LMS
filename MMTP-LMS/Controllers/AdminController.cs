using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMTP_LMS.Data;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var student_email = _context.Person.Select(s => s.Email);
            //var student_course_id = _context.Person.Select(s => s.Course.Id);
            var courses = _context.Course.Select(c => c.Name);

            var adminViewModel = new AdminViewModel()
            {
                Courses = _context.Course.Where(c => c.Id == 1).ToList()
            };

            return View(adminViewModel);
        }
    }
}