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
using MMTP_LMS.Utilities;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class LmsActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        public List<SelectListItem> clist;
        private DateUtilities dateUtilities;
        private static int retViewId;
        public LmsActivitiesController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
            dateUtilities = new DateUtilities();
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
            if (id == null) return NotFound();
            
            var module = await _context.Module.Include(l => l.LmsActivities).FirstOrDefaultAsync(m => m.Id == id);

            
            if (module == null) return NotFound();
            retViewId = module.Id;
            var model = new LmsActivityViewModel()
            {
                ModuleId = (int)id,          
                LmsActivityStartDate = dateUtilities.GetActivityStartDate(_context, id),
                LmsActivityEndTime = dateUtilities.GetActivityEndDate(_context, id)


            };
            if (module.LmsActivities.Count() > 0) model.LmsActivities = module.LmsActivities;
            else model.Modules = new List<Module>();

            return View(model);
        }

        // POST: CreateLmsActivity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLmsActivity( LmsActivityViewModel viewModel)
        {
          //  var myid = clist.Select(h => h.Value).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var lmsactivity = new LmsActivity()
                {
                    Name = viewModel.LmsActivityName,
                    StartDate = viewModel.LmsActivityStartDate,
                    EndTime = viewModel.LmsActivityEndTime,
                    Description = viewModel.LmsActivityDescription,
                    LmsActivityTypeId = viewModel.LmsActivityTypeId,
                    LmsActivityType = viewModel.LmsActivityType,
                    ModuleId = viewModel.ModuleId,
                    IsSubmission = viewModel.IsSubmission

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
        public async Task<IActionResult> EditLmsactivity(int id,LmsActivity lmsActivity)
        {
            if (id != lmsActivity.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var activity = new LmsActivity();
                    activity.Id = lmsActivity.Id;
                     _context.Attach(activity);
                    activity.Name = lmsActivity.Name;
                    activity.Description = lmsActivity.Description;
                    activity.StartDate = lmsActivity.StartDate;
                    activity.EndTime = lmsActivity.EndTime;
                    activity.ModuleId = lmsActivity.ModuleId;
                    activity.IsSubmission = lmsActivity.IsSubmission;
                    // activity.LmsActivityTypeId = lmsActivity.LmsActivityTypeId;

                    // _context.Update(lmsActivity);
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
                return RedirectToAction(nameof(CreateLmsActivity), new { Id = retViewId });
            }
            return View(lmsActivity);


        }

        private bool LmsActivityExists(int id)
        {
            return _context.LmsActivity.Any(e => e.Id == id);
        }
        private List<SelectListItem> GetActivityList()
        {
            return _context.LmsActivityType.Select(a =>
                                            new SelectListItem
                                            {
                                                Value = a.Id.ToString(),
                                                Text = a.Name,

                                            }).ToList();
        }
    }
}