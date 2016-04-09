using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Music.Models;


namespace Music.Controllers
{
    public class AlbumsController : Controller
    {
        private MusicContext db = new MusicContext();

        // GET: Albums
        public ActionResult Index(string searchString, string sortOrder)
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);

            ViewBag.ArtistSortParam = String.IsNullOrEmpty(sortOrder) ? "artist_desc" : "";
            ViewBag.GenreSortParam = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewBag.TitleSortParam = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            switch (sortOrder)
            {
                case "artist_desc":
                    albums = albums.OrderByDescending(a => a.ArtistID);
                    break;
                case "Genre":
                    albums = albums.OrderBy(a => a.GenreID);
                    break;
                case "genre_desc":
                    albums = albums.OrderByDescending(a => a.GenreID);
                    break;
                case "Title":
                    albums = albums.OrderBy(a => a.Title);
                    break;
                case "title_desc":
                    albums = albums.OrderByDescending(a => a.Title);
                    break;
                case "Price":
                    albums = albums.OrderBy(a => a.Price);
                    break;
                case "price_desc":
                    albums = albums.OrderByDescending(a => a.Price);
                    break;

            }

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(a => a.Title.Contains(searchString));
            }



            return View(albums.ToList());
        }

        public ActionResult ViewGenres(String Genre)
        {
            var genres = db.Albums.Include(a => a.Genre);
            return View(genres.ToList());


        }

        public ActionResult ViewArtists(String Artist)
        {
            var artists = db.Albums.Include(a => a.Artist);
            return View(artists.ToList());


        }





        //public ActionResult Index(string sortOrder)
        //{
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
        //    ViewBag.DateSortParm = sortOrder == "Artist" ? "Genre" : "Price";
        //    var albums = from a in db.Albums
        //                   select a;
        //    switch (sortOrder)
        //    {
        //        case "Title":
        //            albums = albums.OrderByDescending(a => a.Title);
        //            break;
        //        case "Artist":
        //            albums = albums.OrderBy(a => a.Artist);
        //            break;
        //        case "Genre":
        //            albums = albums.OrderByDescending(a => a.Genre);
        //            break;
        //        default:
        //            albums = albums.OrderBy(a => a.Artist);
        //            break;
        //    }
        //    return View(albums.ToList());
        //}

        //public ActionResult List()
        //{
        //    IEnumerable<Album> album =
        //      _productService.GetAlbums();

        //    return View(album);
        //}

        public ActionResult BrowseAlbums(string genre)
        {
            var genreModel = db.Genres.Include("Albums")         
                .Single(g => g.Name == genre);  
            return View(genreModel);
        }



        public ActionResult ShowSomeAlbums(int id)
        {
            var albums = db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Where(a => a.GenreID == id);
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try {
                Album album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.AlbumID == id).Single();
            
                if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddGenre()
        {
            ViewBag.Genre = new SelectList(db.Genres.OrderByDescending(g => g.Name), "Name");
            return View();
        }

        public ActionResult AddArtist()
        {
            ViewBag.Artist = new SelectList(db.Artists.OrderByDescending(g => g.Name), "Name");
            return View();
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(db.Genres.OrderByDescending(g => g.Name), "GenreID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,Title,GenreID,Price,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumID,Title,GenreID,Price,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

















//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Music.Models;
////using PagedList;

//namespace Music.Controllers
//{
//    public class AlbumsController : Controller
//    {
//        private MusicContext db = new MusicContext();

//        // GET: Albums
//        public ActionResult Index(string searchString, string sortOrder/*, string currentFilter, int? page*/)
//        {
//            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);

//            ViewBag.CurrentSort = sortOrder;

//            ViewBag.ArtistSortParam = String.IsNullOrEmpty(sortOrder) ? "artist_desc" : "";
//            ViewBag.GenreSortParam = sortOrder == "Genre" ? "genre_desc" : "Genre";
//            ViewBag.TitleSortParam = sortOrder == "Title" ? "title_desc" : "Title";
//            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";


//            //if (searchString != null)
//            //{
//            //    page = 1;
//            //}
//            //else
//            //{
//            //    searchString = currentFilter;
//            //}

//            //ViewBag.CurrentFilter = searchString;



//            switch (sortOrder)
//            {
//                case "artist_desc":
//                    albums = albums.OrderByDescending(a => a.ArtistID);
//                    break;
//                case "Genre":
//                    albums = albums.OrderBy(a => a.GenreID);
//                    break;
//                case "genre_desc":
//                    albums = albums.OrderByDescending(a => a.GenreID);
//                    break;
//                case "Title":
//                    albums = albums.OrderBy(a => a.Title);
//                    break;
//                case "title_desc":
//                    albums = albums.OrderByDescending(a => a.Title);
//                    break;
//                case "Price":
//                    albums = albums.OrderBy(a => a.Price);
//                    break;
//                case "price_desc":
//                    albums = albums.OrderByDescending(a => a.Price);
//                    break;

//            }

//            if (!String.IsNullOrEmpty(searchString))
//            {
//                albums = albums.Where(a => a.Title.Contains(searchString));
//            }

//            //int pageSize = 3;
//            //int pageNumber = (page ?? 1);
//            //return View(albums.ToPagedList(pageNumber, pageSize));

//            return View(albums.ToList());
//        }

//        public ActionResult ViewGenres(String Genre)
//        {
//            var genres = db.Albums.Include(a => a.Genre);
//            return View(genres.ToList());


//        }

//        public ActionResult ViewArtists(String Artist)
//        {
//            var artists = db.Albums.Include(a => a.Artist);
//            return View(artists.ToList());


//        }

//        //public ActionResult Playlists(String Playlist)
//        //{
//        //    //var playlists = db.Albums.Include(p => p.Playlist);
//        //    return View(/*playlists.ToList()*/);


//        //}


//        //public ActionResult Index(string sortOrder)
//        //{
//        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
//        //    ViewBag.DateSortParm = sortOrder == "Artist" ? "Genre" : "Price";
//        //    var albums = from a in db.Albums
//        //                   select a;
//        //    switch (sortOrder)
//        //    {
//        //        case "Title":
//        //            albums = albums.OrderByDescending(a => a.Title);
//        //            break;
//        //        case "Artist":
//        //            albums = albums.OrderBy(a => a.Artist);
//        //            break;
//        //        case "Genre":
//        //            albums = albums.OrderByDescending(a => a.Genre);
//        //            break;
//        //        default:
//        //            albums = albums.OrderBy(a => a.Artist);
//        //            break;
//        //    }
//        //    return View(albums.ToList());
//        //}

//        //public ActionResult List()
//        //{
//        //    IEnumerable<Album> album =
//        //      _productService.GetAlbums();

//        //    return View(album);
//        //}

//        public ActionResult BrowseAlbums(string genre)
//        {
//            var genreModel = db.Genres.Include("Albums")
//                .Single(g => g.Name == genre);
//            return View(genreModel);
//        }



//        public ActionResult ShowSomeAlbums(int id)
//        {
//            var albums = db.Albums
//                .Include(a => a.Artist)
//                .Include(a => a.Genre)
//                .Where(a => a.GenreID == id);
//            return View(albums.ToList());
//        }

//        // GET: Albums/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            try
//            {
//                Album album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.AlbumID == id).Single();

//                if (album == null)
//                {
//                    return HttpNotFound();
//                }
//                return View(album);
//            }
//            catch (Exception)
//            {
//                return RedirectToAction("Index");
//            }
//        }

//        public ActionResult AddGenre()
//        {
//            ViewBag.Genre = new SelectList(db.Genres.OrderByDescending(g => g.Name), "Name");
//            return View();
//        }

//        public ActionResult AddArtist()
//        {
//            ViewBag.Artist = new SelectList(db.Artists.OrderByDescending(g => g.Name), "Name");
//            return View();
//        }

//        // GET: Albums/Create
//        public ActionResult Create()
//        {
//            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name");
//            ViewBag.GenreID = new SelectList(db.Genres.OrderByDescending(g => g.Name), "GenreID", "Name");
//            return View();
//        }

//        // POST: Albums/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "AlbumID,Title,GenreID,Price,ArtistID")] Album album)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Albums.Add(album);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
//            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
//            return View(album);
//        }

//        // GET: Albums/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Album album = db.Albums.Find(id);
//            if (album == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
//            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
//            return View(album);
//        }

//        // POST: Albums/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "AlbumID,Title,GenreID,Price,ArtistID")] Album album)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(album).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
//            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
//            return View(album);
//        }

//        // GET: Albums/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Album album = db.Albums.Find(id);
//            if (album == null)
//            {
//                return HttpNotFound();
//            }
//            return View(album);
//        }

//        // POST: Albums/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Album album = db.Albums.Find(id);
//            db.Albums.Remove(album);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
