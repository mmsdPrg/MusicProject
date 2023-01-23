using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        DB_Music db;
        public HomeController(DB_Music _db)
        {
            db = _db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["Artists"] = db.Artists.ToList();
            ViewData["RecentlyAdded"] = db.Music.Include(z => z.Artists).ThenInclude(z => z.Artist).Include(z => z.Imgs).OrderByDescending(z => z.Id).Take(11).ToList();
            return View();
        }
       
        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }

    }
}
