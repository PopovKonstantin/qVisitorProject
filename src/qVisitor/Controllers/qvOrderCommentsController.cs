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
    public class qvOrderCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvOrderCommentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: qvOrderComments
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderComments.ToListAsync());
        }

        // GET: qvOrderComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderComment = await _context.OrderComments.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderComment == null)
            {
                return NotFound();
            }

            return View(qvOrderComment);
        }

        // GET: qvOrderComments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: qvOrderComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,CommentDate")] qvOrderComment qvOrderComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qvOrderComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qvOrderComment);
        }

        // GET: qvOrderComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderComment = await _context.OrderComments.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderComment == null)
            {
                return NotFound();
            }
            return View(qvOrderComment);
        }

        // POST: qvOrderComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,CommentDate")] qvOrderComment qvOrderComment)
        {
            if (id != qvOrderComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qvOrderComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvOrderCommentExists(qvOrderComment.Id))
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
            return View(qvOrderComment);
        }

        // GET: qvOrderComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvOrderComment = await _context.OrderComments.SingleOrDefaultAsync(m => m.Id == id);
            if (qvOrderComment == null)
            {
                return NotFound();
            }

            return View(qvOrderComment);
        }

        // POST: qvOrderComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvOrderComment = await _context.OrderComments.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderComments.Remove(qvOrderComment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool qvOrderCommentExists(int id)
        {
            return _context.OrderComments.Any(e => e.Id == id);
        }
    }
}
