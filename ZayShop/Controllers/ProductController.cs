using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZayShop.Models;
using ZayShop.Models.Data;

namespace ZayShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (product.ImageFile != null)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "assets", "img", "products");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                //Global unik ID
                string filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(stream);
                }
                product.ImageName = fileName;
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel product, bool DeleteImage = false)
        {
            var dbProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == product.Id);
            if (dbProduct == null)
            {
                return NotFound();
            }
            if (DeleteImage && !string.IsNullOrEmpty(dbProduct.ImageName))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, "assets", "img", "products", dbProduct.ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                dbProduct.ImageName = null;
            }
            else if (product.ImageFile != null)
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, "assets", "img", "products", dbProduct.ImageName ?? "");
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                string filename = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string newImagePath = Path.Combine(_env.WebRootPath, "assets", "img", "products", filename);
                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(stream);
                }
                product.ImageName = filename;
            }
            else
            {
                dbProduct.ImageName = dbProduct.ImageName; // Keep the old image if no new file is provided
            }
            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(product.ImageName))
            {
                var imagePath = Path.Combine(_env.WebRootPath, "assets", "img", "products", product.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
