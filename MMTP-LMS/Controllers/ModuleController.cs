using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using MMTP_LMS.Models;
using MMTP_LMS.Utilities;
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class ModuleController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        private DateUtilities dateUtilities;
        private static int retViewId;
        public ModuleController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
            dateUtilities = new DateUtilities();
        }

        // GET: Modules
        public IActionResult Index()
        {
            var model = new ModuleViewModel()
            {
                Modules = _context.Module.ToList()
            };
            return View(model);
        }

        // GET: Module/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreateModule
        public async Task<ActionResult> CreateModule(int? id = null)
        {
            //ToDo: Fix 
            if (id == null) return NotFound();
            
            var course = await _context.Course.Include(m => m.Modules).FirstOrDefaultAsync(c => c.Id == id);
            retViewId = course.Id;
            
            if (course == null) return NotFound();
            
            var model = new ModuleViewModel()
            {
                CourseId = (int)id,
                ModuleStartDate = dateUtilities.GetModuleStartDate(_context,id),
                ModuleEndDate = dateUtilities.GetModuleEndDate(_context, id)


            };
            if (course.Modules.Count() > 0) model.Modules = course.Modules;
            else model.Modules = new List<Module>();

            return View(model);
        }

        // POST: CreateModule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModule([Bind("Id,ModuleName,ModuleDescription,ModuleStartDate,ModuleEndDate,CourseId")] ModuleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var module = new Module()
                {
                    Name = viewModel.ModuleName,
                    StartDate = viewModel.ModuleStartDate,
                    EndDate = viewModel.ModuleEndDate,
                    Description = viewModel.ModuleDescription,
                    CourseId = viewModel.CourseId

                };
                _context.Module.Add(module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateModule), new { id = module.CourseId });
            }
            return View(viewModel);
        }
        
        // GET: Module/EditModule/
        public async Task<IActionResult> EditModule(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Module.FindAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            return View(module);
        }

        // POST: Module/EditModule/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModule(int id, Module module)
        {
            if (id != module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var m = new Module();
                    m.Id = module.Id;
                    _context.Attach(m);
                    m.Name = module.Name;
                    m.Description = module.Description;
                    m.StartDate = module.StartDate;
                    m.EndDate = module.EndDate;

                  //  _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CreateModule), new { Id = retViewId } );
            }
            return View(module);
        }
        private bool ModuleExists(int id)
        {
            return _context.Module.Any(e => e.Id == id);
        }


        // GET: Module/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Module/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}