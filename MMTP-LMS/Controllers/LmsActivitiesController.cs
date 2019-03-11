using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using MMTP_LMS.Models;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class LmsActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;

        public LmsActivitiesController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: LmsActivity/LmsActivities
        public IActionResult Index()
        {
            var model = new LmsActivityViewModel()
            {
                LmsActivities = _context.LmsActivity.ToList()
            };
            return View(model);
        }
        // GET: CreateLmsActivity
        public async Task<ActionResult> CreateLmsActivity(int? id = null)
        {
            if (id == null) return View();

            var module = await _context.Module.Include(l => l.LmsActivities).FirstOrDefaultAsync(m => m.Id == id);


            if (module == null) return NotFound();

            var model = new LmsActivityViewModel()
            {
                ModuleId = (int)id,


            };
            if (module.LmsActivities.Count() > 0) model.LmsActivities = module.LmsActivities;
            else model.Modules = new List<Module>();

            return View(model);
        }

        // POST: CreateLmsActivity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLmsActivity([Bind("Id,LmsActivityName,LmsActivityDescription,LmsActivityStartDate,LmsActivityEndDate,Modules")] LmsActivityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lmsactivity = new LmsActivity()
                {
                    Name = viewModel.LmsActivityName,
                    StartDate = viewModel.LmsActivityStartDate,
                    EndTime = viewModel.LmsActivityEndTime,
                    Description = viewModel.LmsActivityDescription,
                    LmsActivityType = viewModel.LmsActivityType,
                    ModuleId = viewModel.ModuleId

                };
                _context.LmsActivity.Add(lmsactivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateLmsActivity), new { id = lmsactivity.ModuleId });
            }
            return View(viewModel);
        }

    }
}