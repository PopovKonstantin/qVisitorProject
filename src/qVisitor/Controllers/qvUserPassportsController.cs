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
    public class qvUserPassportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvUserPassportsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvUserPassports
        public async Task<IActionResult> Index()
        {
            return View(await _context.qvUserPassports.ToListAsync());
        }

        // GET: qvUserPassports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvUserPassport = await _context.qvUserPassports.SingleOrDefaultAsync(m => m.Id == id);
            if (qvUserPassport == null)
            {
                return NotFound();
            }

            return View(qvUserPassport);
        }

        // GET: qvUserPassports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvUserPassports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Birthdate,Gender,Name,Patronymic,Surname")] qvUserPassport qvUserPassport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvUserPassport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvUserPassport);
        }

        // GET: qvUserPassports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvUserPassport = await _context.qvUserPassports.SingleOrDefaultAsync(m => m.Id == id);
            if (qvUserPassport == null)
            {
                return NotFound();
            }
            return View(qvUserPassport);
        }

        // POST: qvUserPassports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Birthdate,Gender,Name,Patronymic,Surname")] qvUserPassport qvUserPassport)
        {
            if (id != qvUserPassport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvUserPassport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvUserPassportExists(qvUserPassport.Id))
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
            return View(qvUserPassport);
        }

        // GET: qvUserPassports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvUserPassport = await _context.qvUserPassports.SingleOrDefaultAsync(m => m.Id == id);
            if (qvUserPassport == null)
            {
                return NotFound();
            }

            return View(qvUserPassport);
        }

        // POST: qvUserPassports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvUserPassport = await _context.qvUserPassports.SingleOrDefaultAsync(m => m.Id == id);
            _context.qvUserPassports.Remove(qvUserPassport);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvUserPassportExists(int id)
        {
            return _context.qvUserPassports.Any(e => e.Id == id);
        }
    }
}
