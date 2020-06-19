using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Top2000.Models
{
    [Table("SongArtist")]
    public class SongArtist
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Song")]
        public int SongID { get; set; }
        public virtual Artist Song { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Artist")]
        public int ArtistID { get; set; }
        public virtual Artist Artist { get; set; }

    }
}