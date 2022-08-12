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

namespace MusicProject.Mapper
{
    public class ProfileMapper:Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public ProfileMapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ProfileMapper()
        {
            CreateMap<SignUpViewModel, ApplicationUser>();
            CreateMap<AddMusicViewModel, Music>();
        }
    }
}
