using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using MMTP_LMS.Models;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class UsersController : Controller
    {

        public List<SelectListItem> clist;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        public UsersController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }
       
        public IActionResult CreateUser()
        {
            clist = GetCourseList();
            ViewBag.List = clist;
            ViewBag.Course =_context.Course.ToList();
            var model = new UserInputViewModel()
            {
                People = _context.Person.ToList(),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserInputViewModel Input)
        {
            if (ModelState.IsValid)
            {
                var user = new Person
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    CourseId = Input.CourseId,
                    Course = Input.Course,
                    
                    
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {                  
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);                   
                    return RedirectToAction("CreateUser","Users");
                }
                foreach (var error in result.Errors)
                {
                    return RedirectToAction("CreateUser", "Users");
                   // ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(Input);
        }
        public async Task<IActionResult> EditUser(string id)
        {
            clist = GetCourseList();
            ViewBag.List = clist;
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, [Bind("Id,FirstName,LastName,Email,CourseId")]  Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(person.Id);
                    user.FirstName = person.FirstName;
                    user.LastName = person.LastName;
                    user.CourseId = person.CourseId;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("CreateUser", "Users");
                }
                catch (DbUpdateException e)
                {
                    new Exception(e.Message);
                }
                
            }
            
            return View(person);
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("CreateUser", "Users");
        }

       
        private bool PersonExists(string id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
        private List<SelectListItem> GetCourseList()
        {
            return _context.LmsActivity.Select(a =>
                                            new SelectListItem
                                            {
                                                Value = a.Id.ToString(),
                                                Text = a.Name,
                                                
                                            }).ToList();
        }
    }
}