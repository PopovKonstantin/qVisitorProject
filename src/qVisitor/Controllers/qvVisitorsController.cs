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
    public class qvVisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitorsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvVisitors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Visitors.ToListAsync());
        }

        // GET: qvVisitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitor = await _context.Visitors.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitor == null)
            {
                return NotFound();
            }

            return View(qvVisitor);
        }

        // GET: qvVisitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvVisitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gender,birthdate,name,patronymic,surname")] qvVisitor qvVisitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvVisitor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvVisitor);
        }

        // GET: qvVisitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitor = await _context.Visitors.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitor == null)
            {
                return NotFound();
            }
            return View(qvVisitor);
        }

        // POST: qvVisitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Gender,birthdate,name,patronymic,surname")] qvVisitor qvVisitor)
        {
            if (id != qvVisitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvVisitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvVisitorExists(qvVisitor.Id))
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
            return View(qvVisitor);
        }

        // GET: qvVisitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitor = await _context.Visitors.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitor == null)
            {
                return NotFound();
            }

            return View(qvVisitor);
        }

        // POST: qvVisitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvVisitor = await _context.Visitors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Visitors.Remove(qvVisitor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvVisitorExists(int id)
        {
            return _context.Visitors.Any(e => e.Id == id);
        }
    }
}
