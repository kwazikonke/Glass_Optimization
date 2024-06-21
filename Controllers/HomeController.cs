using GlassOpt.Models;
using GlassOpt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace GlassOpt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GlassCuttingOptimizerService _optService;

        public HomeController(ILogger<HomeController> logger, GlassCuttingOptimizerService optService)
        {
            _logger = logger;
            _optService = optService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Optimize(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewData["Message"] = "Please upload a file.";
                return View("Index");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".txt")
            {
                TempData["AlertMessage"] = "Please note that a file must be a text file (.txt).";
                return RedirectToAction("Index");
            }

            var filesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            if (!Directory.Exists(filesDir))
            {
                Directory.CreateDirectory(filesDir);
            }
            var filePath = Path.Combine(filesDir, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var optSheets = _optService.OptimizingCutting(filePath);
            return View("OptResults",optSheets);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
