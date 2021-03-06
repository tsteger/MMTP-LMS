﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MMTP_LMS.Data;
using MMTP_LMS.Models;

namespace MMTP_LMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> userManager;

        public PeopleController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: LmsActivities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LmsActivity;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,CourseId,Password")] Person person)
        {
            if (ModelState.IsValid)
            {
                var store = new UserStore<Person>(_context);
                await userManager.UpdateAsync(person);
                await store.Context.SaveChangesAsync();
                //_context.Add(person);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", person.CourseId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", person.CourseId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email,CourseId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(person.Id);
                    user.FirstName = person.FirstName;
                    user.LastName = person.LastName;
                    user.CourseId = person.CourseId;
                    await userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException e)
                {
                    new Exception(e.Message);
                }

            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", person.CourseId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
