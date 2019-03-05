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
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using MMTP_LMS.Models;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public static double Nav_date { get; private set; }
        public StudentController(ApplicationDbContext context, UserManager<Person> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        

        public IActionResult Student(double id = 0)
        {
            if (id == 0) Nav_date = 0d;
            Nav_date += id;
            var userName = User.Identity.Name;        
            if (userName == null)
            {
                return RedirectToAction("Index","Home");
            }
            var user_doc = _context.Document.Where(p => p.UserName.ToLower().Trim() == userName.ToLower().Trim())
               .Select(p => p.Name)
               .ToArray();

            var user_course_id = _context.Person.Where(p => p.UserName.ToLower().Trim() == userName.ToLower().Trim())
                .Select(p => p.CourseId)
                .FirstOrDefault();

            var today_module_id = _context.Module.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now && d.CourseId== user_course_id)
                .Select(m => m.Id)
                .FirstOrDefault();

            var today_activities = _context.LmsActivity.Where(m=>m.ModuleId== today_module_id && m.StartDate.Day<=DateTime.Now.AddDays(Nav_date).Day && m.EndTime.Day >= DateTime.Now.AddDays(Nav_date).Day);



            if (Nav_date == 0) ViewBag.TodayHeader = "Dagens Aktiviteter";
            else if(Nav_date ==-1) ViewBag.TodayHeader = "Gårdagens Aktiviteter";
            else if (Nav_date == 1) ViewBag.TodayHeader = "Morgondagens Aktiviteter";
            else ViewBag.TodayHeader = $"{DateTime.Now.AddDays(Nav_date).ToString("dd MMMM")} Aktiviteter";

            ViewBag.Course = _context.Course.Where(i=>i.Id == user_course_id).Select(n=> n.Name).FirstOrDefault();

            var ret = today_activities.Select(x => new StudentViewModel()
            {
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndTime = x.EndTime,
                Documents = x.Documents,
                LmsActivityType = x.LmsActivityType,
                AntalDagar = x.StartDate.Day - x.EndTime.Day,
                UserDocuments = user_doc

            });
            return View(ret);
        }

        public IActionResult CourseModules()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string txt)
        {
            if (file == null || file.Length == 0)
            {              
                return Content("file not selected");              
            }
            
             
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
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
                Description =  txt,
                TimeStamp = DateTime.Now,
                UserName= User.Identity.Name,
                Url = path,
               // PersonId = _context.Person.Where(p => p.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault()

            };
            _context.Document.Add(doc);
            _context.SaveChanges();
            
            return RedirectToAction("Student", "Student");
        }       
        
    }
}
