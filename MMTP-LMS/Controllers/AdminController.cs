using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var student_email = _context.Person.Select(s => s.Email);
            var student_course_id = _context.Person.Select(s => s.Course.Id);
            var courses = _context.Course.Select(c => c.Name);

            var users = _context.Person.ToList();
            
            var adminViewModel = new AdminViewModel()
            {
                Courses = _context.Course.Where(c => c.Id != 0).ToList(),
                Modules = _context.Module.Where(m => m.Id != 0).ToList(),
                Documents = _context.Document.Where(d => d.Id != 0).ToList(),
                // Users = _context.User.Where(u => u.Id != null).ToList()
                LmsActivities = _context.LmsActivity.Where(l => l.Id != 0).ToList(),
                Students = _context.Person.ToList()
            };

            return View(adminViewModel);
        }
        public IActionResult CreateCourse()
        {
            return View();
        }
        

    }
}