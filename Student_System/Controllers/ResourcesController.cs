using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_System.Data;
using Student_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Student_System.Controllers
{
    [Authorize]
    public class ResourcesController : Controller
    {
        private readonly Student_SystemContext _context;

        public ResourcesController(Student_SystemContext context)
        {
            _context = context;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            var CourseResources = await _context.Resources
                .Include(rc => rc.Course)
                .OrderBy(rc => rc.Course.StartDate)
                .ThenByDescending(rc => rc.Course.EndDate)
                .ToListAsync();
              return View(CourseResources);
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resources == null)
            {
                return NotFound();
            }

            var resources = await _context.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resources == null)
            {
                return NotFound();
            }

            return View(resources);
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
            var Courses = _context.Courses.ToList();
            ViewData["Courses"] = new SelectList(Courses, "Id", "Name");
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,typeOfResource,Url,Courses,CourseId")] Resources resources)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resources);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resources);
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var Courses = _context.Courses.ToList();
            ViewData["Courses"] = new SelectList(Courses, "Id", "Name");

            if (id == null || _context.Resources == null)
            {
                return NotFound();
            }

            var resources = await _context.Resources.FindAsync(id);
            if (resources == null)
            {
                return NotFound();
            }
            return View(resources);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,typeOfResource,Url,Courses,CourseId")] Resources resources)
        {
            if (id != resources.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourcesExists(resources.Id))
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
            return View(resources);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resources == null)
            {
                return NotFound();
            }

            var resources = await _context.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resources == null)
            {
                return NotFound();
            }

            return View(resources);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resources == null)
            {
                return Problem("Entity set 'Student_SystemContext.Resources'  is null.");
            }
            var resources = await _context.Resources.FindAsync(id);
            if (resources != null)
            {
                _context.Resources.Remove(resources);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourcesExists(int id)
        {
          return _context.Resources.Any(e => e.Id == id);
        }
    }
}
