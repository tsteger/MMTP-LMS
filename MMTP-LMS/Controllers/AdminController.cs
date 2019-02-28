using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;

namespace MMTP_LMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
                
        public IActionResult AdminListView()
        {
            var courses = _context.Course.Select(c => c.Name);
            var modules = _context.Module.Select(m => m.Name);
            var lmsActivities = _context.LmsActivity.Select(m => m.Name);
            var documents = _context.Document.Select(d => d.Name);
            var people = _context.Person.Select(p => p.FirstName);


            return View();
        }

    }
}