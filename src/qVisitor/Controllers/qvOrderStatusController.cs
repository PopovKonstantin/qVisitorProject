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
    public class qvOrderStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvOrderStatusController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvOrderStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderStatuses.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvOrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderStatus = await _context.OrderStatuses.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderStatus == null)
            {
                return NotFound();
            }

            return View(qvOrderStatus);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvOrderStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvOrderStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Description")] qvOrderStatus qvOrderStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvOrderStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvOrderStatus);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvOrderStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderStatus = await _context.OrderStatuses.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderStatus == null)
            {
                return NotFound();
            }
            return View(qvOrderStatus);
        }

        // POST: qvOrderStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Description")] qvOrderStatus qvOrderStatus)
        {
            if (id != qvOrderStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvOrderStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvOrderStatusExists(qvOrderStatus.Id))
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
            return View(qvOrderStatus);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvOrderStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderStatus = await _context.OrderStatuses.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderStatus == null)
            {
                return NotFound();
            }

            return View(qvOrderStatus);
        }

        // POST: qvOrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvOrderStatus = await _context.OrderStatuses.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderStatuses.Remove(qvOrderStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvOrderStatusExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.Id == id);
        }
    }
}
