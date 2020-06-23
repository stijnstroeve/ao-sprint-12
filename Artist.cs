using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Top2000.Models
{
    [Table("Artist")]
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public virtual ICollection<SongArtist> SongArtists { get; set; }
    }
}