using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;

namespace MMTP_LMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Student()
        {
            //  var applicationDbContext = _context.Person.Include(p => p.Course);
            //
            //return View(await applicationDbContext.ToListAsync());
            var student_email = _context.Person.Select(s => s.Email);
            var student_course_id = _context.Person.Select(s => s.Course.Id);


            var org = _context.Person.Include(p => p.Course);

            var ret = Mapper.Map<ViewModels.StudentViewModel>(org);

            return View();
        }
    }
}