using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top2000.DAL;
using Top2000.Models;

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
            var songs = db.GetSongList(id, parsedPage, PAGE_SIZE);

            // Set the viewbag params
            ViewBag.Year = id;

            return View(songs);
        }

    }
}
