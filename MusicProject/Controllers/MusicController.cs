using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProject.Data;
using MusicProject.Models;
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

        public IActionResult CountOfPlays(int? Id)
        {
            if(Id!=null || Id != 0)
            {
                var Music = db.Music.FirstOrDefault(z => z.Id == Id);
                Music.CountOfPlays++;
                db.SaveChanges();
                return Json(new { counts = Music.CountOfPlays });
            }
            return Json(new { counts = 0 });
          
            
        }
        public IActionResult ArtistPage(int id)
        {
            Artist artist = db.Artists.Include(z => z.Musics).ThenInclude(z => z.Music).ThenInclude(z => z.Imgs).FirstOrDefault(z => z.id == id);
            return View(artist);
        }
        public JsonResult LastMusic()
        {
            var Object = db.Music.Include(z=>z.Imgs).Include(z=>z.Artists).ThenInclude(z=>z.Artist).OrderBy(z => z.Created).ToList().LastOrDefault();
            List<string> NameArtist = new List<string>();
            foreach (var item in Object.Artists)
            {
                NameArtist.Add(item.Artist.ArtistName);
            }
            string Artists =string.Join("-",NameArtist);
            return Json(new {MusicName= Object.Name,Img=Object.Imgs[0].ImgPath,Artists=Artists,MusicPath=Object.MusicPath320});
        }
        public IActionResult ArtistsPage()
        {
            return View();
        }
    }
}
