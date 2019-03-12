using MMTP_LMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Utilities
{
    public class DateUtilities
    {
        public DateTime GetModuleStartDate(ApplicationDbContext _context,int? id)
        {
            if(id==null)
                return DateTime.Now;
            var mod = _context.Module.Where(c => c.CourseId == id).LastOrDefault().EndDate;
            if (mod != null)
                return mod.AddDays(1); // inte klar en star bara
            else
                return DateTime.Now;
        }
    }
}
