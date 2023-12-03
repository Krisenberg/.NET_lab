using Microsoft.AspNetCore.Mvc;

namespace List_08.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Solve(int a, int b, int c)
        {
            ViewBag.aParameter = a;
            ViewBag.bParameter = b;
            ViewBag.cParameter = c;
            return View();
        }
    }
}
