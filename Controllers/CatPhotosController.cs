using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CATSTAGRAM.Data;
using CATSTAGRAM.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace CATSTAGRAM.Controllers
{
    public class CatPhotosController : Controller
    {
        private readonly HomeDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CatPhotosController(HomeDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: CatPhotos
        public async Task<IActionResult> Index()
        {
              return _context.CatPhoto != null ? 
                          View(await _context.CatPhoto.ToListAsync()) :
                          Problem("Entity set 'HomeDbContext.CatPhoto'  is null.");
        }

        // GET: CatPhotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CatPhoto == null)
            {
                return NotFound();
            }

            var catPhoto = await _context.CatPhoto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catPhoto == null)
            {
                return NotFound();
            }

            return View(catPhoto);
        }

        // GET: CatPhotos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatPhotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]

        // POST: CatPhotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CatId,Name,Description,ImageUrl,Image")] CatPhoto cat)
        {

            if (ModelState.IsValid)
            {
                // Check if an image was uploaded
                if (cat.ImageFile != null)
                {
                    // Get the filename of the uploaded image
                    string fileName = Path.GetFileName(cat.ImageFile.FileName);
                    // Set the path where the image will be saved
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                    // Copy the image to the file path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await cat.ImageFile.CopyToAsync(stream);
                    }

                    // Set the ImageUrl property of the cat object to the path where the image was saved
                    cat.ImageUrl = $"/images/{fileName}";
                }

                _context.Add(cat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }








        // GET: CatPhotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CatPhoto == null)
            {
                return NotFound();
            }

            var catPhoto = await _context.CatPhoto.FindAsync(id);
            if (catPhoto == null)
            {
                return NotFound();
            }
            return View(catPhoto);
        }

        // POST: CatPhotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorName,AuthorEmail,ImageTitle,ImageDescription,ImageFileName,ImageData,DateAdded,Comments")] CatPhoto catPhoto)
        {
            if (id != catPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatPhotoExists(catPhoto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(catPhoto);
        }

        // GET: CatPhotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CatPhoto == null)
            {
                return NotFound();
            }

            var catPhoto = await _context.CatPhoto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catPhoto == null)
            {
                return NotFound();
            }

            return View(catPhoto);
        }

        // POST: CatPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CatPhoto == null)
            {
                return Problem("Entity set 'HomeDbContext.CatPhoto'  is null.");
            }
            var catPhoto = await _context.CatPhoto.FindAsync(id);
            if (catPhoto != null)
            {
                _context.CatPhoto.Remove(catPhoto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatPhotoExists(int id)
        {
          return (_context.CatPhoto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
