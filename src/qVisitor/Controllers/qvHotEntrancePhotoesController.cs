using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qVisitor.Data;
using qVisitor.Models;

namespace qVisitor.Controllers
{
    public class qvHotEntrancePhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvHotEntrancePhotoesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvHotEntrancePhotoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HotEntrancePhotos.Include(q => q.HotEntrance);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: qvHotEntrancePhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrancePhoto == null)
            {
                return NotFound();
            }

            return View(qvHotEntrancePhoto);
        }

        // GET: qvHotEntrancePhotoes/Create
        public IActionResult Create()
        {
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id");
            return View();
        }

        // POST: qvHotEntrancePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HotEntranceId,Photo")] qvHotEntrancePhoto qvHotEntrancePhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvHotEntrancePhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntrancePhoto.HotEntranceId);
            return View(qvHotEntrancePhoto);
        }

        // GET: qvHotEntrancePhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrancePhoto == null)
            {
                return NotFound();
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntrancePhoto.HotEntranceId);
            return View(qvHotEntrancePhoto);
        }

        // POST: qvHotEntrancePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HotEntranceId,Photo")] qvHotEntrancePhoto qvHotEntrancePhoto)
        {
            if (id != qvHotEntrancePhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvHotEntrancePhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvHotEntrancePhotoExists(qvHotEntrancePhoto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntrancePhoto.HotEntranceId);
            return View(qvHotEntrancePhoto);
        }

        // GET: qvHotEntrancePhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrancePhoto == null)
            {
                return NotFound();
            }

            return View(qvHotEntrancePhoto);
        }

        // POST: qvHotEntrancePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            _context.HotEntrancePhotos.Remove(qvHotEntrancePhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvHotEntrancePhotoExists(int id)
        {
            return _context.HotEntrancePhotos.Any(e => e.Id == id);
        }
    }
}
