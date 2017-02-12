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
    public class qvOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvOrdersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(q => q.OrderStatus).Include(q => q.OrderType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: qvOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrder = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrder == null)
            {
                return NotFound();
            }

            return View(qvOrder);
        }

        // GET: qvOrders/Create
        public IActionResult Create()
        {
            ViewData["OrderStausid"] = new SelectList(_context.OrderStatuses, "Id", "Id");
            ViewData["OrderTypeid"] = new SelectList(_context.OrderTypes, "Id", "Id");
            return View();
        }

        // POST: qvOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CloseTime,EndDate,OpenTime,OrderStausid,OrderTypeid,StartDate")] qvOrder qvOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OrderStausid"] = new SelectList(_context.OrderStatuses, "Id", "Id", qvOrder.OrderStausid);
            ViewData["OrderTypeid"] = new SelectList(_context.OrderTypes, "Id", "Id", qvOrder.OrderTypeid);
            return View(qvOrder);
        }

        // GET: qvOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrder = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrder == null)
            {
                return NotFound();
            }
            ViewData["OrderStausid"] = new SelectList(_context.OrderStatuses, "Id", "Id", qvOrder.OrderStausid);
            ViewData["OrderTypeid"] = new SelectList(_context.OrderTypes, "Id", "Id", qvOrder.OrderTypeid);
            return View(qvOrder);
        }

        // POST: qvOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CloseTime,EndDate,OpenTime,OrderStausid,OrderTypeid,StartDate")] qvOrder qvOrder)
        {
            if (id != qvOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvOrderExists(qvOrder.Id))
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
            ViewData["OrderStausid"] = new SelectList(_context.OrderStatuses, "Id", "Id", qvOrder.OrderStausid);
            ViewData["OrderTypeid"] = new SelectList(_context.OrderTypes, "Id", "Id", qvOrder.OrderTypeid);
            return View(qvOrder);
        }

        // GET: qvOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrder = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrder == null)
            {
                return NotFound();
            }

            return View(qvOrder);
        }

        // POST: qvOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvOrder = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Orders.Remove(qvOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvOrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
