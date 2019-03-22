using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = "Admin")]
    public class LmsActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public List<SelectListItem> clist;
        private DateUtilities dateUtilities;
        private static int retViewId;
        public LmsActivitiesController(ApplicationDbContext context, UserManager<Person> userManager , IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
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
            ViewBag.CourseName = _context.Course.Where(i => i.Id == module.CourseId).Select(n => n.Name).FirstOrDefault(); 
            ViewBag.ModuleName = module.Name;
            ViewBag.ModuleId = module.CourseId;
            ViewBag.DocCount = _context.Document.Where(d => d.LmsActivityId != null && d.IsAdmin).Count();
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
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string txt, string activity_id)
        {

            if (!int.TryParse(activity_id, out int id)) return NoContent();

            var fileUtil = new Utilities.File();
            await fileUtil.UploadFileAsync(file, txt, _hostingEnvironment);
            var doc = new Document
            {
                Name = file.FileName,
                Description = "Activity: "+ _context.LmsActivity.Where(c => c.Id == id).Select(n => n.Name).FirstOrDefault(),
                TimeStamp = DateTime.Now,
                UserName = User.Identity.Name,
                Url = "\\Documents\\" + file.FileName,
                IsAdmin = true,
                LmsActivityId = id,
                PersonId = _context.Person.Where(p => p.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault()
            };

            _context.Document.Add(doc);
            _context.SaveChanges();
           
            return RedirectToAction("CreateLmsActivity", "LmsActivities", new { Id = retViewId });
        }

    }
}
