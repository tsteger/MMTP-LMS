using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MMTP_LMS.Data;
using MMTP_LMS.Models;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public static double Nav_date { get; private set; }
        public AdminController(ApplicationDbContext context, UserManager<Person> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Admin()
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
                LmsActivities = _context.LmsActivity.Where(l => l.Id != 0).ToList(),
                Students = _context.Person.ToList(),
                LmsActivityTypes = _context.LmsActivityType.Where(lt => lt.Id !=0).ToList()

            };

            return View(adminViewModel);
        }
        public IActionResult Teacher()
        {
            return View();
        }
       
        public IActionResult Index()
        {
            GetStats();

            var student_email = _context.Person.Select(s => s.Email);
            var student_course_id = _context.Person.Select(s => s.Course.Id);
            var courses = _context.Course.Select(c => c.Name);

            var users = _context.Person.ToList();

            var adminViewModel = new AdminViewModel()
            {
                Courses = _context.Course.Where(c => c.Id != 0).ToList(),
                Modules = _context.Module.Where(m => m.Id != 0).ToList(),
                Documents = _context.Document.Where(d => d.Id != 0).ToList(),
                LmsActivities = _context.LmsActivity.Where(l => l.Id != 0).ToList(),
                Students = _context.Person.ToList()
            };

            return View(adminViewModel);

        }

        private void GetStats()
        {
            ViewBag.DocCount = _context.Document.Where(d => d.Name != null && d.IsAdmin).Count();
            ViewBag.CourseCount = _context.Course.Where(c => c.Name != null).Count();
            ViewBag.StudentCount = _context.Users.Where(s => s.UserName != null).Count();
            ViewBag.ModuleCount = _context.Module.Where(m => m.Name != null).Count();
            ViewBag.LmsActivityCount = _context.LmsActivity.Where(l => l.Name != null).Count();
            ViewBag.LmsActivityTypeCount = _context.LmsActivityType.Where(lt => lt.Name != null);
        }

       
    }
}