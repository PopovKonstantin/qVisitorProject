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
    public class qvVisitorLuggagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitorLuggagesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvVisitorLuggages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VisitorLuggages.Include(q => q.Order).Include(q => q.Visitor);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        [Route("Visitors/Luggages/Details/{id}")]
        // GET: qvVisitorLuggages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorLuggage = await _context.VisitorLuggages.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorLuggage == null)
            {
                return NotFound();
            }

            return View(qvVisitorLuggage);
        }
        [Route("Visitors/Luggages/Create")]
        // GET: qvVisitorLuggages/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id");
            return View();
        }

        // POST: qvVisitorLuggages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Luggage,OrderId,VisitorId")] qvVisitorLuggage qvVisitorLuggage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvVisitorLuggage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvVisitorLuggage.OrderId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitorLuggage.VisitorId);
            return View(qvVisitorLuggage);
        }
        [Route("Visitors/Luggages/Edit/{id}")]
        // GET: qvVisitorLuggages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorLuggage = await _context.VisitorLuggages.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorLuggage == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvVisitorLuggage.OrderId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitorLuggage.VisitorId);
            return View(qvVisitorLuggage);
        }

        // POST: qvVisitorLuggages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Luggage,OrderId,VisitorId")] qvVisitorLuggage qvVisitorLuggage)
        {
            if (id != qvVisitorLuggage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvVisitorLuggage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvVisitorLuggageExists(qvVisitorLuggage.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvVisitorLuggage.OrderId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", qvVisitorLuggage.VisitorId);
            return View(qvVisitorLuggage);
        }
        [Route("Visitors/Luggages/Delete/{id}")]
        // GET: qvVisitorLuggages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorLuggage = await _context.VisitorLuggages.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorLuggage == null)
            {
                return NotFound();
            }

            return View(qvVisitorLuggage);
        }

        // POST: qvVisitorLuggages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvVisitorLuggage = await _context.VisitorLuggages.SingleOrDefaultAsync(m => m.Id == id);
            _context.VisitorLuggages.Remove(qvVisitorLuggage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvVisitorLuggageExists(int id)
        {
            return _context.VisitorLuggages.Any(e => e.Id == id);
        }
    }
}
