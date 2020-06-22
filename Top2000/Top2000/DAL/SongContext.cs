﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Top2000.Models;
using Top2000.ViewModels;

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

            //modelBuilder.Entity<SongRank>()
            //    .HasKey(sc => new { sc.SongID });

            modelBuilder.Entity<Song>()
                .HasMany(c => c.SongArtists)
                .WithRequired()
                .HasForeignKey(c => c.SongID);

            modelBuilder.Entity<Song>()
                .HasMany(c => c.SongRanks)
                .WithRequired()
                .HasForeignKey(c => c.SongID);

            modelBuilder.Entity<Artist>()
                .HasMany(c => c.SongArtists)
                .WithRequired()
                .HasForeignKey(c => c.ArtistID);

            modelBuilder.Entity<RankedSongViewModel>();

        }

        public virtual ObjectResult<RankedSongViewModel> spSelectListingOnYear(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RankedSongViewModel>("spSelectListingOnYear", yearParameter);
        }

        public List<Song> GetSongList(int year, int page, int pageSize)
        {

            // List all songs and paginate them
            List<Song> songs = Songs
                .Where(x => x.SongRanks.Any(y => y.Year == year))
                .OrderBy(x => x.SongRanks.Where(y => y.Year == year).FirstOrDefault().Rank)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return songs;
        }
    }
}