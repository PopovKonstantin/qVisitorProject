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
        [Route("Visitors")]
        // GET: qvVisitors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Visitors.Include(q => q.Gender);
            return View(await applicationDbContext.ToListAsync());
        }
        [Route("Visitors/Details/{id}")]
        // GET: qvVisitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitor = await _context.Visitors.Include(vp => vp.VisitorPhotoes).Include(vd => vd.VisitorDocs).Include(vl => vl.VisitorLuggages).Include(vs => vs.VisitorScans).SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitor == null)
            {
                return NotFound();
            }

            return View(qvVisitor);
        }
        [Route("Visitors/Create")]
        // GET: qvVisitors/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id");
            return View();
        }

        // POST: qvVisitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GenderId,birthdate,name,patronymic,surname")] qvVisitor qvVisitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvVisitor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", qvVisitor.GenderId);
            return View(qvVisitor);
        }
        [Route("Visitors/Edit/{id}")]
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
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", qvVisitor.GenderId);
            return View(qvVisitor);
        }

        // POST: qvVisitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenderId,birthdate,name,patronymic,surname")] qvVisitor qvVisitor)
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
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", qvVisitor.GenderId);
            return View(qvVisitor);
        }
        [Route("Visitors/Delete/{id}")]
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
