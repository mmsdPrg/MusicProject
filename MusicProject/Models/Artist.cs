using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.Models
{
    public class Artist
    {
        public int id { get; set; }
        public string ArtistName { get; set; }
        public float Score { get; set; }
        public string ImagePath { get; set; }
        public string Biography { get; set; }
        public ICollection<ArtistMusic> Musics { get; set; }
    }
}
