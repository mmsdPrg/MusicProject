using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicProject.Areas.Admin.ViewModel
{
    public class AddMusicViewModel
    {
        [Required(ErrorMessage ="نام آهنگ ضروری است")]
        public string Name { get; set; }
        [Required(ErrorMessage = "فایل آهنگ ضروری است")]
        public IFormFile MusicFile { get; set; }
        [Required(ErrorMessage ="کاور موسیقی الزامی می باشد")]
        public IFormFile MusicCover  { get; set; }
        //[Required(ErrorMessage = "خواننده آهنگ ضروری است")]
        //public List<string> Artists  { get; set; }
        public string Discription  { get; set; }
        public string TimeMusicDuration { get; set; }
    }
}
