using Microsoft.AspNetCore.Mvc;

namespace MySession.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var randomValue = Guid.NewGuid().ToString();
            var session = HttpContext.GetSession();
            session.SetString("KEY", randomValue);
            session.CommitAsync();
            return View();
        }

        public async Task<IActionResult> GetSessionAsync(string key = "KEY")
        {
            var session = HttpContext.GetSession();
            await session.LoadAsync();
            return Content(session.GetString(key) ?? string.Empty);
        }
    }
}
