using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Top2000.Models;

namespace Top2000.DAL
{
    public class SongContext : DbContext
    {

        public SongContext() : base("name=Top2000") { }

        public DbSet<Song> Songs { get; set; }
    }
}