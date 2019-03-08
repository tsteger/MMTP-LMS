using MMTP_LMS.Data;
using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Utilities
{
    public class DbUtilities
    {
        private readonly ApplicationDbContext _context;
        public DbUtilities(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddDatabaseData()
        {
            
            if (_context.Course.Where(n => n.Id == 1).FirstOrDefault() == null)
            {
                var course = new Course
                {
                    Name = "Default",
                    Description = "Default",
                    StartDate = DateTime.Now.AddYears(-1),
                    EndDate = DateTime.Now.AddYears(-1),
                 

                };
                _context.Course.Add(course);
                _context.SaveChanges();
            }
            
            if (_context.Module.Where(n => n.Id == 1).FirstOrDefault() == null)
            {
                var module = new Module
                {
                    Name = "Default",
                    Description = "Default",
                    StartDate = DateTime.Now.AddYears(-1),
                    EndDate = DateTime.Now.AddYears(-1),
                    CourseId = 1

                };
                _context.Module.Add(module);
                _context.SaveChanges();
            }
           
            var chk = _context.LmsActivity.Where(n => n.Name == "No Activity").FirstOrDefault();
            if (chk == null)
            {
                var act = new LmsActivity
                {
                    Name = "No Activity",
                    Description = "No Activity Today",
                    StartDate = DateTime.Now.AddYears(-1),
                    EndTime = DateTime.Now.AddYears(-1),
                    ModuleId = 1,
                    

                };
                _context.LmsActivity.Add(act);
                _context.SaveChanges();
            }
        }
    }
}
