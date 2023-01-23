using AutoMapper;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.Areas.Admin.IRepository
{
   public interface IArtist
    {
       Task <int> Create(AddArtistViewModel model,IMapper mapper);
        bool DeleteArtist(int ? id);
       Task<bool> Edit(int id,AddArtistViewModel model, IMapper mapper);

        bool DeleteImageArtist(int ? id);

    }
}
