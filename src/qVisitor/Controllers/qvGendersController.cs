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
    public class qvGendersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvGendersController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Route("Genders")]
        // GET: qvGenders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genders.ToListAsync());
        }
        [Route("Genders/Details/{id}")]
        // GET: qvGenders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvGender = await _context.Genders.SingleOrDefaultAsync(m => m.Id == id);
            if (qvGender == null)
            {
                return NotFound();
            }

            return View(qvGender);
        }
        [Route("Genders/Create")]
        // GET: qvGenders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvGenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name")] qvGender qvGender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvGender);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvGender);
        }
        [Route("Genders/Edit/{id}")]
        // GET: qvGenders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvGender = await _context.Genders.SingleOrDefaultAsync(m => m.Id == id);
            if (qvGender == null)
            {
                return NotFound();
            }
            return View(qvGender);
        }

        // POST: qvGenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name")] qvGender qvGender)
        {
            if (id != qvGender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvGender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvGenderExists(qvGender.Id))
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
            return View(qvGender);
        }
        [Route("Genders/Delete/{id}")]
        // GET: qvGenders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvGender = await _context.Genders.SingleOrDefaultAsync(m => m.Id == id);
            if (qvGender == null)
            {
                return NotFound();
            }

            return View(qvGender);
        }

        // POST: qvGenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvGender = await _context.Genders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Genders.Remove(qvGender);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvGenderExists(int id)
        {
            return _context.Genders.Any(e => e.Id == id);
        }
    }
}
