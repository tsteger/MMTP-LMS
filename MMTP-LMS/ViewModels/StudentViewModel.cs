﻿using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class StudentViewModel
    {
     
        public string Name { get; set; }
       
       
        public DateTime StartDate { get; set; }
      
        public DateTime EndTime { get; set; }
      
        public LmsActivityType LmsActivityType { get; set; }
        public int LmsActivityTypeId { get; set; }

        public int ModuleId { get; set; }
        
        public Module Module { get; set; }

       
        public ICollection<Document> Documents { get; set; }

    }
}