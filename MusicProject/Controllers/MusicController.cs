using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProject.Data;
using MusicProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.Controllers
{
    public class MusicController : Controller
    {
        DB_Music db;
        public MusicController(DB_Music _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult MusicDetail(int ? Id)
        {
            var Music = db.Music.Include(z => z.Artists).ThenInclude(z=>z.Artist).FirstOrDefault(z => z.Id == Id);
            var Image = db.MusicImage.FirstOrDefault(z => z.AlbomOrMusicId == Id).ImgPath;
            string Joins = "";
            foreach (var item in Music.Artists)
            {
                List<string> Names = new List<string>();
                Names.Add(item.Artist.ArtistName);
                Joins = string.Join(',', Names);
            }
            return Json(new { id = Id, name = Music.Name,musicRoot=Music.MusicPath320, artists = Joins,imagePath=Image});
        }
    }
}
