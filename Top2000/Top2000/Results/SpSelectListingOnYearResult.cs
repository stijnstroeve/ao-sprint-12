using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Top2000.Results
{
    public class SpSelectListingOnYearResult
    {
        public int SongID { get; set; }
        public string SongTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public int Rank { get; set; }

    }
}