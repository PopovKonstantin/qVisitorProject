using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qVisitor.Data;
using qVisitor.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace qVisitor.Controllers
{
    public class qvVisitiorPhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitiorPhotoesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitiorPhotoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VisitorPhotos.Include(q => q.Visitor);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitiorPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitiorPhoto = await _context.VisitorPhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitiorPhoto == null)
            {
                return NotFound();
            }

            return View(qvVisitiorPhoto);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitiorPhotoes/Create
        public IActionResult Create(int? reffid)
        {
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors select
                           new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName",reffid);
            ViewData["Reff"] = reffid;
            return View();
        }
        
        // POST: qvVisitiorPhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhotoDate,VisitorId")] qvVisitiorPhoto qvVisitiorPhoto, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(Photo.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)Photo.Length);
                    }
                    // установка массива байтов
                    qvVisitiorPhoto.Photo = imageData;
                }

                _context.Add(qvVisitiorPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvVisitors", new { id = qvVisitiorPhoto.VisitorId});
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors select
                           new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitiorPhoto.VisitorId);
            return View(qvVisitiorPhoto);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitiorPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitiorPhoto = await _context.VisitorPhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitiorPhoto == null)
            {
                return NotFound();
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors select
                           new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitiorPhoto.VisitorId);
            return View(qvVisitiorPhoto);
        }

        // POST: qvVisitiorPhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhotoDate,VisitorId")] qvVisitiorPhoto qvVisitiorPhoto, IFormFile Photo)
        {
            if (id != qvVisitiorPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Photo != null)
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(Photo.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)Photo.Length);
                        }
                        // установка массива байтов
                        qvVisitiorPhoto.Photo = imageData;
                    }
                    _context.Update(qvVisitiorPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvVisitiorPhotoExists(qvVisitiorPhoto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvVisitors", new { id = qvVisitiorPhoto.VisitorId });
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors select
                                        new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitiorPhoto.VisitorId);
            return View(qvVisitiorPhoto);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitiorPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitiorPhoto = await _context.VisitorPhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitiorPhoto == null)
            {
                return NotFound();
            }

            return View(qvVisitiorPhoto);
        }

        // POST: qvVisitiorPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvVisitiorPhoto = await _context.VisitorPhotos.SingleOrDefaultAsync(m => m.Id == id);
            _context.VisitorPhotos.Remove(qvVisitiorPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvVisitors", new { id = qvVisitiorPhoto.VisitorId });
        }

        private bool qvVisitiorPhotoExists(int id)
        {
            return _context.VisitorPhotos.Any(e => e.Id == id);
        }
    }
}
