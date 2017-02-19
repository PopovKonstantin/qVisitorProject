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
    public class qvCitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvCitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvCities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cities.Include(q => q.Country);
            return View(await applicationDbContext.ToListAsync());
        }
        [Route("Countries/Cities/{id}/Objects")]
        // GET: qvCities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCity = await _context.Cities.Include(o => o.Objects).SingleOrDefaultAsync(m => m.Id == id);
            if (qvCity == null)
            {
                return NotFound();
            }

            return View(qvCity);
        }
        [Route("Countries/Cities/Create")]
        // GET: qvCities/Create
        public IActionResult Create(int? reffid)
        {
            if (reffid == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "Id", "Name", reffid);
            ViewData["Reff"] = reffid;
            return View();
        }

        // POST: qvCities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Countries/Cities/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryID,Name")] qvCity qvCity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvCity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvCountries", new { id = qvCity.CountryID });
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "Id", "Name", qvCity.CountryID);
            return View(qvCity);
        }
        [Route("Countries/Cities/Edit/{id}")]
        // GET: qvCities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCity = await _context.Cities.SingleOrDefaultAsync(m => m.Id == id);
            if (qvCity == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "Id", "Name", qvCity.CountryID);
            return View(qvCity);
        }

        // POST: qvCities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Countries/Cities/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryID,Name")] qvCity qvCity)
        {
            if (id != qvCity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvCity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvCityExists(qvCity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvCountries", new {id = qvCity.CountryID});
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "Id", "Name", qvCity.CountryID);
            return View(qvCity);
        }
        [Route("Countries/Cities/Delete/{id}")]
        // GET: qvCities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCity = await _context.Cities.SingleOrDefaultAsync(m => m.Id == id);
            if (qvCity == null)
            {
                return NotFound();
            }

            return View(qvCity);
        }
        [Route("Countries/Cities/Delete/{id}")]
        // POST: qvCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvCity = await _context.Cities.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cities.Remove(qvCity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvCountries", new { id = qvCity.CountryID });
        }

        private bool qvCityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
