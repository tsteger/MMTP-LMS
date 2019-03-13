using MMTP_LMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Utilities
{
    public class DateUtilities
    {
        
        public DateTime GetModuleStartDate(ApplicationDbContext _context, int? id)
        {
            if(_context.Module.Where(c => c.CourseId == id).Count() < 1)
            {
                return  _context.Course.Where(c => c.Id == id).FirstOrDefault().StartDate;
            }
               
            return _context.Module.Where(c => c.CourseId == id).LastOrDefault().EndDate.AddDays(1);

           
        }
        public DateTime GetModuleEndDate(ApplicationDbContext _context, int? id)
        {
            if (_context.Module.Where(c => c.CourseId == id).Count() < 1)
            {
                return _context.Course.Where(c => c.Id == id).FirstOrDefault().StartDate.AddMonths(3);
            }

            return _context.Module.Where(c => c.CourseId == id).LastOrDefault().EndDate.AddMonths(3);


        }
        public DateTime GetActivityStartDate(ApplicationDbContext _context, int? id)
        {

            if (_context.LmsActivity.Where(c => c.ModuleId == id).Count() < 1)
            {
                return DateTime.Now;
               // return _context.Module.Where(c => c.Id == id).FirstOrDefault().StartDate;
            }

            return _context.LmsActivity.Where(c => c.ModuleId == id).LastOrDefault().EndTime.AddDays(1);


        }
        public DateTime GetActivityEndDate(ApplicationDbContext _context, int? id)
        {

            if (_context.LmsActivity.Where(c => c.ModuleId == id).Count() < 1)
            {
                return DateTime.Now;
                //return _context.Module.Where(c => c.Id == id).FirstOrDefault().StartDate;
            }

            return _context.LmsActivity.Where(c => c.ModuleId == id).LastOrDefault().EndTime.AddDays(1);


        }
    }
}
