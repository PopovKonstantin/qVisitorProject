using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qVisitor.Data;
using qVisitor.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace qVisitor.Controllers
{
    public class qvHotEntrancePhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public qvHotEntrancePhotoesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrancePhotoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HotEntrancePhotos.Include(q => q.HotEntrance);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrancePhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (qvHotEntrancePhoto == null)
            {
                return NotFound();
            }

            return View(qvHotEntrancePhoto);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrancePhotoes/Create
        public IActionResult Create(int? myid, int? reffid)
        {
            ViewData["HotEntranceId"] = new SelectList((from s in _context.HotEntrances
                                                        select
new { Id = s.Id, FullName = s.Surname + " " + s.Name + " " + s.Patronymic }), "Id", "FullName", myid);
            ViewData["Reffid"] = reffid;
            ViewData["Myid"] = myid;
            var qvHotEntrance = _context.HotEntrances.SingleOrDefault(q => q.Id == myid);
            ViewData["FullName"] = qvHotEntrance.Surname + " " + qvHotEntrance.Name + " " + qvHotEntrance.Patronymic;
            return View();
        }

        // POST: qvHotEntrancePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HotEntranceId")] qvHotEntrancePhoto qvHotEntrancePhoto, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(Photo.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)Photo.Length);
                    }
                    // установка массива байтов
                    qvHotEntrancePhoto.Photo = imageData;
                }
                _context.Add(qvHotEntrancePhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "qvCheckPoints");
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntrancePhoto.HotEntranceId);
            return View(qvHotEntrancePhoto);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrancePhotoes/Edit/5
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
            ViewData["Reffid"] = LG[0].Value[LG[0].Value.Length - 1];
            if (id == null)
            {
                return NotFound();
            }

            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.Include(q => q.HotEntrance).SingleOrDefaultAsync(m => m.Id == id);
            ViewData["FullName"] = qvHotEntrancePhoto.HotEntrance.Surname + " " + qvHotEntrancePhoto.HotEntrance.Name + " " + qvHotEntrancePhoto.HotEntrance.Patronymic;
            if (qvHotEntrancePhoto == null)
            {
                return NotFound();
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntrancePhoto.HotEntranceId);
            return View(qvHotEntrancePhoto);
        }

        // POST: qvHotEntrancePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HotEntranceId")] qvHotEntrancePhoto qvHotEntrancePhoto, IFormFile Photo)
        {
            if (id != qvHotEntrancePhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Photo != null)
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(Photo.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)Photo.Length);
                        }
                        // установка массива байтов
                        qvHotEntrancePhoto.Photo = imageData;
                    }
                    _context.Update(qvHotEntrancePhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!qvHotEntrancePhotoExists(qvHotEntrancePhoto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "qvCheckPoints");
            }
            ViewData["HotEntranceId"] = new SelectList(_context.HotEntrances, "Id", "Id", qvHotEntrancePhoto.HotEntranceId);
            return View(qvHotEntrancePhoto);
        }
        [Authorize(Roles = "Охрана")]
        // GET: qvHotEntrancePhotoes/Delete/5
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

            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.Include(q => q.HotEntrance).SingleOrDefaultAsync(m => m.Id == id);
            ViewData["FullName"] = qvHotEntrancePhoto.HotEntrance.Surname + " " + qvHotEntrancePhoto.HotEntrance.Name + " " + qvHotEntrancePhoto.HotEntrance.Patronymic;
            if (qvHotEntrancePhoto == null)
            {
                return NotFound();
            }

            return View(qvHotEntrancePhoto);
        }

        // POST: qvHotEntrancePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qvHotEntrancePhoto = await _context.HotEntrancePhotos.SingleOrDefaultAsync(m => m.Id == id);
            _context.HotEntrancePhotos.Remove(qvHotEntrancePhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "qvCheckPoints");
        }

        private bool qvHotEntrancePhotoExists(int id)
        {
            return _context.HotEntrancePhotos.Any(e => e.Id == id);
        }
    }
}
