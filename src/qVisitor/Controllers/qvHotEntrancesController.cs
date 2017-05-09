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
    public class qvHotEntrancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvHotEntrancesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrances
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = _context.HotEntrances.Include(q => q.Department);
            ViewData["Reffid"] = id;
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrances/Details/5
        public async Task<IActionResult> Details(int? id, int? reffid)
        {
            ViewData["Reffid"] = reffid;
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrance = await _context.HotEntrances.Include(q => q.HotEntranceDocs).Include(q => q.HotEntrancePhotoes).SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrance == null)
            {
                return NotFound();
            }

            return View(qvHotEntrance);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrances/Create
        public IActionResult Create(int? reffid)
        {
            ViewData["Reffid"] = reffid;
            var qvCheckPoint = _context.CheckPoints.Include(q=>q.Object).SingleOrDefault(q => q.Id == reffid);
            ViewData["DepartmentId"] = new SelectList(_context.Departments.Where(q =>q.Branch.CityId == qvCheckPoint.Object.CityId), "Id", "Name");
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
                return RedirectToAction("Index", "qvCheckPoints");
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", qvHotEntrance.DepartmentId);
            return View(qvHotEntrance);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrances/Edit/5
        public async Task<IActionResult> Edit(int? id, int? reffid)
        {
            ViewData["Reffid"] = reffid;
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrance = await _context.HotEntrances.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrance == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", qvHotEntrance.DepartmentId);
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
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrances/Delete/5
        public async Task<IActionResult> Delete(int? id, int? reffid)
        {
            ViewData["Reffid"] = reffid;
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrance = await _context.HotEntrances.Include(q => q.Department).SingleOrDefaultAsync(m => m.Id == id);
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
