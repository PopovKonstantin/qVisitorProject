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
    public class qvVisitiorPhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitiorPhotoesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvVisitiorPhotoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VisitorPhotos.Include(q => q.Visitor);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        [Route("Visitors/Photoes/Details/{id}")]
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
        [Route("Visitors/Photoes/Create")]
        // GET: qvVisitiorPhotoes/Create
        public IActionResult Create()
        {
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id");
            return View();
        }

        // POST: qvVisitiorPhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Photo,PhotoDate,VisitorId")] qvVisitiorPhoto qvVisitiorPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvVisitiorPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitiorPhoto.VisitorId);
            return View(qvVisitiorPhoto);
        }
        [Route("Visitors/Photoes/Edit/{id}")]
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
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitiorPhoto.VisitorId);
            return View(qvVisitiorPhoto);
        }

        // POST: qvVisitiorPhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Photo,PhotoDate,VisitorId")] qvVisitiorPhoto qvVisitiorPhoto)
        {
            if (id != qvVisitiorPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction("Index");
            }
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitiorPhoto.VisitorId);
            return View(qvVisitiorPhoto);
        }
        [Route("Visitors/Photoes/Delete/{id}")]
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
            return RedirectToAction("Index");
        }

        private bool qvVisitiorPhotoExists(int id)
        {
            return _context.VisitorPhotos.Any(e => e.Id == id);
        }
    }
}
