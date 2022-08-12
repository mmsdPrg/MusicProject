using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MusicProject.Areas.Admin.ViewModel
{
    public class AddMusicViewModel
    {
        [Required(ErrorMessage ="نام آهنگ ضروری است")]
        public string Name { get; set; }
        [Required(ErrorMessage = "فایل آهنگ ضروری است")]
        public IFormFile MusicFile { get; set; }
        public IFormFile MusicCover  { get; set; }

        [Required(ErrorMessage = "خواننده آهنگ ضروری است")]
        public string Artist  { get; set; }
        public string Discription  { get; set; }
    }
}
