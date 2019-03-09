using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MMTP_LMS.Controllers
{
    public class LmsActivitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}