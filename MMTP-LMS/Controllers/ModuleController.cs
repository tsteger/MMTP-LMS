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
using MMTP_LMS.ViewModels;

namespace MMTP_LMS.Controllers
{
    public class ModuleController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;

        public ModuleController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Module
        public IActionResult Index()
        {
            var model = new ModuleViewModel()
            {
                Modules = _context.Module.ToList()
            };
            return View();
        }

        // GET: Module/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreateModule
        public async Task<ActionResult> CreateModule(int? id = null)
        {
            if (id == null) return View();

            var course = await _context.Course.Include(m => m.Modules).FirstOrDefaultAsync(c => c.Id == id);
           

            if (course == null) return NotFound();
            
            var model = new ModuleViewModel()
            {
                CourseId = (int)id,
               
                
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
        
        // GET: Module/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Module/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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