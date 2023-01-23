using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MusicProject.Areas.Admin.IRepository;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Data;
using MusicProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.Areas.Admin.Repository
{
    public class ArtistRepository : IArtist
    {
        DB_Music dB;
        IWebHostEnvironment root;
        public ArtistRepository(DB_Music _dB, IWebHostEnvironment _root)
        {
            dB = _dB;
            root = _root;
        }
        public async Task<int> Create(AddArtistViewModel model, IMapper mapper)
        {
            Artist artist = mapper.Map<Artist>(model);
            string FolderPath = Path.Combine(root.WebRootPath, "ArtistFile");
            if (Directory.Exists(FolderPath) == false)
            {
                Directory.CreateDirectory(FolderPath);
            }
            string PathImage = Path.Combine(root.WebRootPath, "ArtistFile", model.ArtistCover.FileName);
            var filestream = new FileStream(PathImage, FileMode.Create);
            await model.ArtistCover.CopyToAsync(filestream);
            artist.ImagePath = model.ArtistCover.FileName;
            dB.Add(artist);
            if (dB.SaveChanges() != 0)
                return 1;
            return 0;

        }

        public bool DeleteArtist(int? id)
        {
            if (id != 0)
            {
                //foreach (var item in dB.ArtistMusic.Where(z => z.ArtistId == id))
                //{
                //    dB.Remove(item);
                //    dB.SaveChanges();
                //}
                Artist artist = dB.Artists.FirstOrDefault(z => z.id == id);
                dB.Remove(artist);
                dB.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteImageArtist(int? id)
        {
            Artist artist = dB.Artists.FirstOrDefault(z => z.id == id);
            if (artist != null)
            {
                if (artist.ImagePath != null || artist.ImagePath != "")
                {
                    string PaTh = Path.Combine(root.WebRootPath, "ArtistFile", artist.ImagePath);
                    System.IO.File.Delete(PaTh);
                    artist.ImagePath = null;
                    dB.SaveChanges();
                }
                return true;
            }
            return false;

        }

        public async Task<bool> Edit(int id, AddArtistViewModel model, IMapper mapper)
        {
            if (id != 0)
            {
                Artist artist = dB.Artists.Find(id);
                artist = mapper.Map(model, artist);
                if (model.ArtistCover != null)
                {
                    string Paths = Path.Combine(root.WebRootPath, "ArtistFile", model.ArtistCover.FileName);
                    var filestream = new FileStream(Paths, FileMode.Create);
                    await model.ArtistCover.CopyToAsync(filestream);
                }
                if (model.ArtistCover != null)
                    artist.ImagePath = model.ArtistCover.FileName;
                dB.Update(artist);
                dB.SaveChanges();
                return true;
            }
            return false;

        }
    }
}
