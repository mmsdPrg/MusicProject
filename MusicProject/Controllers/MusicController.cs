using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProject.Data;
using MusicProject.Models;
using MusicProject.NextPreviousData;
using MusicProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MusicProject.Controllers
{
    public class MusicController : Controller
    {
        DB_Music db;
        private IWebHostEnvironment env { get; }
        public MusicController(DB_Music _db, IWebHostEnvironment env)
        {
            db = _db;
            this.env = env;
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
            List<int>IDArtist = new List<int>();
            foreach (var item in Object.Artists)
            {
                NameArtist.Add(item.Artist.ArtistName);
                IDArtist.Add(item.ArtistId);
            }
            string Artists =string.Join("-",NameArtist);
            return Json(new {MusicName= Object.Name,Img=Object.Imgs[0].ImgPath,Artists=Artists,MusicPath=Object.MusicPath320,idmusic=Object.Id});
        }
        public IActionResult NextMusic(int ? idmusic)
        {
           var Object = db.Music.Include(z => z.Imgs).Include(z => z.Artists).ThenInclude(z => z.Artist).OrderByDescending(z => z.Id).GetNext(db.Music.Find(idmusic));
            List<string> NameArtist = new List<string>();
            List<int> IDArtist = new List<int>();
            foreach (var item in Object.Artists)
            {
                NameArtist.Add(item.Artist.ArtistName);
                IDArtist.Add(item.ArtistId);
            }
            string Artists = string.Join("-", NameArtist);
            return Json(new { MusicName = Object.Name, Img = Object.Imgs[0].ImgPath, Artists = Artists, MusicPath = Object.MusicPath320, idmusic = Object.Id });
        }

        public IActionResult PrevMusic(int? idmusic)
        {
            var Object = db.Music.Include(z => z.Imgs).Include(z => z.Artists).ThenInclude(z => z.Artist).OrderByDescending(z => z.Id).GetPrevious(db.Music.Find(idmusic));
            if (Object == null)
            {
                Object= db.Music.Include(z => z.Imgs).Include(z => z.Artists).ThenInclude(z => z.Artist).OrderByDescending(z => z.Id).Last();
            }
            List<string> NameArtist = new List<string>();
            List<int> IDArtist = new List<int>();
            foreach (var item in Object.Artists)
            {
                NameArtist.Add(item.Artist.ArtistName);
                IDArtist.Add(item.ArtistId);
            }
            string Artists = string.Join("-", NameArtist);
            return Json(new { MusicName = Object.Name, Img = Object.Imgs[0].ImgPath, Artists = Artists, MusicPath = Object.MusicPath320, idmusic = Object.Id });
        }
        public IActionResult ArtistsPage()
        {
         
            ViewData["Artists"] = db.Artists.ToList();
            return View();
        }
        public JsonResult NewArtistFilter() => Json(db.Artists.OrderByDescending(z => z.id).ToList());

        // انتخاب شده از آهنگ
        public IActionResult Release(int ? Id)
        {
            ViewData["Music"] = db.Music.Include(z => z.Imgs).Include(x => x.Artists).ThenInclude(z=>z.Artist).FirstOrDefault(z=>z.Id==Id);
            var IdArtist = db.ArtistMusic.Where(z => z.MusicId == Id).Select(x=>x.ArtistId).ToList();
            ViewData["OtherMusic"] = db.ArtistMusic.Include(x => x.Music).ThenInclude(z => z.Imgs).Include(z => z.Artist).Where(z => IdArtist.Any(x=>x==z.ArtistId) && z.MusicId != Id).ToList();
            return View();
        }

        //public IActionResult ReleaseArtist(List<int>IdArtists)
        //{
        //    //ViewData["Music"] = db.Music.Include(z => z.Imgs).Include(x => x.Artists).ThenInclude(z => z.Artist).FirstOrDefault(z => z.Id == Id);
        //    //var IdArtist = db.ArtistMusic.FirstOrDefault(z => z.MusicId == Id).ArtistId;
        //    //ViewData["OtherMusic"] = db.ArtistMusic.Include(x => x.Music).ThenInclude(z => z.Imgs).Include(z => z.Artist).Where(z => z.ArtistId == IdArtist && z.MusicId != Id).ToList();
        //    return View();
        //}
        public IActionResult DownloadMusic(int ? id)
        {
            var Music = db.Music.FirstOrDefault(z => z.Id == id);
            string file = System.IO.Path.Combine(env.WebRootPath, "uploads", Music.MusicPath320);
            var FilesByte = System.IO.File.ReadAllBytes(file);
            return File(FilesByte, "audio/mpeg",Music.MusicPath320);
        }
    }
}
