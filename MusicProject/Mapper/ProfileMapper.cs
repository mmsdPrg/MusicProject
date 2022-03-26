using AutoMapper;
using MusicProject.Areas.Identity.Data;
using MusicProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.Mapper
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<SignUpViewModel, ApplicationUser>();
        }
    }
}
