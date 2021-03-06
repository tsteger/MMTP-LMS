﻿
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
using MMTP_LMS.Utilities;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class StudentController : Controller
    {
        private List<SelectListItem> clist;
        private static int? user_course_id;
        private static int today_module_id;
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        private static DateTime NavDate { get; set; } = DateTime.Now;
        public static int Nav_date { get; private set; }
        public static int SelectedCourseId { get; private set; } = 1;
        public StudentController(ApplicationDbContext context, UserManager<Person> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            clist = GetCourseList();
            
        }

        public IActionResult Student(string Id, int course_select = 0)
        {
            
            var dbUtilities = new DbUtilities();
            dbUtilities.AddDatabaseData(_context);
            var userName = User.Identity.Name;

            if (DateTime.TryParse(Id, out DateTime dateTime))
            {
                if (NavDate < dateTime.Date)
                {                 
                    Nav_date += dateTime.Date.Day - NavDate.Day;
                    NavDate = dateTime.Date;
                }
                else if (NavDate > dateTime.Date)
                {
                    Nav_date += dateTime.Date.Day - NavDate.Day;
                    NavDate = dateTime.Date;
                }
                else
                    NavDate = dateTime.Date;
                ViewBag.datum = Nav_date;
            }
            else
            {
                NavDate = DateTime.Now.Date;
                ViewBag.datum = 0;
                Nav_date = 0;
            }

            if (userName == null)
            {
                return RedirectToAction("Index", "Home");
            }
            user_course_id = GetCourseId(userName, course_select);
            today_module_id = GetModuleId(user_course_id);
            
            var today_activities = _context.LmsActivity.Where(m => m.ModuleId == today_module_id && m.StartDate.Date <= NavDate && m.EndTime.Date >= NavDate);
            if (today_activities.Count() < 1) today_activities = _context.LmsActivity.Where(m => m.StartDate.Year <= DateTime.Now.AddYears(-1).Year);

            clist = GetCourseList();
            if (Nav_date == 0) ViewBag.TodayHeader = "Todays Activities";
            else if (Nav_date == -1) ViewBag.TodayHeader = "Yesterdays Activities";
            else if (Nav_date == 1) ViewBag.TodayHeader = "Tomorrows Activities";
            else ViewBag.TodayHeader = $"{DateTime.Now.AddDays(Nav_date).ToString("dd MMMM")} Activities";
            ViewBag.Course = _context.Course.Where(i => i.Id == user_course_id).Select(n => n.Name).FirstOrDefault();

            var ret = today_activities.Select(x => new StudentViewModel()
            {
                
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndTime = x.EndTime,
                Documents = x.Documents,
                LmsActivityType = x.LmsActivityType,
                AntalDagar =  x.EndTime.Day - x.StartDate.Day,
                CourseList = clist,
                IsSubmission = x.IsSubmission,
                Id =x.Id

            });

            return View(ret);
        }

        private List<SelectListItem> GetCourseList()
        {
            return _context.Course.Select(a =>
                                            new SelectListItem
                                            {
                                                Value = a.Id.ToString(),
                                                Text = a.Name,
                                                Selected = a.Id == user_course_id
                                            }).ToList();
        }

        public IActionResult Course(int course_select = 0)
        {                               
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.courses = _context.Document.Where(d => d.CourseId != null && d.IsAdmin);
            NavDate = DateTime.Now.Date;
            ViewBag.datum = 0;
            Nav_date = 0;
            user_course_id = GetCourseId(User.Identity.Name, course_select);
            clist = GetCourseList();
            var mod = _context.Module.Where(m => m.CourseId == user_course_id);
            ViewBag.Course = _context.Course.Where(i => i.Id == user_course_id).Select(n => n.Name).FirstOrDefault();
            var ret = mod.Select(x => new StudentModuleViewModel()
            {
                
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Id = x.Id,
                Documents = x.Documents,
                LmsActivities = x.LmsActivities,
                CourseList = clist,
                
                
            });

            return View(ret.OrderBy(d=>d.StartDate));

        }
        public IActionResult Users()
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var mod = _context.Person.Where(c => c.CourseId == user_course_id);
            var ret = mod.Select(p => new UsersViewModel()
            {
                Id =p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email =p.Email,
                Course = p.Course,
                myCourse = p.Course.Name,
                UserName = p.UserName
                

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
        public async Task<IActionResult> UploadFile(IFormFile file, string txt, string activity_id)
        {
        
            if (!int.TryParse(activity_id, out int id)) return NoContent();

            var fileUtil = new Utilities.File();
            await fileUtil.UploadFileAsync(file, txt, _hostingEnvironment);
            var doc = new Document
            {
                Name = file.FileName,
                Description = txt,
                TimeStamp = DateTime.Now,
                UserName = User.Identity.Name,
                Url = "\\Documents\\" + file.FileName,
                CourseId = user_course_id,
                ModuleId = today_module_id,
                LmsActivityId =  id,
                PersonId = _context.Person.Where(p => p.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault()
            };

            _context.Document.Add(doc);
            _context.SaveChanges();

            return RedirectToAction("Student", "Student", new { Id = _context.LmsActivity.Where(i=>i.Id == id).FirstOrDefault().StartDate.ToShortDateString() });
        }

    }
}
