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
        public DbSet<SongArtist> SongArtists { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongArtist>()
                .HasKey(sc => new { sc.SongID, sc.ArtistID });

            modelBuilder.Entity<Song>()
                .HasMany(c => c.SongArtists)
                .WithRequired()
                .HasForeignKey(c => c.SongID);

            modelBuilder.Entity<Artist>()
                .HasMany(c => c.SongArtists)
                .WithRequired()
                .HasForeignKey(c => c.ArtistID);
        }
    }
}