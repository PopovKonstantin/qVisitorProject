using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qVisitor.Data;
using qVisitor.Models;
using Microsoft.AspNetCore.Authorization;

namespace qVisitor.Controllers
{
    public class qvEntranceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvEntranceTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntranceTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EntranceTypes.ToListAsync());
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntranceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntranceType = await _context.EntranceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntranceType == null)
            {
                return NotFound();
            }

            return View(qvEntranceType);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntranceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvEntranceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Description")] qvEntranceType qvEntranceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvEntranceType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvEntranceType);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntranceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntranceType = await _context.EntranceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntranceType == null)
            {
                return NotFound();
            }
            return View(qvEntranceType);
        }

        // POST: qvEntranceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Description")] qvEntranceType qvEntranceType)
        {
            if (id != qvEntranceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvEntranceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvEntranceTypeExists(qvEntranceType.Id))
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
            return View(qvEntranceType);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntranceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntranceType = await _context.EntranceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntranceType == null)
            {
                return NotFound();
            }

            return View(qvEntranceType);
        }

        // POST: qvEntranceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvEntranceType = await _context.EntranceTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.EntranceTypes.Remove(qvEntranceType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvEntranceTypeExists(int id)
        {
            return _context.EntranceTypes.Any(e => e.Id == id);
        }
    }
}
