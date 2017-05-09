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
    public class qvEntrancePhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvEntrancePhotoesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrancePhotoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EntrancePhotos.Include(q => q.Entrance);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrancePhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntrancePhoto = await _context.EntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntrancePhoto == null)
            {
                return NotFound();
            }

            return View(qvEntrancePhoto);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrancePhotoes/Create
        public IActionResult Create(int? id)
        {
            var applicationDbContext = _context.Entrances.Include(q => q.CheckPoint).SingleOrDefault(m => m.Id == id);
            ViewData["KPPName"] = applicationDbContext.CheckPoint.Name;
            ViewData["CheckPointId"] = applicationDbContext.CheckPointId;
            ViewData["EntranceIdData"] = id;
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id",id);
            return View();
        }

        // POST: qvEntrancePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EntranceId")] qvEntrancePhoto qvEntrancePhoto, IFormFile Photo)
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
                    qvEntrancePhoto.Photo = imageData;
                }
                _context.Add(qvEntrancePhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvEntrances", new { id = qvEntrancePhoto.EntranceId });
            }
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id", qvEntrancePhoto.EntranceId);
            return View(qvEntrancePhoto);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrancePhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntrancePhoto = await _context.EntrancePhotos.Include(q => q.Entrance).ThenInclude(q => q.CheckPoint).SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntrancePhoto == null)
            {
                return NotFound();
            }
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id", qvEntrancePhoto.EntranceId);
            return View(qvEntrancePhoto);
        }

        // POST: qvEntrancePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EntranceId")] qvEntrancePhoto qvEntrancePhoto, IFormFile Photo)
        {
            if (id != qvEntrancePhoto.Id)
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
                        qvEntrancePhoto.Photo = imageData;
                    }
                    _context.Update(qvEntrancePhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvEntrancePhotoExists(qvEntrancePhoto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvEntrances", new { id = qvEntrancePhoto.EntranceId });
            }
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id", qvEntrancePhoto.EntranceId);
            return View(qvEntrancePhoto);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrancePhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntrancePhoto = await _context.EntrancePhotos.Include(q => q.Entrance).ThenInclude(q => q.CheckPoint).SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntrancePhoto == null)
            {
                return NotFound();
            }

            return View(qvEntrancePhoto);
        }

        // POST: qvEntrancePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvEntrancePhoto = await _context.EntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            _context.EntrancePhotos.Remove(qvEntrancePhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvEntrances", new { id = qvEntrancePhoto.EntranceId });
        }

        private bool qvEntrancePhotoExists(int id)
        {
            return _context.EntrancePhotos.Any(e => e.Id == id);
        }
    }
}
