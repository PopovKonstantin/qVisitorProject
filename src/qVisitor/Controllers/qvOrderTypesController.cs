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
    public class qvOrderTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvOrderTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvOrderTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderTypes.ToListAsync());
        }

        // GET: qvOrderTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderType = await _context.OrderTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderType == null)
            {
                return NotFound();
            }

            return View(qvOrderType);
        }

        // GET: qvOrderTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvOrderTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Description")] qvOrderType qvOrderType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvOrderType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvOrderType);
        }

        // GET: qvOrderTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderType = await _context.OrderTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderType == null)
            {
                return NotFound();
            }
            return View(qvOrderType);
        }

        // POST: qvOrderTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Description")] qvOrderType qvOrderType)
        {
            if (id != qvOrderType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvOrderType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvOrderTypeExists(qvOrderType.Id))
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
            return View(qvOrderType);
        }

        // GET: qvOrderTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderType = await _context.OrderTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderType == null)
            {
                return NotFound();
            }

            return View(qvOrderType);
        }

        // POST: qvOrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvOrderType = await _context.OrderTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderTypes.Remove(qvOrderType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvOrderTypeExists(int id)
        {
            return _context.OrderTypes.Any(e => e.Id == id);
        }
    }
}
