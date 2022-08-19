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
        Task<int> Create(AddMusicViewModel Music, [FromServices] IMapper mapper);
        void Delete(int id);
        void Update(AddMusicViewModel Music,[FromServices] IMapper mapper);
        List<Music> List();
        Music GetById(int id);
    }
}
