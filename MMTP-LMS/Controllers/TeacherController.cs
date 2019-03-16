using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TeacherController(ApplicationDbContext context, UserManager<Person> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Teacher()
        {
            //var courses = _context.Course;

            //var getCourses = courses.Select(x => new TeacherViewModel()
            //{
            //    CourseName = x.Name,
            //    CourseDescription = x.Description,
            //    CourseStartDate = x.StartDate,
            //    CourseEndDate = x.EndDate,
            //});
            var model = new CourseViewModel()
            {
                Courses = _context.Course.ToList()
            };

            return View(model);
        }

        public IActionResult CreateCourse()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse([Bind("Id,CourseName,CourseDescription,CourseStartDate,CourseEndDate")] CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var course = new Course()
                {
                    Name = viewModel.CourseName,
                    StartDate = viewModel.CourseStartDate,
                    EndDate = viewModel.CourseEndDate,
                    Description = viewModel.CourseDescription

                };
                _context.Course.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Teacher));
            }
            return View(viewModel);
        }
        // GET: Course/EditCourse/5
        public async Task<IActionResult> EditCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Course/EditCourse/5<------Här ligger jag !
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(int id, [Bind("Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Teacher));
            }
            return View(course);
        }
        // GET: Course/Delete/
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourseConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Teacher));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string txt, string course_id)
        {

            if (!int.TryParse(course_id, out int id)) return NoContent();

            var fileUtil = new Utilities.File();
            await fileUtil.UploadFileAsync(file, txt, _hostingEnvironment);
            var doc = new Document
            {
                Name = file.FileName,
                Description = "Course: " + _context.Course.Where(c=>c.Id == id).Select(n =>n.Name).FirstOrDefault(),
                TimeStamp = DateTime.Now,
                UserName = User.Identity.Name,
                Url = "\\Documents\\" + file.FileName,
                IsAdmin = true,
                CourseId = id,
                PersonId = _context.Person.Where(p => p.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault()
            };

            _context.Document.Add(doc);
            _context.SaveChanges();

            return RedirectToAction("Teacher", "Teacher");
        }

    }
}