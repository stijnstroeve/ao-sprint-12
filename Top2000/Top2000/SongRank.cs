//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Top2000
{
    using System;
    using System.Collections.Generic;
    
    public partial class SongRank
    {
        public int Rank { get; set; }
        public int Year { get; set; }
        public int SongID { get; set; }
    
        public virtual Song Song { get; set; }
    }
}
