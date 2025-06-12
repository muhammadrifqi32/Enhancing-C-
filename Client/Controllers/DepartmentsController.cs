using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            //    return RedirectToAction("Index", "Accounts"); // redirect ke login kalau belum login
            return View();
        }
    }
}
