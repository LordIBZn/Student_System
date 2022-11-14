using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_System.Data;
using Student_System.Models;

namespace Student_System.Controllers
{
    [Authorize(Roles ="Admin")]
    public class StudentsController : Controller
    {
        private readonly Student_SystemContext _context;

        public StudentsController(Student_SystemContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        public async Task<IActionResult> StudentNumberCourse(int? StudentId)
        {
            var Stundent = _context.Students.ToList();
            ViewData["Stundent"] = new SelectList(Stundent, "Id", "Name");
            
            var StudentNumberCourse = await _context.Students
                .ToListAsync();
            
            if(StudentId >= 1)
            {
                StudentNumberCourse = await _context.Students
                .Where(snc => snc.Id == StudentId)
                .ToListAsync();
            }

            var Students = new List<StudentNumberCourseViewModel>();
            StudentNumberCourse.ForEach(c =>
            {
                var StudentNumberVie = new StudentNumberCourseViewModel()
                {
                    StudentName = c.Name,
                    NumCourse = NumCourses(c.Id),
                    TotalPrice = TotalPrice(c.Id),
                    AvaregePrice = AvaregePrice(c.Id)

                };
                Students.Add(StudentNumberVie);
            });
            var StudentsOrderBy = Students.OrderByDescending(c => c.TotalPrice)
                .ThenByDescending(c => c.NumCourse)
                .ThenByDescending(c => c.StudentName);

            return View(StudentsOrderBy);

        }

        public int NumCourses(int StudentId)
        {
            var NumCourses = _context.StudentCourses
                .Where(c => c.StudentId == StudentId)
                .Count();

            return NumCourses;
        }

        public int TotalPrice(int studentId)
        {
            var TotalPrice = _context.StudentCourses
                .Where(c => c.StudentId == studentId)
                .Select(c => c.Course)
                .Sum(c => c.price);
            
            return (int)(float)TotalPrice;
        }

        public int AvaregePrice(int StudentId)
        {
            var AvaregePrice = _context.StudentCourses
                .Where(c => c.StudentId == StudentId)
                .Select(c => c.Course)
                .Average(c => c.price);
                

            return (int)(float)AvaregePrice;
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,RegitrationDate,BirthDay,Email")] Students students)
        {
            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,RegitrationDate,BirthDay,Email")] Students students)
        {
            if (id != students.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.Id))
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
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'Student_SystemContext.Students'  is null.");
            }
            var students = await _context.Students.FindAsync(id);
            if (students != null)
            {
                _context.Students.Remove(students);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
