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
    public class qvVisitorDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvVisitorDocsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorDocs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VisitorDocs.Include(q => q.Visitor);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorDoc = await _context.VisitorDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorDoc == null)
            {
                return NotFound();
            }

            return View(qvVisitorDoc);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorDocs/Create
        public IActionResult Create(int? reffid)
        {
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors select
                                                            new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", reffid);
            ViewData["Reff"] = reffid;
            return View();
        }

        // POST: qvVisitorDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpireDate,IssueDate,Name,Number,Surname,VisitorId")] qvVisitorDoc qvVisitorDoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvVisitorDoc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "qvVisitors", new { id = qvVisitorDoc.VisitorId });
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
                               new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitorDoc.VisitorId);
            return View(qvVisitorDoc);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorDoc = await _context.VisitorDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorDoc == null)
            {
                return NotFound();
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
                               new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitorDoc.VisitorId);
            return View(qvVisitorDoc);
        }

        // POST: qvVisitorDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpireDate,IssueDate,Name,Number,Surname,VisitorId")] qvVisitorDoc qvVisitorDoc)
        {
            if (id != qvVisitorDoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvVisitorDoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvVisitorDocExists(qvVisitorDoc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "qvVisitors", new { id = qvVisitorDoc.VisitorId });
            }
            ViewData["VisitorId"] = new SelectList((from s in _context.Visitors
                                                    select
                               new { Id = s.Id, FullName = s.surname + " " + s.name + " " + s.patronymic }), "Id", "FullName", qvVisitorDoc.VisitorId);
            return View(qvVisitorDoc);
        }
        [Authorize(Roles = "Менеджер")]
        // GET: qvVisitorDocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvVisitorDoc = await _context.VisitorDocs.SingleOrDefaultAsync(m => m.Id == id);
            if (qvVisitorDoc == null)
            {
                return NotFound();
            }

            return View(qvVisitorDoc);
        }

        // POST: qvVisitorDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvVisitorDoc = await _context.VisitorDocs.SingleOrDefaultAsync(m => m.Id == id);
            _context.VisitorDocs.Remove(qvVisitorDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "qvVisitors", new { id = qvVisitorDoc.VisitorId });
        }

        private bool qvVisitorDocExists(int id)
        {
            return _context.VisitorDocs.Any(e => e.Id == id);
        }
    }
}
