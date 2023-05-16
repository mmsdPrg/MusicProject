using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicProject.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MusicPath320 { get; set; }
        public string Description { get; set; }
        public List<MusicImage> Imgs { get; set; }
        public double CountOfPlays { get; set; }
        public DateTime Created { get; set; }
        public ICollection<ArtistMusic> Artists { get; set; }
        public string MusicDuration { get; set; }
    }
}
