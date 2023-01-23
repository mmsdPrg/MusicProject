using AutoMapper;
using MusicProject.Areas.Admin.ViewModel;
using MusicProject.Areas.Identity.Data;
using MusicProject.Models;
using MusicProject.ViewModel;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicProject.Data;

namespace MusicProject.Mapper
{
    public class ProfileMapper:Profile
    {
     
        public ProfileMapper()
        {
            CreateMap<SignUpViewModel, ApplicationUser>();
            CreateMap<AddMusicViewModel, Music>();
            CreateMap<AddArtistViewModel, Artist>();
        }
    }
}
