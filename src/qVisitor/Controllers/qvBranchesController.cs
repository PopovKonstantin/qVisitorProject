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
    public class qvBranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvBranchesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        
        // GET: qvBranches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Branches.Include(q => q.City).Include(q => q.Company);
            return View(await applicationDbContext.ToListAsync());
        }
        [Route("Companies/Branches/{id}/Departments")]
        // GET: qvBranches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvBranch = await _context.Branches.Include(d => d.Departments).SingleOrDefaultAsync(m => m.Id == id);
            if (qvBranch == null)
            {
                return NotFound();
            }

            return View(qvBranch);
        }
        [Route("Companies/Branches/Create")]
        // GET: qvBranches/Create
        public IActionResult Create(int? reffid)
        {
            if (reffid == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name",reffid);
            ViewData["Reff"] = reffid;
            return View();
        }

        // POST: qvBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Companies/Branches/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,CompanyId,HighBranchId,Name")] qvBranch qvBranch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvCompany", new { id = qvBranch.CompanyId });
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", qvBranch.CityId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", qvBranch.CompanyId);
            return View(qvBranch);
        }
        [Route("Companies/Branches/Edit/{id}")]
        // GET: qvBranches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvBranch = await _context.Branches.SingleOrDefaultAsync(m => m.Id == id);
            if (qvBranch == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", qvBranch.CityId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", qvBranch.CompanyId);
            return View(qvBranch);
        }

        // POST: qvBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Companies/Branches/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,CompanyId,HighBranchId,Name")] qvBranch qvBranch)
        {
            if (id != qvBranch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvBranch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvBranchExists(qvBranch.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvCompany", new { id = qvBranch.CompanyId });
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", qvBranch.CityId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", qvBranch.CompanyId);
            return View(qvBranch);
        }
        [Route("Companies/Branches/Delete/{id}")]
        // GET: qvBranches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvBranch = await _context.Branches.SingleOrDefaultAsync(m => m.Id == id);
            if (qvBranch == null)
            {
                return NotFound();
            }

            return View(qvBranch);
        }

        // POST: qvBranches/Delete/5
        [Route("Companies/Branches/Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvBranch = await _context.Branches.SingleOrDefaultAsync(m => m.Id == id);
            _context.Branches.Remove(qvBranch);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvCompany", new { id = qvBranch.CompanyId });
        }

        private bool qvBranchExists(int id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }
}
