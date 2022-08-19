using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MusicProject.Areas.Admin.IRepository;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Data;
using MusicProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MusicProject.Areas.Admin.Repository
{
    public class MusicRepository : IMusic
    {
        DB_Music dB;
        IHostingEnvironment WebRoot;
        public MusicRepository(DB_Music _dB, IHostingEnvironment _WebRoot)
        {
            dB = _dB;
            WebRoot = _WebRoot;
        }

        public async Task<int> Create(AddMusicViewModel model, [FromServices]IMapper mapper)
        {
            Music music = mapper.Map<AddMusicViewModel, Music>(model);
            music.MusicPath320 = model.MusicFile.FileName;
            music.Created = DateTime.Now;
            music.Description = model.Discription;
            dB.Add(music);
            dB.SaveChanges();
            MusicImage musicImage = new MusicImage
            {
                AlbomOrMusicId = music.Id,
                ImgPath = model.MusicCover.FileName,
            };

            string path = Path.Combine(WebRoot.WebRootPath, "ImagesCover", model.MusicCover.FileName);
            var exists = System.IO.File.Exists(path);

            FileStream file = new FileStream(path, FileMode.Create);
            await model.MusicCover.CopyToAsync(file);
            dB.Add(musicImage);
            if (dB.SaveChanges() != 0)
                return 1;
            else
                return -1;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Music GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Music> List()
        {
            throw new System.NotImplementedException();
        }

        public void Update(AddMusicViewModel Music, [FromServices] IMapper mapper)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
