using System.ComponentModel.DataAnnotations.Schema;

namespace MusicProject.Models
{
    public class MusicImage
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public int AlbomOrMusicId { get; set; }
        [ForeignKey("AlbomOrMusicId")]
        public Music Music { get; set; }

    }
}