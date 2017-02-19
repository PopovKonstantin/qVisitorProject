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
    public class qvObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvObjectsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvObjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Objects.Include(q => q.City);
            return View(await applicationDbContext.ToListAsync());
        }
        [Route("Countries/Cities/Objects{id}")]
        // GET: qvObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvObject = await _context.Objects.SingleOrDefaultAsync(m => m.Id == id);
            if (qvObject == null)
            {
                return NotFound();
            }

            return View(qvObject);
        }
        [Route("Countries/Cities/Objects/Create")]
        // GET: qvObjects/Create
        public IActionResult Create(int? reffid)
        {
            if (reffid == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", reffid);
            ViewData["Reff"] = reffid;
            return View();
        }

        // POST: qvObjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Countries/Cities/Objects/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,Name")] qvObject qvObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvObject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvCities", new { id = qvObject.CityId });
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", qvObject.CityId);
            return View(qvObject);
        }
        [Route("Countries/Cities/Objects/Edit/{id}")]
        // GET: qvObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvObject = await _context.Objects.SingleOrDefaultAsync(m => m.Id == id);
            if (qvObject == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", qvObject.CityId);
            return View(qvObject);
        }

        // POST: qvObjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Countries/Cities/Objects/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,Name")] qvObject qvObject)
        {
            if (id != qvObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvObjectExists(qvObject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvCities", new { id = qvObject.CityId });
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", qvObject.CityId);
            return View(qvObject);
        }
        [Route("Countries/Cities/Objects/Detete/{id}")]
        // GET: qvObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvObject = await _context.Objects.SingleOrDefaultAsync(m => m.Id == id);
            if (qvObject == null)
            {
                return NotFound();
            }

            return View(qvObject);
        }

        // POST: qvObjects/Delete/5
        [Route("Countries/Cities/Objects/Detete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvObject = await _context.Objects.SingleOrDefaultAsync(m => m.Id == id);
            _context.Objects.Remove(qvObject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvCities", new { id = qvObject.CityId });
        }

        private bool qvObjectExists(int id)
        {
            return _context.Objects.Any(e => e.Id == id);
        }
    }
}
