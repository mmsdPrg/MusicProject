using System;
using System.Collections.Generic;

namespace MusicProject.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MusicPath320 { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        public List<MusicImage> Imgs { get; set; }
        public DateTime Created { get; set; }
    }
}
