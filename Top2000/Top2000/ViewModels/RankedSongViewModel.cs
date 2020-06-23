
using System;
using System.Collections.Generic;

namespace Top2000.ViewModels
{
    public class RankedSongViewModel
    {
        public string SongTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ExternalImageUrl { get; set; }
        public string ExternalSampleUrl { get; set; }
        public List<string> Artists { get; set; }
        public int Rank { get; set; }
        public RankProgress Progress { get; set; }
    }
}