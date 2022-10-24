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
    public class StudentCoursesController : Controller
    {
        private readonly Student_SystemContext _context;

        public StudentCoursesController(Student_SystemContext context)
        {
            _context = context;
        }

        // GET: StudentCourses
        public async Task<IActionResult> Index()
        {
              return View(await _context.StudentCourses.ToListAsync());
        }

        // GET: StudentCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentCourses == null)
            {
                return NotFound();
            }

            var studentCourses = await _context.StudentCourses
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentCourses == null)
            {
                return NotFound();
            }

            return View(studentCourses);
        }

        // GET: StudentCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId")] StudentCourses studentCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentCourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentCourses);
        }

        // GET: StudentCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentCourses == null)
            {
                return NotFound();
            }

            var studentCourses = await _context.StudentCourses.FindAsync(id);
            if (studentCourses == null)
            {
                return NotFound();
            }
            return View(studentCourses);
        }

        // POST: StudentCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,CourseId")] StudentCourses studentCourses)
        {
            if (id != studentCourses.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentCourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCoursesExists(studentCourses.StudentId))
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
            return View(studentCourses);
        }

        // GET: StudentCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentCourses == null)
            {
                return NotFound();
            }

            var studentCourses = await _context.StudentCourses
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentCourses == null)
            {
                return NotFound();
            }

            return View(studentCourses);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentCourses == null)
            {
                return Problem("Entity set 'Student_SystemContext.StudentCourses'  is null.");
            }
            var studentCourses = await _context.StudentCourses.FindAsync(id);
            if (studentCourses != null)
            {
                _context.StudentCourses.Remove(studentCourses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentCoursesExists(int id)
        {
          return _context.StudentCourses.Any(e => e.StudentId == id);
        }
    }
}
