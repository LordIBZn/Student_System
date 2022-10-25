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
    public class HomeworkController : Controller
    {
        private readonly Student_SystemContext _context;

        public HomeworkController(Student_SystemContext context)
        {
            _context = context;
        }

        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var StudentHomework = await _context.Homework
                .Include(sh => sh.Students)
                .ToListAsync();

              return View(StudentHomework);
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Homework == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create()
        {
            var Students = _context.Students.ToList();
            ViewData["Students"] = new SelectList(Students, "Id", "Name");
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,ContentType,SubmissionDate,Students,StudentsId")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homework);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var Students = _context.Students.ToList();
            ViewData["Students"] = new SelectList(Students, "Id", "Name");

            if (id == null || _context.Homework == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,ContentType,SubmissionDate,Students,StudentsId")] Homework homework)
        {
            if (id != homework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.Id))
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
            return View(homework);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Homework == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Homework == null)
            {
                return Problem("Entity set 'Student_SystemContext.Homework'  is null.");
            }
            var homework = await _context.Homework.FindAsync(id);
            if (homework != null)
            {
                _context.Homework.Remove(homework);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(int id)
        {
          return _context.Homework.Any(e => e.Id == id);
        }
    }
}
