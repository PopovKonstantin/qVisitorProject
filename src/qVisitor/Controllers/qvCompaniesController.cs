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
    public class qvCompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvCompaniesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Route("Companies")]
        // GET: qvCompanies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Companies.Include(q => q.Country);
            return View(await applicationDbContext.ToListAsync());
        }
        [Route("Companies/{id}/Branches")]
        // GET: qvCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCompany = await _context.Companies.Include(b =>b.Branches).SingleOrDefaultAsync(m => m.Id == id);
            if (qvCompany == null)
            {
                return NotFound();
            }

            return View(qvCompany);
        }
        [Route("Companies/Create")]
        // GET: qvCompanies/Create
        public IActionResult Create()
        {
            ViewData["CounryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: qvCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Companies/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CounryId,Name")] qvCompany qvCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CounryId"] = new SelectList(_context.Countries, "Id", "Name", qvCompany.CounryId);
            return View(qvCompany);
        }
        [Route("Companies/Edit/{id}")]
        // GET: qvCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCompany = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            if (qvCompany == null)
            {
                return NotFound();
            }
            ViewData["CounryId"] = new SelectList(_context.Countries, "Id", "Name", qvCompany.CounryId);
            return View(qvCompany);
        }

        // POST: qvCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Companies/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CounryId,Name")] qvCompany qvCompany)
        {
            if (id != qvCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvCompanyExists(qvCompany.Id))
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
            ViewData["CounryId"] = new SelectList(_context.Countries, "Id", "Name", qvCompany.CounryId);
            return View(qvCompany);
        }
        [Route("Companies/Delete/{id}")]
        // GET: qvCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvCompany = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            if (qvCompany == null)
            {
                return NotFound();
            }

            return View(qvCompany);
        }

        // POST: qvCompanies/Delete/5
        [Route("Companies/Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvCompany = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Companies.Remove(qvCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvCompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
