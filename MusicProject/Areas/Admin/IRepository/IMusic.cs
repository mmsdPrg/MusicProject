using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicProject.Areas.Admin.IRepository
{
    public interface IMusic
    {
        Task<int> Create(AddMusicViewModel Music, [FromServices] IMapper mapper,List<string>Artists);
        void Delete(string Name);
        Task Update(int id,AddMusicViewModel Music,[FromServices] IMapper mapper,List<string> Artists);
        List<Music> List();
        Music GetById(int id);
        bool DeleteImage(int id);
    }
}
