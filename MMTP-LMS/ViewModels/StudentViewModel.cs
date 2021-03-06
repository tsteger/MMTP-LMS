﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.ViewModels
{
    public class StudentViewModel
    {
        public string Name { get; set; } 
        public string Description { get; set; } 
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        public LmsActivityType LmsActivityType { get; set; }

        public int AntalDagar { get; set; }           

        public ICollection<Document> Documents { get; set; }

        public IFormFile FileToUpload { get; set; }

        public List<SelectListItem> CourseList { get; set; }

        public bool IsSubmission { get; set; }
        public int Id { get; set; }

    }
}
