using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using MMTP_LMS.Models;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class StudentController : Controller
    {
        private static int? user_course_id;
        private static int today_module_id;
        private static int selected_activity_id;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public static double Nav_date { get; private set; }
        public static int SelectedCourseId { get; private set; } = 1;
        public StudentController(ApplicationDbContext context, UserManager<Person> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Student(double id = 0, int course_select = 0)
        {
           // NoActivity();
            if (id == 0) Nav_date = 0d;
            Nav_date += id;
            var userName = User.Identity.Name;

            if (userName == null)
            {
                return RedirectToAction("Index", "Home");
            }
            user_course_id = GetCourseId(userName, course_select);
            today_module_id = GetModuleId(user_course_id);
            var today_activities = _context.LmsActivity.Where(m => m.ModuleId == today_module_id && m.StartDate.Day <= DateTime.Now.AddDays(Nav_date).Day && m.EndTime.Day >= DateTime.Now.AddDays(Nav_date).Day);
            if (today_activities.Count() < 1) today_activities = _context.LmsActivity.Where(m => m.StartDate.Year <= DateTime.Now.AddYears(-1).Year);



            selected_activity_id = today_activities.Select(i => i.Id).FirstOrDefault();
            var clist = _context.Course.Select(a =>
                                new SelectListItem
                                {
                                    Value = a.Id.ToString(),
                                    Text = a.Name,
                                    Selected = a.Id == user_course_id
                                }).ToList();


            if (Nav_date == 0) ViewBag.TodayHeader = "Dagens Aktiviteter";
            else if (Nav_date == -1) ViewBag.TodayHeader = "Gårdagens Aktiviteter";
            else if (Nav_date == 1) ViewBag.TodayHeader = "Morgondagens Aktiviteter";
            else ViewBag.TodayHeader = $"{DateTime.Now.AddDays(Nav_date).ToString("dd MMMM")} Aktiviteter";
            ViewBag.Course = _context.Course.Where(i => i.Id == user_course_id).Select(n => n.Name).FirstOrDefault();

            var ret = today_activities.Select(x => new StudentViewModel()
            {
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndTime = x.EndTime,
                Documents = x.Documents,
                LmsActivityType = x.LmsActivityType,
                AntalDagar = x.StartDate.Day - x.EndTime.Day,
                CourseList = clist,
            });

            return View(ret);
        }

        private void NoActivity()
        {
            var act = new LmsActivity
            {
                Name = "No Activity",
                Description = "No Activity Today",
                StartDate = DateTime.Now.AddYears(-1),
                EndTime = DateTime.Now.AddYears(-1),
                ModuleId = 1,
                LmsActivityTypeId = 1

            };
            var chk = _context.LmsActivity.Where(n => n.Name == "No Activity").FirstOrDefault();
            if (chk == null)
            {
                _context.LmsActivity.Add(act);
                _context.SaveChanges();
            }
        }

        public IActionResult Course()
        {
            
            int? user_course_id = GetCourseId(User.Identity.Name);
            var mod = _context.Module.Where(m => m.CourseId == user_course_id);
            ViewBag.Course = _context.Course.Where(i => i.Id == user_course_id).Select(n => n.Name).FirstOrDefault();
            var ret = mod.Select(x => new StudentModuleViewModel()
            {
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                Id = x.Id,
                Documents = x.Documents,
                LmsActivities = x.LmsActivities
                
                
            });

            return View(ret);

        }

        private int GetModuleId(int? user_course_id)
        {
            return _context.Module.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now && d.CourseId == user_course_id)
                .Select(m => m.Id)
                .FirstOrDefault();
        }

        private int? GetCourseId(string userName, int course_select = 0)
        {
            int? user_course_id = _context.Person.Where(p => p.UserName.ToLower().Trim() == userName.ToLower().Trim())
                .Select(p => p.CourseId)
                .FirstOrDefault();
            if (user_course_id == null) user_course_id = 1;
            if (User.IsInRole("Admin"))
            {
                if (course_select != 0) SelectedCourseId = course_select;
                user_course_id = SelectedCourseId;
            }

            return user_course_id;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string txt)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }
            string webRootPath = _hostingEnvironment.WebRootPath;
            var path = Path.Combine(
                        webRootPath, "Documents",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var doc = new Document
            {
                Name = file.FileName,
                Description = txt,
                TimeStamp = DateTime.Now,
                UserName = User.Identity.Name,
                Url = path,
                CourseId = user_course_id,
                ModuleId = today_module_id,
                LmsActivityId = selected_activity_id,
                PersonId = _context.Person.Where(p => p.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault()
            };
           
            _context.Document.Add(doc);
            _context.SaveChanges();

            return RedirectToAction("Student", "Student");
        }

    }
}
