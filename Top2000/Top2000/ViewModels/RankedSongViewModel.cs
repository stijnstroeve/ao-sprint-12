using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Top2000.Models;

namespace Top2000.ViewModels
{
    public class RankedSongViewModel
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Song")]
        public int SongID { get; set; }
        public Song Song { get; set; }
        public int Rank { get; set; }
    }
}