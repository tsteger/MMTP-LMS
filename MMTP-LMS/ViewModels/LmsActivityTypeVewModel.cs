using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class LmsActivityTypeVewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LmsActivityType> LmsActivityTypes { get; set; }
    }
}
