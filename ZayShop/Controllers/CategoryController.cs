using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZayShop.Models;
using ZayShop.Models.Data;

namespace ZayShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            if (category.ImageFile != null)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "assets", "img", "category");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(category.ImageFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await category.ImageFile.CopyToAsync(stream);
                }

                category.ImageName = fileName;
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel category, bool DeleteImage = false)
        {
            var dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == category.Id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            if (DeleteImage && !string.IsNullOrEmpty(dbCategory.ImageName))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, "assets", "img", "category", dbCategory.ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                dbCategory.ImageName = null;
            }
            else if (category.ImageFile != null)
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, "assets", "img", "category", dbCategory.ImageName ?? "");
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                string filename = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                string newImagePath = Path.Combine(_env.WebRootPath, "assets", "img", "category", filename);
                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await category.ImageFile.CopyToAsync(stream);
                }
                category.ImageName = filename;
            }
            else
            {
                dbCategory.ImageName = dbCategory.ImageName; // Keep the old image if no new file is provided
            }
            _context.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(category.ImageName))
            {
                var imagePath = Path.Combine(_env.WebRootPath, "assets", "img", "category", category.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

    }
}
