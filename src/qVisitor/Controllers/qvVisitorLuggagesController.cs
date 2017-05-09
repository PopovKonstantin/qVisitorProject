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
    public class qvVisitorLuggagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitorLuggagesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorLuggages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VisitorLuggages.Include(q => q.Order).Include(q => q.Visitor);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
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

        // GET: qvVisitorLuggages/Create
        [Authorize(Roles = "Менеджер")]
        public IActionResult Create(int? ordid)
        {
            if (ordid == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", ordid);
            ViewData["VisitorId"] = new SelectList((from s in _context.refOrderVisitor.Include(q => q.Order).Include(q => q.Visitor)
                                                    where s.OrderId == ordid
                                                    select
new { Id = s.Visitor.Id, FullName = s.Visitor.surname + " " + s.Visitor.name + " " + s.Visitor.patronymic }), "Id", "FullName");

            ViewData["RefOrdId"] = ordid;
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
                return RedirectToAction("Details", "qvOrders", new { id = qvVisitorLuggage.OrderId });
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvVisitorLuggage.OrderId);
            ViewData["VisitorId"] = new SelectList((from s in _context.refOrderVisitor.Include(q => q.Order).Include(q => q.Visitor)
                                                    where s.OrderId == qvVisitorLuggage.OrderId
            select
new { Id = s.Visitor.Id, FullName = s.Visitor.surname + " " + s.Visitor.name + " " + s.Visitor.patronymic }), "Id", "FullName");
            ViewData["RefOrdId"] = qvVisitorLuggage.OrderId;
            ViewData["RefVisId"] = qvVisitorLuggage.VisitorId;
            return RedirectToAction("Create", "qvVisitorLuggages", new { visid = qvVisitorLuggage.VisitorId, ordid = qvVisitorLuggage.OrderId });
        }

        [Authorize(Roles = "Менеджер")]
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

            ViewData["VisitorId"] = new SelectList((from s in _context.refOrderVisitor.Include(q => q.Order).Include(q => q.Visitor)
                                                    where s.OrderId == qvVisitorLuggage.OrderId
                                                    select
           new { Id = s.Visitor.Id, FullName = s.Visitor.surname + " " + s.Visitor.name + " " + s.Visitor.patronymic }), "Id", "FullName");
            ViewData["RefOrdId"] = qvVisitorLuggage.OrderId;
            ViewData["RefVisId"] = qvVisitorLuggage.VisitorId;
            var cn = from c in _context.Visitors
                     where c.Id == qvVisitorLuggage.VisitorId
                     select new { FullName = c.surname + " " + c.name + " " + c.patronymic };
            ViewData["VisitorName"] = cn.Select(e => e.FullName).ToList()[0];
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
                return RedirectToAction("Details", "qvOrders", new { id = qvVisitorLuggage.OrderId });
            }
            ViewData["RefOrdId"] = qvVisitorLuggage.OrderId;
            ViewData["RefVisId"] = qvVisitorLuggage.VisitorId;
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvVisitorLuggage.OrderId);
            ViewData["VisitorId"] = new SelectList((from s in _context.refOrderVisitor.Include(q => q.Order).Include(q => q.Visitor)
                                                    where s.OrderId == qvVisitorLuggage.OrderId
                                                    select
            new { Id = s.Visitor.Id, FullName = s.Visitor.surname + " " + s.Visitor.name + " " + s.Visitor.patronymic }), "Id", "FullName");
            var cn = from c in _context.Visitors
                     where c.Id == qvVisitorLuggage.VisitorId
                     select new { FullName = c.surname + " " + c.name + " " + c.patronymic };
            ViewData["VisitorName"] = cn.Select(e => e.FullName).ToList()[0];
            return View(qvVisitorLuggage);
        }
        [Authorize(Roles = "Менеджер")]
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
            ViewData["RefOrdId"] = qvVisitorLuggage.OrderId;
            ViewData["RefVisId"] = qvVisitorLuggage.VisitorId;
            var cn = from c in _context.Visitors
                     where c.Id == qvVisitorLuggage.VisitorId
                     select new { FullName = c.surname + " " + c.name + " " + c.patronymic };
            ViewData["VisitorName"] = cn.Select(e => e.FullName).ToList()[0];
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
            return RedirectToAction("Details", "qvOrders", new { id = qvVisitorLuggage.OrderId });
        }

        private bool qvVisitorLuggageExists(int id)
        {
            return _context.VisitorLuggages.Any(e => e.Id == id);
        }
    }
}
