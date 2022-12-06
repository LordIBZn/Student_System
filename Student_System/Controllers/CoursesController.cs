using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_System.Data;
using Student_System.Models;

namespace Student_System.Controllers
{
    [Authorize(Roles ="Admin,Student")]
    public class CoursesController : Controller
    {
        private readonly Student_SystemContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AspNetUsers> _userManager;

        public CoursesController(Student_SystemContext context, RoleManager<IdentityRole> roleManager, UserManager<AspNetUsers> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var ResourceCourses = await _context.Courses
                    .Include(rc => rc.Resources)
                    .OrderBy(rc => rc.StartDate)
                    .ThenByDescending(rc => rc.EndDate)
                    .ToListAsync();

                return View(ResourceCourses);
            }
            else
            {
                return View("error");       
            }

        }

        [HttpGet]
        [Route("Courses/GetCoursesByStudentId")]

        public async Task<IActionResult> GetCoursesByStudentId()
        {
            //var user = await _userManager.GetUserIdAsync();

            //var CourseStudent = await _context.Courses
            //.Include(rc => rc.Resources)
            //.OrderBy(rc => rc.StartDate)
            //.ThenByDescending(rc => rc.EndDate);
                /*FirstOrDefaultAsync(rc => rc.Id == StudentId);*/


            return View();

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResourceMore()
        {

            var ResourceCount = await _context.Courses
               .Include(rc => rc.Resources)
               .Where(rc => rc.Resources.Count() > 5)
               .OrderByDescending(rc => rc.StartDate)
               .OrderByDescending(rc => rc.Resources.Count()).ToListAsync();

            return View(ResourceCount);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllCoursesonGivenDate(DateTime? EndDate)
        {
            var AllCoursesonGivenDate = await _context.Courses
                .Where(acg => acg.EndDate <= EndDate)
                .ToListAsync();

            var courses = new List<CoursesViewModel>();

            AllCoursesonGivenDate.ForEach(C =>
            {
                var CourseWiew = new CoursesViewModel()
                {
                    CourseName = C.Name,
                    StarDate = C.StartDate,
                    EndDate = C.EndDate,
                    CourseDuration = C.EndDate.Subtract(C.StartDate),
                    StudentCount = NumStudents(C.Id)

                };
                courses.Add(CourseWiew);
            });

            var CoursesOrderBy = courses.OrderByDescending(c => c.StudentCount)
                .ThenByDescending(c => c.CourseDuration);

            return View(CoursesOrderBy);
        }

        public int NumStudents(int CurseId)
        {
            var NumStudent = _context.StudentCourses
                .Where(c => c.CourseId == CurseId)
                .Count();

            return NumStudent;
        }


        [Authorize(Roles ="Admin")]

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

        [Authorize(Roles ="Admin")]

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

        [Authorize(Roles ="Admin")]
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
