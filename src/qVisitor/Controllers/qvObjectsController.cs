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
    public class qvObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvObjectsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Администратор")]
        // GET: qvObjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Objects.Include(q => q.City);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Администратор")]
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
        [Authorize(Roles = "Администратор")]
        // GET: qvObjects/Create
        public IActionResult Create(int? reffid, int? countryreff)
        {
            if (reffid == null)
            {
                return NotFound();
            }

            if (countryreff == null)
            {
                return NotFound();
            }

            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", reffid);
            ViewData["Reff"] = reffid;
            ViewData["CountryReff"] = countryreff;
            var cn = from c in _context.Countries
                     where c.Id == countryreff
                     select c.Name;
            ViewData["CountryName"] = cn.ToList()[0];
            var citn = from c in _context.Cities
                       where c.Id == reffid
                       select c.Name;
            ViewData["CityName"] = citn.ToList()[0];
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
        [Authorize(Roles = "Администратор")]
        // GET: qvObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvObject = await _context.Objects.Include(c=> c.City).SingleOrDefaultAsync(m => m.Id == id);
            if (qvObject == null)
            {
                return NotFound();
            }
            ViewData["CountryReff"] = qvObject.City.CountryID;
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", qvObject.CityId);

            var cn = from c in _context.Countries
                     where c.Id == qvObject.City.CountryID
                     select c.Name;
            ViewData["CountryName"] = cn.ToList()[0];
            var citn = from c in _context.Cities
                       where c.Id == qvObject.CityId
                       select c.Name;
            ViewData["CityName"] = citn.ToList()[0];
            var on = from c in _context.Objects
                       where c.Id == id
                     select c.Name;
            ViewData["ObjectName"] = on.ToList()[0];

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
        [Authorize(Roles = "Администратор")]
        // GET: qvObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvObject = await _context.Objects.Include(c => c.City).SingleOrDefaultAsync(m => m.Id == id);
            if (qvObject == null)
            {
                return NotFound();
            }
            ViewData["CountryReff"] = qvObject.City.CountryID;

            var cn = from c in _context.Countries
                     where c.Id == qvObject.City.CountryID
                     select c.Name;
            ViewData["CountryName"] = cn.ToList()[0];
            var citn = from c in _context.Cities
                       where c.Id == qvObject.CityId
                       select c.Name;
            ViewData["CityName"] = citn.ToList()[0];
            var on = from c in _context.Objects
                     where c.Id == id
                     select c.Name;
            ViewData["ObjectName"] = on.ToList()[0];

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
