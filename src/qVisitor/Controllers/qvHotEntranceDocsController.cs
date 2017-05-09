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
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace qVisitor.Controllers
{
    public class qvHotEntranceDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvHotEntranceDocsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntranceDocs
        public async Task<IActionResult> Index(int? reffid)
        {
            ViewData["Reffid"] = reffid;
            var applicationDbContext = _context.qvHotEntranceDoc.Include(q => q.HotEntrance);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntranceDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntranceDoc == null)
            {
                return NotFound();
            }

            return View(qvHotEntranceDoc);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntranceDocs/Create
        public IActionResult Create(int? myid, int? reffid)
        {
            ViewData["HotEntranceId"] = new SelectList((from s in _context.HotEntrances
                                                        select
new { Id = s.Id, FullName = s.Surname + " " + s.Name + " " + s.Patronymic }), "Id", "FullName", myid);
            ViewData["Reffid"] = reffid;
            ViewData["Myid"] = myid;
            var qvHotEntrance = _context.HotEntrances.SingleOrDefault(q => q.Id == myid);
            ViewData["FullName"] = qvHotEntrance.Surname + " "+ qvHotEntrance.Name + " "+ qvHotEntrance.Patronymic;
            return View();
        }

        // POST: qvHotEntranceDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HotEntranceId")] qvHotEntranceDoc qvHotEntranceDoc, IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                if (Document != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(Document.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)Document.Length);
                    }
                    // установка массива байтов
                    qvHotEntranceDoc.Document = imageData;
                }

                _context.Add(qvHotEntranceDoc);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "qvCheckPoints");
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntranceDoc.HotEntranceId);
            return View(qvHotEntranceDoc);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntranceDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var a = Request.Headers["Referer"].ToString();
            string pat = @"=\d+";
            Regex reg = new Regex(pat);
            Match mat = reg.Match(a);
            List<Group> LG = new List<Group>();
            while (mat.Success)
            {
                LG.Add(mat.Groups[0]);
                mat = mat.NextMatch();
            }
            ViewData["Reffid"] = LG[0].Value[LG[0].Value.Length-1];
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.Include(q => q.HotEntrance).SingleOrDefaultAsync(m => m.Id == id);
            
            //ViewData["Reffid"] = reffid;
            ViewData["FullName"] = qvHotEntranceDoc.HotEntrance.Surname + " " + qvHotEntranceDoc.HotEntrance.Name + " " + qvHotEntranceDoc.HotEntrance.Patronymic;

            if (qvHotEntranceDoc == null)
            {
                return NotFound();
            }
            ViewData["HotEntranceId"] = new SelectList((from s in _context.HotEntrances
                                                        select
new { Id = s.Id, FullName = s.Surname + " " + s.Name + " " + s.Patronymic }), "Id", "FullName", id);
            return View(qvHotEntranceDoc);
        }

        // POST: qvHotEntranceDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HotEntranceId")] qvHotEntranceDoc qvHotEntranceDoc, IFormFile Document)
        {
            if (id != qvHotEntranceDoc.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (Document != null)
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(Document.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)Document.Length);
                        }
                        // установка массива байтов
                        qvHotEntranceDoc.Document = imageData;
                    }
                    _context.Update(qvHotEntranceDoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvHotEntranceDocExists(qvHotEntranceDoc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return Redirect(Request.Headers["Referer"].ToString());
                return RedirectToAction("Index", "qvCheckPoints");
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntranceDoc.HotEntranceId);
            return View(qvHotEntranceDoc);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntranceDocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var a = Request.Headers["Referer"].ToString();
            string pat = @"=\d+";
            Regex reg = new Regex(pat);
            Match mat = reg.Match(a);
            List<Group> LG = new List<Group>();
            while (mat.Success)
            {
                LG.Add(mat.Groups[0]);
                mat = mat.NextMatch();
            }
            ViewData["Reffid"] = LG[0].Value[LG[0].Value.Length - 1];
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.Include(q => q.HotEntrance).SingleOrDefaultAsync(m => m.Id == id);
            ViewData["FullName"] = qvHotEntranceDoc.HotEntrance.Surname + " " + qvHotEntranceDoc.HotEntrance.Name + " " + qvHotEntranceDoc.HotEntrance.Patronymic;
            if (qvHotEntranceDoc == null)
            {
                return NotFound();
            }

            return View(qvHotEntranceDoc);
        }

        // POST: qvHotEntranceDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvHotEntranceDoc = await _context.qvHotEntranceDoc.Include(q => q.HotEntrance).SingleOrDefaultAsync(m => m.Id == id);
            _context.qvHotEntranceDoc.Remove(qvHotEntranceDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "qvCheckPoints");
        }

        private bool qvHotEntranceDocExists(int id)
        {
            return _context.qvHotEntranceDoc.Any(e => e.Id == id);
        }
    }
}
