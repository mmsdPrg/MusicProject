using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class MusicRepository : IMusic
    {
        DB_Music dB;
        IHostingEnvironment WebRoot;
        public MusicRepository(DB_Music _dB, IHostingEnvironment _WebRoot)
        {
            dB = _dB;
            WebRoot = _WebRoot;
        }

        public async Task<int> Create(AddMusicViewModel model, [FromServices] IMapper mapper, List<string> Artists)
        {
            #region AddMuisc & ImageMusic & Artist
            Music music = mapper.Map<AddMusicViewModel, Music>(model);
            music.MusicPath320 = model.MusicFile.FileName;
            music.Created = DateTime.Now;
            music.Description = model.Discription;
            dB.Add(music);
            dB.SaveChanges();
            foreach (var item in Artists)
            {

                ArtistMusic musics_Artists = new ArtistMusic
                {
                    ArtistId = Convert.ToInt32(item),
                    MusicId = music.Id,
                };
                dB.Add(musics_Artists);
                dB.SaveChanges();
            }
            MusicImage musicImage = new MusicImage
            {
                AlbomOrMusicId = music.Id,
                ImgPath = model.MusicCover.FileName,
            };
            dB.Add(musicImage);
            dB.SaveChanges();
            #endregion
            #region Save PathMusicCover
            string path = Path.Combine(WebRoot.WebRootPath, "ImagesCover", model.MusicCover.FileName);
            FileStream file = new FileStream(path, FileMode.Create);
            await model.MusicCover.CopyToAsync(file);
            #endregion

            if (dB.SaveChanges() != 0)
                return 1;
            else
                return -1;
        }

        public void Delete(string Name)
        {
            var Music = dB.Music.FirstOrDefault(z => z.MusicPath320 == Name);
            if (Music != null)
            {
                dB.Remove(Music);
                dB.SaveChanges();
            }
        }

        public bool DeleteImage(int id)
        {
            MusicImage image = dB.MusicImage.Find(id);
            if (image != null)
            {
                dB.Remove(image);
                dB.SaveChanges();
                return true;
            }
            return false;
        }

        public Music GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Music> List()
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(int id, AddMusicViewModel Model, [FromServices] IMapper mapper, List<string> Artists)
        {
            Music music = dB.Music.Include(z => z.Artists).Include(z => z.Imgs).FirstOrDefault(z => z.Id == id);
            if (Model.MusicFile != null)
                music.MusicPath320 = Model.MusicFile.FileName;
            music.Created = DateTime.Now;
            music.Name = Model.Name;
            music.Description = Model.Discription;
            dB.Update(music);
            if (music.Artists != null)
            {
                foreach (var item in music.Artists)
                {
                    dB.Remove(item);
                    dB.SaveChanges();
                }
            }

            foreach (var item in Artists)
            {
                ArtistMusic musics_Artists = new ArtistMusic
                {
                    ArtistId = Convert.ToInt32(item),
                    MusicId = music.Id,
                };
                dB.Add(musics_Artists);
                dB.SaveChanges();
            }
            if (Model.MusicCover!= null)
            {
                if (music.Imgs != null)
                {
                    foreach (var item in music.Imgs)
                    {
                        dB.Remove(item);
                        dB.SaveChanges();
                    }
                }
                MusicImage musicImage = new MusicImage
                {
                    AlbomOrMusicId = music.Id,
                    ImgPath = Model.MusicCover.FileName,
                };
                dB.Add(musicImage);
                dB.SaveChanges();
                string path = Path.Combine(WebRoot.WebRootPath, "ImagesCover", Model.MusicCover.FileName);
                FileStream file = new FileStream(path, FileMode.Create);
                await Model.MusicCover.CopyToAsync(file);
            }
          
        }
    }
}
