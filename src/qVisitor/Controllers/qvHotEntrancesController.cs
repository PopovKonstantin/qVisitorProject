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
    public class qvHotEntrancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvHotEntrancesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvHotEntrances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HotEntrances.Include(q => q.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: qvHotEntrances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrance = await _context.HotEntrances.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrance == null)
            {
                return NotFound();
            }

            return View(qvHotEntrance);
        }

        // GET: qvHotEntrances/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            return View();
        }

        // POST: qvHotEntrances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Attendant,Comment,DepartmentId,DocumentNumber,Name,Organization,Patronymic,Surname")] qvHotEntrance qvHotEntrance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvHotEntrance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", qvHotEntrance.DepartmentId);
            return View(qvHotEntrance);
        }

        // GET: qvHotEntrances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrance = await _context.HotEntrances.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrance == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", qvHotEntrance.DepartmentId);
            return View(qvHotEntrance);
        }

        // POST: qvHotEntrances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Attendant,Comment,DepartmentId,DocumentNumber,Name,Organization,Patronymic,Surname")] qvHotEntrance qvHotEntrance)
        {
            if (id != qvHotEntrance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvHotEntrance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvHotEntranceExists(qvHotEntrance.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", qvHotEntrance.DepartmentId);
            return View(qvHotEntrance);
        }

        // GET: qvHotEntrances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrance = await _context.HotEntrances.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrance == null)
            {
                return NotFound();
            }

            return View(qvHotEntrance);
        }

        // POST: qvHotEntrances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvHotEntrance = await _context.HotEntrances.SingleOrDefaultAsync(m => m.Id == id);
            _context.HotEntrances.Remove(qvHotEntrance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvHotEntranceExists(int id)
        {
            return _context.HotEntrances.Any(e => e.Id == id);
        }
    }
}
