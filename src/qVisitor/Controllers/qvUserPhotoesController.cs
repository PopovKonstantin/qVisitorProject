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
    public class qvUserPhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvUserPhotoesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvUserPhotoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserPhotos.ToListAsync());
        }

        // GET: qvUserPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvUserPhoto = await _context.UserPhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvUserPhoto == null)
            {
                return NotFound();
            }

            return View(qvUserPhoto);
        }

        // GET: qvUserPhotoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvUserPhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Photo,PhotoDate")] qvUserPhoto qvUserPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvUserPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvUserPhoto);
        }

        // GET: qvUserPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvUserPhoto = await _context.UserPhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvUserPhoto == null)
            {
                return NotFound();
            }
            return View(qvUserPhoto);
        }

        // POST: qvUserPhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Photo,PhotoDate")] qvUserPhoto qvUserPhoto)
        {
            if (id != qvUserPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvUserPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvUserPhotoExists(qvUserPhoto.Id))
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
            return View(qvUserPhoto);
        }

        // GET: qvUserPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvUserPhoto = await _context.UserPhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvUserPhoto == null)
            {
                return NotFound();
            }

            return View(qvUserPhoto);
        }

        // POST: qvUserPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvUserPhoto = await _context.UserPhotos.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserPhotos.Remove(qvUserPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvUserPhotoExists(int id)
        {
            return _context.UserPhotos.Any(e => e.Id == id);
        }
    }
}
