using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using MMTP_LMS.Models;

namespace MMTP_LMS.Controllers
{
    public class LmsActivityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LmsActivityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LmsActivityTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LmsActivityType.ToListAsync());
        }

        // GET: LmsActivityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lmsActivityType = await _context.LmsActivityType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lmsActivityType == null)
            {
                return NotFound();
            }

            return View(lmsActivityType);
        }

        // GET: LmsActivityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LmsActivityTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] LmsActivityType lmsActivityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lmsActivityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lmsActivityType);
        }

        // GET: LmsActivityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lmsActivityType = await _context.LmsActivityType.FindAsync(id);
            if (lmsActivityType == null)
            {
                return NotFound();
            }
            return View(lmsActivityType);
        }

        // POST: LmsActivityTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] LmsActivityType lmsActivityType)
        {
            if (id != lmsActivityType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lmsActivityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LmsActivityTypeExists(lmsActivityType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lmsActivityType);
        }

        // GET: LmsActivityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lmsActivityType = await _context.LmsActivityType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lmsActivityType == null)
            {
                return NotFound();
            }

            return View(lmsActivityType);
        }

        // POST: LmsActivityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lmsActivityType = await _context.LmsActivityType.FindAsync(id);
            _context.LmsActivityType.Remove(lmsActivityType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LmsActivityTypeExists(int id)
        {
            return _context.LmsActivityType.Any(e => e.Id == id);
        }
    }
}
