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
    public class qvNotRecognizedDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvNotRecognizedDocsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvNotRecognizedDocs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NotRecognizedDocs.Include(q => q.CheckPoint);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvNotRecognizedDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvNotRecognizedDoc == null)
            {
                return NotFound();
            }

            return View(qvNotRecognizedDoc);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvNotRecognizedDocs/Create
        public IActionResult Create(int? id)
        {
            var applicationDbContext = _context.CheckPoints.SingleOrDefault(m => m.Id == id);
            ViewData["KPPName"] = applicationDbContext.Name;
            ViewData["KPPId"] = id;
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id",id);
            return View();
        }

        // POST: qvNotRecognizedDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CheckPointId")] qvNotRecognizedDoc qvNotRecognizedDoc, IFormFile Scan)
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
                    qvNotRecognizedDoc.Scan = imageData;
                }
                _context.Add(qvNotRecognizedDoc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvCheckPoints", new { id = qvNotRecognizedDoc.CheckPointId });
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvNotRecognizedDoc.CheckPointId);
            return View(qvNotRecognizedDoc);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvNotRecognizedDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.Include(q => q.CheckPoint).SingleOrDefaultAsync(m => m.Id == id);
            if (qvNotRecognizedDoc == null)
            {
                return NotFound();
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvNotRecognizedDoc.CheckPointId);
            return View(qvNotRecognizedDoc);
        }

        // POST: qvNotRecognizedDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckPointId")] qvNotRecognizedDoc qvNotRecognizedDoc, IFormFile Scan)
        {
            if (id != qvNotRecognizedDoc.Id)
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
                        qvNotRecognizedDoc.Scan = imageData;
                    }
                    _context.Update(qvNotRecognizedDoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvNotRecognizedDocExists(qvNotRecognizedDoc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvCheckPoints", new { id = qvNotRecognizedDoc.CheckPointId });
            }
            ViewData["CheckPointId"] = new SelectList(_context.CheckPoints, "Id", "Id", qvNotRecognizedDoc.CheckPointId);
            return View(qvNotRecognizedDoc);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvNotRecognizedDocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.Include(q=>q.CheckPoint).SingleOrDefaultAsync(m => m.Id == id);
            if (qvNotRecognizedDoc == null)
            {
                return NotFound();
            }

            return View(qvNotRecognizedDoc);
        }

        // POST: qvNotRecognizedDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvNotRecognizedDoc = await _context.NotRecognizedDocs.SingleOrDefaultAsync(m => m.Id == id);
            _context.NotRecognizedDocs.Remove(qvNotRecognizedDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvCheckPoints", new { id = qvNotRecognizedDoc.CheckPointId });
        }

        private bool qvNotRecognizedDocExists(int id)
        {
            return _context.NotRecognizedDocs.Any(e => e.Id == id);
        }
    }
}
