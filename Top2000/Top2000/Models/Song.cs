using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Top2000.Models
{
    [Table("Song")]
    public class Song
    {
        [Key]
        public int SongID { get; set; }

        [Required]
        public string SongTitle { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public string ExternalImageUrl { get; set; }

        public string ExternalSampleUrl { get; set; }

        public virtual ICollection<SongArtist> SongArtists { get; set; }
    }
}