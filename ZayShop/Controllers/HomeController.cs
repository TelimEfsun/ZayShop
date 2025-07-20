using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZayShop.Models;
using ZayShop.Models.Data;

namespace ZayShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        var sliders = await _context.Sliders.ToListAsync();
       

        var categories = new List<CategoryModel>
        {
            new CategoryModel
            {
                Id=1,
                Name="Watches",
                Image="category_img_01.jpg"
            },
            new CategoryModel
            {
                Id=2,
                Name="Shoes",
                Image="category_img_02.jpg"
            },
            new CategoryModel
            {
                Id=3,
                Name="Accessories",
                Image="category_img_03.jpg"
            }
        };  
        var products = new List<ProductModel>
        {
            new ProductModel
            {
                Id=1,
                Name="Gym Weight",
                Description="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt in culpa qui officia deserunt.",
                Image="feature_prod_01.jpg",
                ReviewCount= 24,
                Price= 240
            },
            new ProductModel
            {
                Id=2,
                Name="Cloud Nike Shoes",
                Description="Aenean gravida dignissim finibus. Nullam ipsum diam, posuere vitae pharetra sed, commodo ullamcorper.",
                Image="feature_prod_02.jpg",
                ReviewCount= 48,
                Price= 480
            },
            new ProductModel
            {
                Id=3,
                Name="Summer Addides Shoes",
                Description="Curabitur ac mi sit amet diam luctus porta. Phasellus pulvinar sagittis diam, et scelerisque ipsum lobortis nec.",
                Image="feature_prod_03.jpg",
                ReviewCount= 74,
                Price= 360
            }
        };

        var homeviewmodel = new HomeViewModel
        {
            Sliders = sliders,
            Categories = categories,
            Products = products            
        };

        return View(homeviewmodel);
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Shop()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
