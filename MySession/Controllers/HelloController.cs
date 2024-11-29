using Microsoft.AspNetCore.Mvc;

namespace MySession.Controllers
{
    public class HelloController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from new controller");
            return View();
        }
    }
}
