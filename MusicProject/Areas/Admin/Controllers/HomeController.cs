using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(policy: "AdminAccess")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
