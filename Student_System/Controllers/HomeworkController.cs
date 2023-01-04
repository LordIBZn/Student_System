using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_System.Data;
using Student_System.Migrations;
using Student_System.Models;
using Student_System.Models.ViewModels;

namespace Student_System.Controllers
{
    [Authorize(Roles = "Admin,Student")]
    public class HomeworkController : Controller
    {
        private readonly Student_SystemContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeworkController(Student_SystemContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public string UploadFile(Homework homework)
        {
            string fileName = null;

            if (homework.File != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                fileName = Path.GetFileNameWithoutExtension(homework.File.FileName);
                string extension = Path.GetExtension(homework.File.FileName);
                string path = Path.Combine(wwwRootPath + "/files/",fileName + extension);
                homework.FileName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    homework.File.CopyTo(filestream);
                }

            }
            return fileName;
        }


        public string GetPath(Homework homework)
        {
            
            string fileName = null;
            string FullPath = null;
            string RelativePath = null;
            if (homework.File != null)
            {
                
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                fileName = Path.GetFileNameWithoutExtension(homework.File.FileName);
                string extension = Path.GetExtension(homework.File.FileName);
                string path = Path.Combine(wwwRootPath + "/files/", fileName);
                homework.FileName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                FullPath = path + extension;
                RelativePath = Path.Combine(fileName);
                
                //using (var filestream = new FileStream(path, FileMode.Create))
                //{
                //    homework.File.CopyTo(filestream);
                //}
            }
            return  RelativePath;

        }

        [HttpPost]
        public FileStreamResult DowloadsFile(int Id)
        {
            var homework = _context.Homework
                .Where(i => i.Id == Id)
                .FirstOrDefault();

            string fileName = homework.FileName;
            string fileNameNotnumber = Regex.Replace(fileName, @"[\d-]", string.Empty);
            string fileNameNotExtension = Path.ChangeExtension(fileNameNotnumber, null);
            string extension = Path.GetExtension(fileName);

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/files/" + fileNameNotExtension + extension);
            var stream = new MemoryStream(System.IO.File.ReadAllBytes(path));

            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = fileNameNotExtension + extension
            };

        }

        public VirtualFileResult GetVirtualFile(int id)
        {
            var homework = _context.Homework
                .Where(i => i.Id == id)
                .FirstOrDefault();

            string fileName = homework.FileName;
            string fileNameNotnumber = Regex.Replace(fileName, @"[\d-]", string.Empty);
            string fileNameNotExtension = Path.ChangeExtension(fileNameNotnumber, null);
            string extension = Path.GetExtension(fileName);

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/files/" + fileNameNotExtension + extension);

            string Vpath = Path.Combine("~/files/", fileNameNotnumber);

            return new VirtualFileResult(Vpath, "application/octet-stream");
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

        [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Create([Bind("Id,Content,ContentType,SubmissionDate,Students,StudentsId,File,Path")] Homework homework)
        {
            
            if (ModelState.IsValid)
            {
                //save file to wwwroot/files
                homework.FileName = UploadFile(homework);
                homework.Path = GetPath(homework);
               
                //Insert record
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
                //if (homework.File != null)
                //{
                //    string folder = "files";
                //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath,folder);
                //    folder += homework.File.Name + Guid.NewGuid().ToString();

                //    await homework.File.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                //} 
            }
            return View(homework);
        }

        [Authorize(Roles = "Admin,Student")]
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,ContentType,SubmissionDate,Students,StudentsId,File,Path")] HomeworkViewModel homework)
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

        [Authorize(Roles = "Admin")]
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
