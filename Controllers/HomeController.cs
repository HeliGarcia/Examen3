using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Examen3.Models;
using Examen3.Data;
using Microsoft.EntityFrameworkCore;

namespace Examen3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Hamburguesas.Include(b => b.TipoPlatillo);
        return View(await applicationDbContext.ToListAsync());
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
