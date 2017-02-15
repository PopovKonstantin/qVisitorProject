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
    public class refOrderVisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public refOrderVisitorsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: refOrderVisitors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.refOrderVisitor.Include(r => r.Order).Include(r => r.Visitor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: refOrderVisitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refOrderVisitor = await _context.refOrderVisitor.SingleOrDefaultAsync(m => m.VisitorId == id);
            if (refOrderVisitor == null)
            {
                return NotFound();
            }

            return View(refOrderVisitor);
        }

        // GET: refOrderVisitors/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id");
            return View();
        }

        // POST: refOrderVisitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitorId,OrderId")] refOrderVisitor refOrderVisitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refOrderVisitor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", refOrderVisitor.OrderId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", refOrderVisitor.VisitorId);
            return View(refOrderVisitor);
        }

        // GET: refOrderVisitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refOrderVisitor = await _context.refOrderVisitor.SingleOrDefaultAsync(m => m.VisitorId == id);
            if (refOrderVisitor == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", refOrderVisitor.OrderId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", refOrderVisitor.VisitorId);
            return View(refOrderVisitor);
        }

        // POST: refOrderVisitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitorId,OrderId")] refOrderVisitor refOrderVisitor)
        {
            if (id != refOrderVisitor.VisitorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refOrderVisitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!refOrderVisitorExists(refOrderVisitor.VisitorId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", refOrderVisitor.OrderId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", refOrderVisitor.VisitorId);
            return View(refOrderVisitor);
        }

        // GET: refOrderVisitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refOrderVisitor = await _context.refOrderVisitor.SingleOrDefaultAsync(m => m.VisitorId == id);
            if (refOrderVisitor == null)
            {
                return NotFound();
            }

            return View(refOrderVisitor);
        }

        // POST: refOrderVisitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refOrderVisitor = await _context.refOrderVisitor.SingleOrDefaultAsync(m => m.VisitorId == id);
            _context.refOrderVisitor.Remove(refOrderVisitor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool refOrderVisitorExists(int id)
        {
            return _context.refOrderVisitor.Any(e => e.VisitorId == id);
        }
    }
}
