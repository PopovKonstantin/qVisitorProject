using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qVisitor.Data;
using qVisitor.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace qVisitor.Controllers
{
    public class qvVisitorScansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitorScansController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorScans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VisitorScan.Include(q => q.Visitor);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorScans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorScan == null)
            {
                return NotFound();
            }

            return View(qvVisitorScan);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorScans/Create
        public IActionResult Create(int? reffid)
        {
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
            new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", reffid);
            ViewData["Reff"] = reffid;
            return View();
        }

        // POST: qvVisitorScans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VisitorId")] qvVisitorScan qvVisitorScan, IFormFile Scan)
        {
            if (ModelState.IsValid)
            {
                if (Scan != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(Scan.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)Scan.Length);
                    }
                    // установка массива байтов
                    qvVisitorScan.Scan = imageData;
                }

                _context.Add(qvVisitorScan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvVisitors", new { id = qvVisitorScan.VisitorId });
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
        new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitorScan.VisitorId);
            return View(qvVisitorScan);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorScans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorScan == null)
            {
                return NotFound();
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitorScan.VisitorId);
            return View(qvVisitorScan);
        }

        // POST: qvVisitorScans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitorId")] qvVisitorScan qvVisitorScan, IFormFile Scan)
        {
            if (id != qvVisitorScan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Scan != null)
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(Scan.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)Scan.Length);
                        }
                        // установка массива байтов
                        qvVisitorScan.Scan = imageData;
                    }
                    _context.Update(qvVisitorScan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvVisitorScanExists(qvVisitorScan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvVisitors", new { id = qvVisitorScan.VisitorId });
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
            new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitorScan.VisitorId);
            return View(qvVisitorScan);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorScans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorScan == null)
            {
                return NotFound();
            }

            return View(qvVisitorScan);
        }

        // POST: qvVisitorScans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvVisitorScan = await _context.VisitorScan.SingleOrDefaultAsync(m => m.Id == id);
            _context.VisitorScan.Remove(qvVisitorScan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvVisitors", new { id = qvVisitorScan.VisitorId });
        }

        private bool qvVisitorScanExists(int id)
        {
            return _context.VisitorScan.Any(e => e.Id == id);
        }
    }
}
