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
        public IActionResult Student()
        {
            // --- Code för senare kanske
            //ClaimsPrincipal currentUser = this.User;         
            //var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // null chek
            //var userName = _userManager.GetUserName(currentUser);
            //_userManager.GetUsersInRoleAsync


            var userName = User.Identity.Name;        
            if (userName == null)
            {
                return RedirectToAction("Index","Home");
            }
            var testEmail = _context.Person.Select(e => e.Email).ToArray();  
            
            var user_course_id = _context.Person.Where(p => p.UserName.ToLower().Trim() == userName.ToLower().Trim())
                .Select(p => p.CourseId)
                .FirstOrDefault();

            var today_module_id = _context.Module.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now && d.CourseId== user_course_id)
                .Select(m => m.Id)
                .FirstOrDefault();

             var today_activities = _context.LmsActivity.Where(m=>m.ModuleId== today_module_id && m.StartDate<=DateTime.Now && m.EndTime <= DateTime.Now);
           
         

           
            // Later code
           //  var ret = Mapper.Map<ViewModels.StudentViewModel>(org);

            return View(today_activities);
        }
    }
}
