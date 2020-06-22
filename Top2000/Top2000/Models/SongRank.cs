using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Top2000.Models
{

    [Table("SongRank")]
    public class SongRank
    {
        [Key, Column(Order = 0)]
        public int Rank { get; set; }

        [Key, Column(Order = 1)]
        public int Year { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Song")]
        public int SongID { get; set; }
        public virtual Song Song { get; set; }
    }
}