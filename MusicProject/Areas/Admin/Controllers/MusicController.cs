using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicProject.Areas.Admin.IRepository;
using MusicProject.Areas.Admin.Repository;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Models;
using System.IO;
using System.Threading.Tasks;

namespace MusicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MusicController : Controller
    {
        IWebHostEnvironment env;
        IMusic Repo;
        public MusicController(IWebHostEnvironment env, IMusic _Rpo)
        {
            this.env = env;
            //this.Repo = _Rpo;
            Repo=_Rpo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadMuiscFile(IFormFile filearray, string path)
        {
            string Paths = Path.Combine(env.WebRootPath, "uploads", filearray.FileName);
            var exists = System.IO.File.Exists(Paths);
            if (exists==true)
            {
                return Json(new { status = "duplicate", imagename = "" });

            }else
            {
                var fileStream = new FileStream(Paths, FileMode.Create);
                await filearray.CopyToAsync(fileStream);
                TempData["MSG"] = "ذخیره شد";
                HttpContext.Session.SetString("MusicPath", filearray.FileName);
                return Json(new { status = "success", imagename = "" });
            }
            
        }
        public IActionResult Create()
        {
            //int Success = Repo.Create(model, mapper);
            //if (Success == 1)
            //    TempData["Suc"] = "موزیک ثبت شد";
            return View();
        }
        public IActionResult CreateMusic(AddMusicViewModel model, [FromServices] IMapper mapper)
        {
            int Success = Repo.Create(model, mapper);
            if (Success == 1)
                TempData["Suc"] = "موزیک ثبت شد";
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        //public IActionResult UploadImageFile(IFormFile data, string path)
        //{
        //    return Json(new {status= "success" });
        //}
    }
}
