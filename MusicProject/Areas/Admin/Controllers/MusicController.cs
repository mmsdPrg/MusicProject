using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicProject.Areas.Admin.IRepository;
using MusicProject.Areas.Admin.Repository;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

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

            }
            else
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
        public async Task<IActionResult> CreateMusic(AddMusicViewModel model, [FromServices] IMapper mapper)
        {
            int Success = await Repo.Create(model, mapper);
            if (Success == 1)
                TempData["Suc"] = "موزیک ثبت شد";
            return Redirect("/admin/home/index");
            
        }
        public IActionResult ListOfFile(int ? page)
        {
            int Pages = page ?? 1;
            int pageSize = 10;
            string path = Path.Combine(env.WebRootPath, "uploads");
            ViewData["File"]= Directory.GetFiles(path).ToPagedList(Pages,pageSize);
            return View();
        }
        public IActionResult DeleteFile(string Name)
        {
            string FullPath = Path.Combine(env.WebRootPath, "uploads", Name);
            if (System.IO.File.Exists(FullPath))
            {
                System.IO.File.Delete(FullPath);
                return Json(new { status = true });
            }
            return Json(new { status = false });

        }
        public IActionResult Test()
        {
            return View();
        }
        
    }
}
