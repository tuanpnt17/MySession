using Microsoft.AspNetCore.Mvc;

namespace MySession.Controllers
{
    public class TestController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var randomValue = Guid.NewGuid().ToString();
            var session = HttpContext.GetSession();
            await session.LoadAsync();

            session.SetString("KEY", randomValue);
            await session.CommitAsync();
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
