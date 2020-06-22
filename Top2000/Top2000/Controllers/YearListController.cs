using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Top2000.DAL;
using Top2000.Models;
using Top2000.ViewModels;

namespace Top2000.Controllers
{
    public class YearListController : Controller
    {
        const int PAGE_SIZE = 20;

        private SongContext db = new SongContext();

        // GET: Song
        public ActionResult List(int id, string page)
        {
            // TODO: Max page
            int parsedPage;
            
            // Make sure the page query param is an integer
            if(!int.TryParse(page, out parsedPage))
            {
                // If the page is not an int, go to the first page
                parsedPage = 1;
            }

            // Make sure the minimum page is 1
            if (parsedPage < 1) parsedPage = 1;

            // List all songs by page
            IEnumerable<Song> songs = db.GetSongList(id, parsedPage, PAGE_SIZE);

            // Set the viewbag params
            ViewBag.Year = id;

            // Create the view models
            IEnumerable<RankedSongViewModel> viewModels = songs.Select(song =>
            {
                int rank = song.SongRanks.Single(y => y.Year == ViewBag.Year).Rank;

                RankProgress progress;

                try
                {
                    progress = new RankProgress
                    {
                        Difference = song.SongRanks.Single(y => y.Year == ViewBag.Year - 1).Rank - rank,
                        IsNew = false
                    };
                }
                catch (InvalidOperationException e)
                {
                    progress = new RankProgress
                    {
                        Difference = 0,
                        IsNew = true
                    };
                }

                return new RankedSongViewModel
                {
                    SongTitle = song.SongTitle,
                    ReleaseDate = song.ReleaseDate,
                    ExternalImageUrl = song.ExternalImageUrl,
                    ExternalSampleUrl = song.ExternalSampleUrl,
                    Rank = rank,
                    Artist = String.Join(" & ", song.SongArtists.Select(kvp => kvp.Artist.ArtistName)),
                    Progress = progress
                };
            });

            return View(viewModels);
        }

    }
}
