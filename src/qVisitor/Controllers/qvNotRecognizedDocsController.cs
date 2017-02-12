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
    public class qvNotRecognizedDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvNotRecognizedDocsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvNotRecognizedDocs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NotRecognizedDocs.Include(q => q.CheckPoint);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: qvNotRecognizedDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvNotRecognizedDoc == null)
            {
                return NotFound();
            }

            return View(qvNotRecognizedDoc);
        }

        // GET: qvNotRecognizedDocs/Create
        public IActionResult Create()
        {
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id");
            return View();
        }

        // POST: qvNotRecognizedDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CheckPointId,Scan")] qvNotRecognizedDoc qvNotRecognizedDoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvNotRecognizedDoc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvNotRecognizedDoc.CheckPointId);
            return View(qvNotRecognizedDoc);
        }

        // GET: qvNotRecognizedDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvNotRecognizedDoc == null)
            {
                return NotFound();
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvNotRecognizedDoc.CheckPointId);
            return View(qvNotRecognizedDoc);
        }

        // POST: qvNotRecognizedDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckPointId,Scan")] qvNotRecognizedDoc qvNotRecognizedDoc)
        {
            if (id != qvNotRecognizedDoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvNotRecognizedDoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvNotRecognizedDocExists(qvNotRecognizedDoc.Id))
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
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvNotRecognizedDoc.CheckPointId);
            return View(qvNotRecognizedDoc);
        }

        // GET: qvNotRecognizedDocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvNotRecognizedDoc == null)
            {
                return NotFound();
            }

            return View(qvNotRecognizedDoc);
        }

        // POST: qvNotRecognizedDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.SingleOrDefaultAsync(m => m.Id == id);
            _context.NotRecognizedDocs.Remove(qvNotRecognizedDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvNotRecognizedDocExists(int id)
        {
            return _context.NotRecognizedDocs.Any(e => e.Id == id);
        }
    }
}
