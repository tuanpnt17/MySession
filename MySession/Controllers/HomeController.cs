namespace MySession.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        var session = HttpContext.GetSession();
        session.SetString("Home", "Tuan Pham");

        await session.CommitAsync();

        HttpContext.GetSession().SetInt32("Age", 21);

        await HttpContext.GetSession().CommitAsync();

        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var session = HttpContext.GetSession();
        await session.LoadAsync();
        var homeName = session.GetString("Home");
        var age = session.GetInt32("Age");

        return View("Privacy", (object)age!);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
