using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_System.Data;
using Student_System.Models;

namespace Student_System.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Student_SystemContext _context;

        public CoursesController(Student_SystemContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var ResourceCourses = await _context.Courses
                .Include(rc => rc.Resources)
                .OrderBy(rc => rc.StartDate)
                .ThenByDescending(rc => rc.EndDate)
                .ToListAsync();

            
            return View(ResourceCourses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var ResourceCourses = await _context.Courses
                .Include(rc => rc.Resources)
                .OrderBy(rc => rc.StartDate)
                .ThenByDescending(rc => rc.EndDate)
                .FirstOrDefaultAsync(rc => rc.Id == id);

            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(ResourceCourses);
        }

        public async Task <IActionResult> ResourceMore()
        {

            var ResourceCount = await _context.Courses
               .Include(rc => rc.Resources)
               .Where(rc => rc.Resources.Count() > 5)
               .OrderByDescending(rc => rc.StartDate)
               .OrderByDescending(rc => rc.Resources.Count()).ToListAsync();
               
            return View(ResourceCount);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            var Resources = _context.Courses.ToList();
            ViewData["Resources"] = new SelectList(Resources, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,price,Resources")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            var Resource = _context.Courses.ToList();
            ViewData["Resource"] = new SelectList(Resource, "Id", "Name");

            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,price,Resources")] Courses courses)
        {
            if (id != courses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesExists(courses.Id))
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
            return View(courses);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'Student_SystemContext.Courses'  is null.");
            }
            var courses = await _context.Courses.FindAsync(id);
            if (courses != null)
            {
                _context.Courses.Remove(courses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesExists(int id)
        {
          return _context.Courses.Any(e => e.Id == id);
        }
    }
}
