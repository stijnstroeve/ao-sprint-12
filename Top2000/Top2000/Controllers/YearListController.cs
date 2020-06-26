using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Top2000.ViewModels;

namespace Top2000.Controllers
{
    public class YearListController : Controller
    {
        // The max amount of items in a page
        const int PAGE_SIZE = 20;

        public ActionResult Index()
        {
            int latestYear = GetLatestYear();
            return RedirectToAction("List", new { id = latestYear });
        }

        // GET: YearList/YEAR
        public ActionResult List(int id, int? page)
        {
            // Used for generation urls for pagination
            Func<int, string> PageUrlGenerator = (newPage) => Url.Action("List", new
            {
                page = newPage
            });


            // Set the viewbag params
            ViewBag.Year = id;
            ViewBag.PageUrlGenerator = PageUrlGenerator;
            ViewBag.DistinctYears = GetDistinctYears();

            // Create the view models
            var viewModels = getRankedSongModels(ViewBag.Year, page ?? 1);

            return View(viewModels);
        }

        /// <summary>
        /// Gets all songs ranked by the given year 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private IPagedList<RankedSongViewModel> getRankedSongModels(int year, int page)
        {
            // First fetch all distinct years from the database
            var distinctYears = GetDistinctYears();

            // Get all songs by their year and map it to a view model
            using (var db = new EntityContext())
            {
                var result = db.SongRank
                    .Where(rank => rank.Year == year)
                    .OrderBy(rank => rank.Rank)
                    .Select(rank => new RankedSongViewModel
                    {
                        SongTitle = rank.Song.SongTitle,
                        ReleaseDate = rank.Song.ReleaseDate,
                        ExternalImageUrl = rank.Song.ExternalImageUrl,
                        ExternalSampleUrl = rank.Song.ExternalSampleUrl,
                        Rank = rank.Rank,
                        Artists = (List<string>)rank.Song.Artist.Select(kvp => kvp.ArtistName),
                    })
                    .ToList()
                    .ToPagedList(page, PAGE_SIZE);
                return result;
            }
        }

        /// <summary>
        /// Gets the latest year of the all years
        /// </summary>
        /// <returns></returns>
        private int GetLatestYear()
        {
            return GetDistinctYears().Max();
        }

        /// <summary>
        /// Gets all distinct years from the database
        /// </summary>
        /// <returns>List of years</returns>
        private List<int> GetDistinctYears()
        {
            using (var db = new EntityContext())
            {
                var result = db.SongRank
                    .Select(x => x.Year)
                    .Distinct() // Makes sure only unique values get selected
                    .OrderByDescending(x => x)
                    .ToList();
                return result;
            }
        }
    }
}
