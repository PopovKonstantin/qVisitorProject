using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qVisitor.Data;
using qVisitor.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace qVisitor.Controllers
{
    public class refOrderVisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public refOrderVisitorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Менеджер")]
        // GET: refOrderVisitors
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = _context.refOrderVisitor.Include(r => r.Order).Include(r => r.Visitor).Where(r => r.OrderId == id);
            ViewData["ReffId"] = id;
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> OrdersByVisitors()
        {
            var applicationDbContext = _context.refOrderVisitor.Include(r => r.Order).Include(r => r.Visitor).ThenInclude(r => r.VisitorLuggages);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> EditOrdersByVisitors(int? id)
        {
            var applicationDbContext = _context.refOrderVisitor.Include(r => r.Order).Include(r => r.Visitor).Where(r => r.OrderId == id);
            ViewData["ReffId"] = id;
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
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
        [Authorize(Roles = "Менеджер")]
        // GET: refOrderVisitors/Create
        public IActionResult Create(int? reffid)
        {
            if (reffid == null)
            {
                return NotFound();
            }

            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", reffid);
            ViewData["ReffId"] = reffid;
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName");

            string text = Request.Headers["Referer"].ToString();
            string pat = @"([A-Za-z]+)(/)";
            Regex reg = new Regex(pat);
            Match mat = reg.Match(text);
            List<Group> LG = new List<Group>();
            while (mat.Success)
            {
                LG.Add(mat.Groups[0]);
                mat = mat.NextMatch();
            }

            ViewData["ViewId"] = LG.Last().ToString();
            return View();
        }

        [Authorize(Roles = "Менеджер")]
        // GET: qvOrders/Delete/5
        public async Task<IActionResult> Undo(int? id)
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
        [HttpPost, ActionName("Undo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UndoConfirmed(int id)
        {
            var qvOrder = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Orders.Remove(qvOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "qvOrders");
        }

        // POST: refOrderVisitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitorId,OrderId")] refOrderVisitor refOrderVisitor, string Order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refOrderVisitor);
                await _context.SaveChangesAsync();
                if (Order == "qvOrders")
                {
                    return RedirectToAction("Index", "refOrderVisitors", new { id = refOrderVisitor.OrderId });
                }
                else
                {
                    return RedirectToAction(Order, "refOrderVisitors", new { id = refOrderVisitor.OrderId });
                }
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", refOrderVisitor.OrderId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", refOrderVisitor.VisitorId);
            return View(refOrderVisitor);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: refOrderVisitors/Edit/5
        public async Task<IActionResult> Edit(int? id, int? visid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refOrderVisitor = await _context.refOrderVisitor.Include(r => r.Order).Include(r => r.Visitor).SingleOrDefaultAsync(m => m.OrderId == id && m.VisitorId == visid);
            if (refOrderVisitor == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", refOrderVisitor.OrderId);
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", refOrderVisitor.VisitorId);
            return View(refOrderVisitor);
        }

        // POST: refOrderVisitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("VisitorId,OrderId")] refOrderVisitor refOrderVisitor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refOrderVisitor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("EditOrdersByVisitors", "refOrderVisitors", new { id = refOrderVisitor.OrderId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", refOrderVisitor.OrderId);
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", refOrderVisitor.VisitorId);
            return View(refOrderVisitor);
        }

        [Authorize(Roles = "Менеджер")]
        // GET: refOrderVisitors/Delete/5
        public async Task<IActionResult> Delete(int? id, int? visid)
        {
            if (id == null)
            {
                return NotFound();
            }

            string text = Request.Headers["Referer"].ToString();
            string pat = @"([A-Za-z]+)(/)";
            Regex reg = new Regex(pat);
            Match mat = reg.Match(text);
            List<Group> LG = new List<Group>();
            while (mat.Success)
            {
                LG.Add(mat.Groups[1]);
                mat = mat.NextMatch();
            }

            ViewData["ViewId"] = LG.Last().ToString();
            ViewData["VisitorId"] = visid;

            var refOrderVisitor = await _context.refOrderVisitor.Include(r => r.Order).Include(r => r.Visitor).SingleOrDefaultAsync(m => m.OrderId == id && m.VisitorId == visid);
            if (refOrderVisitor == null)
            {
                return NotFound();
            }

            return View(refOrderVisitor);
        }

        // POST: refOrderVisitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int VisitorId, string OrderId)
        {
            var refOrderVisitor = await _context.refOrderVisitor.Include(r => r.Order).Include(r => r.Visitor).SingleOrDefaultAsync(m => m.OrderId == id && m.VisitorId == VisitorId);
            _context.refOrderVisitor.Remove(refOrderVisitor);
            await _context.SaveChangesAsync();
            if (OrderId == "Index")
            {
                return RedirectToAction("Index", "refOrderVisitors", new { id = refOrderVisitor.OrderId });
            }
            else
            {
                return RedirectToAction(OrderId, "refOrderVisitors", new { id = refOrderVisitor.OrderId });
            }
        }

        private bool refOrderVisitorExists(int id)
        {
            return _context.refOrderVisitor.Any(e => e.VisitorId == id);
        }
    }
}
