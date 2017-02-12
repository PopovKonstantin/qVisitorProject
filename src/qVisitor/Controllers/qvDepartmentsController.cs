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
    public class qvDepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvDepartmentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvDepartments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Departments.Include(q => q.Branch);
            return View(await applicationDbContext.ToListAsync());
        }
        [Route("Companies/Branches/Departments/Details/{id}")]
        // GET: qvDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvDepartment = await _context.Departments.SingleOrDefaultAsync(m => m.Id == id);
            if (qvDepartment == null)
            {
                return NotFound();
            }

            return View(qvDepartment);
        }
        [Route("Companies/Branches/Departments/Create")]
        // GET: qvDepartments/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id");
            return View();
        }

        // POST: qvDepartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchId,Name")] qvDepartment qvDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", qvDepartment.BranchId);
            return View(qvDepartment);
        }
        [Route("Companies/Branches/Departments/Edit/{id}")]
        // GET: qvDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvDepartment = await _context.Departments.SingleOrDefaultAsync(m => m.Id == id);
            if (qvDepartment == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", qvDepartment.BranchId);
            return View(qvDepartment);
        }

        // POST: qvDepartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchId,Name")] qvDepartment qvDepartment)
        {
            if (id != qvDepartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvDepartmentExists(qvDepartment.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", qvDepartment.BranchId);
            return View(qvDepartment);
        }
        [Route("Companies/Branches/Departments/Delete/{id}")]
        // GET: qvDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvDepartment = await _context.Departments.SingleOrDefaultAsync(m => m.Id == id);
            if (qvDepartment == null)
            {
                return NotFound();
            }

            return View(qvDepartment);
        }

        // POST: qvDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvDepartment = await _context.Departments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Departments.Remove(qvDepartment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvDepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
