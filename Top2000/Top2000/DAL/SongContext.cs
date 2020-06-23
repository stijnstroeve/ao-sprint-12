using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Top2000.DAL
{
    public class SongContext : DbContext
    {

        //public SongContext() : base("name=Top2000") { }

        //public DbSet<Song> Songs { get; set; }
        //public DbSet<SongArtist> SongArtists { get; set; }
        //public DbSet<SongRank> SongRanks { get; set; }
        //public DbSet<Artist> Artists { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SongArtist>()
        //        .HasKey(sc => new { sc.SongID, sc.ArtistID });

        //    modelBuilder.Entity<Song>()
        //        .HasMany(c => c.SongArtists)
        //        .WithRequired()
        //        .HasForeignKey(c => c.SongID);

        //    modelBuilder.Entity<Song>()
        //        .HasMany(c => c.SongRanks)
        //        .WithRequired()
        //        .HasForeignKey(c => c.SongID);

        //    modelBuilder.Entity<Artist>()
        //        .HasMany(c => c.SongArtists)
        //        .WithRequired()
        //        .HasForeignKey(c => c.ArtistID);
        //}

        //public List<Song> GetSongList(int year, int page, int pageSize)
        //{

        //    // List all songs and paginate them
        //    //List<Song> songs = Songs
        //        //.Where(x => x.SongRanks.Any(y => y.Year == year))
        //        //.OrderBy(x => x.SongRanks.Where(y => y.Year == year).FirstOrDefault().Rank)
        //        //.Skip((page - 1) * pageSize)
        //        //.Take(pageSize)
        //        //.ToList();

        //    var result = SongRanks
        //            .Where(rank => rank.Year == year)
        //            .OrderBy(rank => rank.Rank)
        //            .Select(rank => new SongViewModel
        //            {
        //                Rank = rank.Rank,
        //                Title = rank.Song.SongTitle,
        //                Artists = rank.Song.SongArtists
        //                    .Select(artist => artist.)
        //                    .ToList(),
        //                Year = rank.Song.ReleaseDate.Year
        //            })
        //            .ToList()
        //            .ToPagedList(page, pageSize);


        //    return result;
        //}
    }
}