using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProject.Areas.Admin.IRepository;
using MusicProject.Areas.Admin.Repository;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Data;
using MusicProject.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MusicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MusicController : Controller
    {
        IWebHostEnvironment env;
        IMusic Repo;
        IArtist ArtRepo;
        DB_Music db;
        public MusicController(IWebHostEnvironment env, IMusic _Rpo, IArtist _ArtRepo, DB_Music db)
        {
            this.db = db;
            this.env = env;
            ArtRepo = _ArtRepo;
            Repo = _Rpo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadMuiscFile(IFormFile filearray, string path)
        {
            string Paths = Path.Combine(env.WebRootPath, "uploads", filearray.FileName);
            var exists = System.IO.File.Exists(Paths);
            if (exists == true)
            {
                return Json(new { status = "duplicate", imagename = "",pathMusic=Paths });
            }
            else
            {
                var fileStream = new FileStream(Paths, FileMode.Create);
                await filearray.CopyToAsync(fileStream);
                TempData["MSG"] = "ذخیره شد";
                HttpContext.Session.SetString("MusicPath", filearray.FileName);
                return Json(new { status = "success", imagename = "" ,pathMusic=Paths});
            }

        }
        public IActionResult Create()
        {
            ViewData["Artist"] = db.Artists.ToList();
            return View();
        }
        public async Task<IActionResult> CreateMusic(AddMusicViewModel model, [FromServices] IMapper mapper, List<string> Artists,string TimeMusic)
        {
            model.TimeMusicDuration = TimeMusic;
            int Success = await Repo.Create(model, mapper, Artists);
            if (Success == 1)
                TempData["Suc"] = "موزیک ثبت شد";
            return Redirect("/admin/home/index");

        }
        public IActionResult ListOfFile(int? page)
        {
            int Pages = page ?? 1;
            int pageSize = 10;
            string path = Path.Combine(env.WebRootPath, "uploads");
            ViewData["File"] = Directory.GetFiles(path).ToPagedList(Pages, pageSize);
            ViewData["IsInDataBase"] = db.Music.ToList();
            ViewData["Artists"] = db.ArtistMusic.Include(z => z.Artist).Include(z => z.Music).ToList();
            return View();
        }
        public IActionResult DeleteFile(string Name)
        {
            string FullPath = Path.Combine(env.WebRootPath, "uploads", Name);
            if (System.IO.File.Exists(FullPath))
            {
                System.IO.File.Delete(FullPath);
                Repo.Delete(Name);
                return Json(new { status = true });
            }
            return Json(new { status = false });

        }
        public IActionResult DeleteImg(string Name)
        {
            string FullPath = Path.Combine(env.WebRootPath, "ImagesCover", Name);
            if (System.IO.File.Exists(FullPath))
            {
                System.IO.File.Delete(FullPath);
                Repo.Delete(Name);
                return Json(new { status = true });
            }
            return Json(new { status = false });

        }

        public IActionResult EditMusic(int id)
        {
            ViewData["Artist"] = db.Artists.ToList();
            return View(db.Music.Include(z => z.Imgs).FirstOrDefault(z => z.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> EditMusic(int id, AddMusicViewModel model, [FromServices] IMapper mapper, List<string> Artists)
        {
            await Repo.Update(id, model, mapper, Artists);
            TempData["Suc"] = "موزیک ویرایش شد";
            return Redirect("/admin/home/index");
        }
        public JsonResult DeleteImageMusic(int id)
        {
            return Json(Repo.DeleteImage(id));
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult ListOfArtist(int? page)
        {
            int Pages = page ?? 1;
            int pageSize = 10;
            var artists = db.Artists.ToPagedList(Pages, pageSize);
            ViewData["CountOfAlbum"] = db.ArtistMusic.ToList();
            return View(artists);
        }
        public JsonResult DeleteArtist(int? Id)
        {
            var IsDelete = ArtRepo.DeleteArtist(Id);
            if (IsDelete == true)
                return Json(true);
            else
                return Json(false);

        }
        public IActionResult EditArtist(int? id)
        {
            return View(db.Artists.FirstOrDefault(z => z.id == id));
        }
        [HttpPost]
        public async Task< IActionResult> EditArtist(int id, AddArtistViewModel model, [FromServices] IMapper mapper)
        {
            if (await ArtRepo.Edit(id, model, mapper))
            {
                TempData["Suc"] = "هنرمند ویرایش شد";
                return Redirect("/admin/home/index");
            }
            TempData["Failed"] = "عملیات با شکست مواجه شد";
            return Redirect("/admin/home/index");
        }
        public IActionResult CreateArtist()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateArtist(AddArtistViewModel model, [FromServices] IMapper mapper)
        {
            int result = await ArtRepo.Create(model, mapper);
            if (result != 0)
                TempData["Suc"] = "خواننده ثبت شد";
            else
                TempData["Failed"] = "خواننده ثبت شد";

            return RedirectToAction("ListOfArtist", "Music");
        }
        public JsonResult DeleteImageArtist(int id)
        {
            var Result = ArtRepo.DeleteImageArtist(id);
            if (Result)
                return Json(new {Icon="success", text="با موفقیت انجام شد" });
            else
                return Json(new { Icon = "error", text = "مشکلی به وجود امده است" });
        }
        public IActionResult FindDupArtist(string ArtistName)
        {
            if (db.Artists.Any(z => z.ArtistName == ArtistName))
                return Json(false);
            return Json(true);
        }
        public IActionResult ListOfCovers(int? page)
        {
            int Pages = page ?? 1;
            int pageSize = 10;
            string path = Path.Combine(env.WebRootPath, "ImagesCover");
            ViewData["File"] = Directory.GetFiles(path).ToPagedList(Pages, pageSize);
            ViewData["Artists"] = db.MusicImage.Include(z => z.Music).ToList();
            ViewData["IsInDataBase"] = db.MusicImage.ToList();
            return View();
        }


    }
}
