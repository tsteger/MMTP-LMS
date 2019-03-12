using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<SelectListItem> clist;
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
            clist = GetActivityList();
            ViewBag.List = clist;
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
        public async Task<IActionResult> CreateLmsActivity([Bind("Id,LmsActivityName,LmsActivityDescription,LmsActivityStartDate,LmsActivityEndDate,ModuleId")] LmsActivityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lmsactivity = new LmsActivity()
                {
                    Name = viewModel.LmsActivityName,
                    StartDate = viewModel.LmsActivityStartDate,
                    EndTime = viewModel.LmsActivityEndTime,
                    Description = viewModel.LmsActivityDescription,
                    // LmsActivityType = viewModel.LmsActivityType,
                    LmsActivityTypeId = _context.LmsActivity.Select(f=>f.LmsActivityTypeId).LastOrDefault(),
                    ModuleId = viewModel.ModuleId

                };
                _context.LmsActivity.Add(lmsactivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateLmsActivity), new { id = lmsactivity.ModuleId });
            }
            return View(viewModel);
        }
        // GET: EditLmsActivity
        public async Task<IActionResult> EditLmsactivity(int? id)
        {
            clist = GetActivityList();
            ViewBag.List = clist;
            if (id == null)
            {
                return NotFound();
            }

            var lmsActivity = await _context.LmsActivity.FindAsync(id);
            if (lmsActivity == null)
            {
                return NotFound();
            }
            return View(lmsActivity);
        }

        // POST: EditLmsActivity
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLmsAvctivity(int id, [Bind("Id,Name,Description,StartDate,EndDate")] LmsActivity lmsActivity)
        {
            if (id != lmsActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lmsActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LmsActivityExists(lmsActivity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CreateLmsActivity));
            }
            return View(lmsActivity);


        }

        private bool LmsActivityExists(int id)
        {
            return _context.LmsActivity.Any(e => e.Id == id);
        }
        private List<SelectListItem> GetActivityList()
        {
            return _context.LmsActivity.Select(a =>
                                            new SelectListItem
                                            {
                                                Value = a.Id.ToString(),
                                                Text = a.Name,

                                            }).ToList();
        }
    }
}