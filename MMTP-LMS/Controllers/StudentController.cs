using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public StudentController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Student()
        {           
            var userName = User.Identity.Name;        
            if (userName == null)
            {
                return RedirectToAction("Index","Home");
            }
            
            var user_course_id = _context.Person.Where(p => p.UserName.ToLower().Trim() == userName.ToLower().Trim())
                .Select(p => p.CourseId)
                .FirstOrDefault();

            var today_module_id = _context.Module.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now && d.CourseId== user_course_id)
                .Select(m => m.Id)
                .FirstOrDefault();

            var today_activities = _context.LmsActivity.Where(m=>m.ModuleId== today_module_id && m.StartDate.Day<=DateTime.Now.Day && m.EndTime.Day >= DateTime.Now.Day);

            



            ViewBag.Course = _context.Course.Where(i=>i.Id == user_course_id).Select(n=> n.Name).FirstOrDefault();

            var ret = today_activities.Select(x => new StudentViewModel()
            {
                Name        = x.Name,
                Description = x.Description,
                StartDate   = x.StartDate,
                EndTime     = x.EndTime,            
                Documents   = x.Documents,
                LmsActivityType = x.LmsActivityType,              
                AntalDagar = x.StartDate.Day - x.EndTime.Day,
               

            });
            return View(ret);
        }
    }
}
