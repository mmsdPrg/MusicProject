using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.Areas.Admin.ViewModel
{
    public class AddArtistViewModel
    {
        [Required(ErrorMessage ="نام خواننده ضروری است")]
        [Remote("FindDupArtist","Music",ErrorMessage ="خواننده تکراری می باشد")]
        public string ArtistName { get; set; }
        public string Biography { get; set; }
        public IFormFile ArtistCover { get; set; }

        [Required(ErrorMessage = "ایدی اینستاگرام اجباری است")]
        public string InstagramId { get; set; }
    }
}
