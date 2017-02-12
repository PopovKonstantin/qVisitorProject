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
    public class qvEntranceDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvEntranceDocsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvEntranceDocs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EntranceDocs.Include(q => q.Entrance);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: qvEntranceDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntranceDoc = await _context.EntranceDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntranceDoc == null)
            {
                return NotFound();
            }

            return View(qvEntranceDoc);
        }

        // GET: qvEntranceDocs/Create
        public IActionResult Create()
        {
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id");
            return View();
        }

        // POST: qvEntranceDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EntranceId,Scan")] qvEntranceDoc qvEntranceDoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvEntranceDoc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id", qvEntranceDoc.EntranceId);
            return View(qvEntranceDoc);
        }

        // GET: qvEntranceDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntranceDoc = await _context.EntranceDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntranceDoc == null)
            {
                return NotFound();
            }
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id", qvEntranceDoc.EntranceId);
            return View(qvEntranceDoc);
        }

        // POST: qvEntranceDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EntranceId,Scan")] qvEntranceDoc qvEntranceDoc)
        {
            if (id != qvEntranceDoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvEntranceDoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvEntranceDocExists(qvEntranceDoc.Id))
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
            ViewData["EntranceId"] = new SelectList(_context.Entrances, "Id", "Id", qvEntranceDoc.EntranceId);
            return View(qvEntranceDoc);
        }

        // GET: qvEntranceDocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntranceDoc = await _context.EntranceDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntranceDoc == null)
            {
                return NotFound();
            }

            return View(qvEntranceDoc);
        }

        // POST: qvEntranceDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvEntranceDoc = await _context.EntranceDocs.SingleOrDefaultAsync(m => m.Id == id);
            _context.EntranceDocs.Remove(qvEntranceDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvEntranceDocExists(int id)
        {
            return _context.EntranceDocs.Any(e => e.Id == id);
        }
    }
}
