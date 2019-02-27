﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<LmsActivity> LmsActivities { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }


    }
}
