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
    public class qvEntrancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvEntrancesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Entrances.Include(q => q.CheckPoint).Include(q => q.EntranceType).Include(q => q.Order).ThenInclude(q => q.RefOrderVisitors).ThenInclude(q => q.Visitor);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntrance = await _context.Entrances.Include(q => q.CheckPoint).Include(q => q.EntranceType).Include(q => q.Order).ThenInclude(q => q.RefOrderVisitors).ThenInclude(q => q.Visitor).Include(q=>q.EntranceDocs).Include(q=>q.EntrancePhotoes).SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntrance == null)
            {
                return NotFound();
            }

            return View(qvEntrance);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrances/Create
        public IActionResult Create(int? reffid)
        {
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Name", reffid);
            ViewData["EntranceTypeId"] = new SelectList(_context.EntranceTypes, "Id", "Description");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["Reff"] = reffid;
            ViewData["KPP"] = _context.CheckPoints.SingleOrDefault(q => q.Id == reffid).Name;
            return View();
        }

        // POST: qvEntrances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActionDate,CheckPointId,EntranceTypeId,OrderId")] qvEntrance qvEntrance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvEntrance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvCheckPoints", new { id = qvEntrance.CheckPointId });
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvEntrance.CheckPointId);
            ViewData["EntranceTypeId"] = new SelectList(_context.EntranceTypes, "Id", "Id", qvEntrance.EntranceTypeId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvEntrance.OrderId);
            return View(qvEntrance);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntrance = await _context.Entrances.SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntrance == null)
            {
                return NotFound();
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Name", qvEntrance.CheckPointId);
            ViewData["EntranceTypeId"] = new SelectList(_context.EntranceTypes, "Id", "Description", qvEntrance.EntranceTypeId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvEntrance.OrderId);
            ViewData["CPName"] = _context.CheckPoints.SingleOrDefault(q => q.Id == qvEntrance.CheckPointId).Name;
            return View(qvEntrance);
        }

        // POST: qvEntrances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActionDate,CheckPointId,EntranceTypeId,OrderId")] qvEntrance qvEntrance)
        {
            if (id != qvEntrance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvEntrance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvEntranceExists(qvEntrance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvCheckPoints", new { id = qvEntrance.CheckPointId });
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvEntrance.CheckPointId);
            ViewData["EntranceTypeId"] = new SelectList(_context.EntranceTypes, "Id", "Id", qvEntrance.EntranceTypeId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", qvEntrance.OrderId);
            return View(qvEntrance);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvEntrances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvEntrance = await _context.Entrances.Include(q => q.CheckPoint).Include(q => q.EntranceType).Include(q => q.Order).ThenInclude(q => q.RefOrderVisitors).ThenInclude(q => q.Visitor).SingleOrDefaultAsync(m => m.Id == id);
            if (qvEntrance == null)
            {
                return NotFound();
            }

            ViewData["CPName"] = _context.CheckPoints.SingleOrDefault(q => q.Id == qvEntrance.CheckPointId).Name;
            return View(qvEntrance);
        }

        // POST: qvEntrances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvEntrance = await _context.Entrances.SingleOrDefaultAsync(m => m.Id == id);
            _context.Entrances.Remove(qvEntrance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvCheckPoints", new { id = qvEntrance.CheckPointId });
        }

        private bool qvEntranceExists(int id)
        {
            return _context.Entrances.Any(e => e.Id == id);
        }
    }
}
