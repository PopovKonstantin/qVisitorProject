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
    public class qvCheckPointsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvCheckPointsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Route("CheckPoints")]
        // GET: qvCheckPoints
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CheckPoints.Include(q => q.Object);
            return View(await applicationDbContext.ToListAsync());
        }
        [Route("CheckPoints/Details/{id}")]
        // GET: qvCheckPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCheckPoint = await _context.CheckPoints.Include(e => e.Entrances).Include(nr => nr.NotRecognizedDocs).SingleOrDefaultAsync(m => m.Id == id);
            if (qvCheckPoint == null)
            {
                return NotFound();
            }

            return View(qvCheckPoint);
        }
        [Route("CheckPoints/Create")]
        // GET: qvCheckPoints/Create
        public IActionResult Create()
        {
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id");
            return View();
        }

        // POST: qvCheckPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ObjectId")] qvCheckPoint qvCheckPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvCheckPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id", qvCheckPoint.ObjectId);
            return View(qvCheckPoint);
        }
        [Route("CheckPoints/Edit/{id}")]
        // GET: qvCheckPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCheckPoint = await _context.CheckPoints.SingleOrDefaultAsync(m => m.Id == id);
            if (qvCheckPoint == null)
            {
                return NotFound();
            }
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id", qvCheckPoint.ObjectId);
            return View(qvCheckPoint);
        }

        // POST: qvCheckPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ObjectId")] qvCheckPoint qvCheckPoint)
        {
            if (id != qvCheckPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvCheckPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvCheckPointExists(qvCheckPoint.Id))
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
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id", qvCheckPoint.ObjectId);
            return View(qvCheckPoint);
        }
        [Route("CheckPoints/Delete/{id}")]
        // GET: qvCheckPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCheckPoint = await _context.CheckPoints.SingleOrDefaultAsync(m => m.Id == id);
            if (qvCheckPoint == null)
            {
                return NotFound();
            }

            return View(qvCheckPoint);
        }

        // POST: qvCheckPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvCheckPoint = await _context.CheckPoints.SingleOrDefaultAsync(m => m.Id == id);
            _context.CheckPoints.Remove(qvCheckPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvCheckPointExists(int id)
        {
            return _context.CheckPoints.Any(e => e.Id == id);
        }
    }
}
