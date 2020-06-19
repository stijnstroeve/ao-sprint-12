using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top2000.DAL;
using Top2000.Models;

namespace Top2000.Controllers
{
    public class SongController : Controller
    {
        const int PAGE_SIZE = 20;

        private SongContext db = new SongContext();

        // GET: Song
        public ActionResult Index(string page)
        {
            int parsedPage;
            
            // Make sure the page query param is an integer
            if(!Int32.TryParse(page, out parsedPage))
            {
                // If the page is not an int, go to the first page
                parsedPage = 1;
            }

            // Make sure the minimum page is 1
            if (parsedPage < 1) parsedPage = 1;

            List<Song> songs = db.Songs.ToList();

            var paginatedSongs = songs.GetRange((parsedPage - 1) * PAGE_SIZE, PAGE_SIZE);

            return View(paginatedSongs);
        }

        // GET: Song/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Song/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Song/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Song/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Song/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Song/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Song/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
