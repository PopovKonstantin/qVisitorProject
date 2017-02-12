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
    public class qvVisitorScansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitorScansController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvVisitorScans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VisitorScan.Include(q => q.Visitor);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        [Route("Visitors/Scans/Details/{id}")]
        // GET: qvVisitorScans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorScan == null)
            {
                return NotFound();
            }

            return View(qvVisitorScan);
        }
        [Route("Visitors/Scans/Create")]
        // GET: qvVisitorScans/Create
        public IActionResult Create()
        {
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id");
            return View();
        }

        // POST: qvVisitorScans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Scan,VisitorId")] qvVisitorScan qvVisitorScan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvVisitorScan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitorScan.VisitorId);
            return View(qvVisitorScan);
        }
        [Route("Visitors/Scans/Edit/{id}")]
        // GET: qvVisitorScans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorScan == null)
            {
                return NotFound();
            }
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitorScan.VisitorId);
            return View(qvVisitorScan);
        }

        // POST: qvVisitorScans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Scan,VisitorId")] qvVisitorScan qvVisitorScan)
        {
            if (id != qvVisitorScan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvVisitorScan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvVisitorScanExists(qvVisitorScan.Id))
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
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitorScan.VisitorId);
            return View(qvVisitorScan);
        }
        [Route("Visitors/Scans/Delete/{id}")]
        // GET: qvVisitorScans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorScan == null)
            {
                return NotFound();
            }

            return View(qvVisitorScan);
        }

        // POST: qvVisitorScans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            _context.VisitorScan.Remove(qvVisitorScan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvVisitorScanExists(int id)
        {
            return _context.VisitorScan.Any(e => e.Id == id);
        }
    }
}
