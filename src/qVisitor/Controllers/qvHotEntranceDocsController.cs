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
    public class qvHotEntranceDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvHotEntranceDocsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvHotEntranceDocs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.qvHotEntranceDoc.Include(q => q.HotEntrance);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: qvHotEntranceDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntranceDoc == null)
            {
                return NotFound();
            }

            return View(qvHotEntranceDoc);
        }

        // GET: qvHotEntranceDocs/Create
        public IActionResult Create()
        {
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id");
            return View();
        }

        // POST: qvHotEntranceDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Document,HotEntranceId")] qvHotEntranceDoc qvHotEntranceDoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvHotEntranceDoc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntranceDoc.HotEntranceId);
            return View(qvHotEntranceDoc);
        }

        // GET: qvHotEntranceDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntranceDoc == null)
            {
                return NotFound();
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntranceDoc.HotEntranceId);
            return View(qvHotEntranceDoc);
        }

        // POST: qvHotEntranceDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Document,HotEntranceId")] qvHotEntranceDoc qvHotEntranceDoc)
        {
            if (id != qvHotEntranceDoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvHotEntranceDoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvHotEntranceDocExists(qvHotEntranceDoc.Id))
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
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntranceDoc.HotEntranceId);
            return View(qvHotEntranceDoc);
        }

        // GET: qvHotEntranceDocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntranceDoc == null)
            {
                return NotFound();
            }

            return View(qvHotEntranceDoc);
        }

        // POST: qvHotEntranceDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.SingleOrDefaultAsync(m => m.Id == id);
            _context.qvHotEntranceDoc.Remove(qvHotEntranceDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvHotEntranceDocExists(int id)
        {
            return _context.qvHotEntranceDoc.Any(e => e.Id == id);
        }
    }
}
