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

        public int Create(AddMusicViewModel MusicViewModel, [FromServices]IMapper mapper)
        {
            Music music = mapper.Map<AddMusicViewModel, Music>(MusicViewModel);
            music.MusicPath320 = MusicViewModel.MusicFile.FileName;
            music.Created = DateTime.Now;
            dB.Add(music);
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
