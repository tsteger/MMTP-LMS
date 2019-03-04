using Microsoft.AspNetCore.Http;
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
        public ICollection<Document> UserDocuments { get; set; }
        static public double nav_date { get; set; }

        public IFormFile FileToUpload { get; set; }
    }
}
