using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BananaSocialNetwork.Models;
using System.IO;

namespace BananaSocialNetwork.Controllers
{
    public class PhotoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Photo/
        public ActionResult Index()
        {

            return View(db.Photos.ToList());
        }

        // GET: /Photo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: /Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Photo/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        private void SaveFile(string fileName, string contentType, Stream inputStream, string userEmail, int albumId)
        {
            string name = @"/server_imgs/" + userEmail + Convert.ToString(albumId) + fileName;
            string fileNamePath = Server.MapPath(name);
            using (var fileStream = System.IO.File.Create(fileNamePath))           // ТУТ ПУТЬ КУДА СОХРАНЯТЬ ФОТО ДЛЯ СЕРВА!!!!!!!!!!!!!!!!!!!!!!!!
            {
                inputStream.CopyTo(fileStream);
            }
        }

        [HttpPost]
        public ActionResult Create(Photo photo, int id, HttpPostedFileBase[] files)
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            Album album = db.Albums.Find(id);
            photo.Album = album;

            foreach (var file in files)
            {
                if (file != null)
                {
                    SaveFile(file.FileName, file.ContentType, file.InputStream, user.Email, id);
                    string name = @"/server_imgs/" + user.Email + Convert.ToString(id) + file.FileName;
                    photo.PhotoPath = name;
                    db.Photos.Add(photo);
                    db.SaveChanges();

                    News newNew = new News() { User = user, CreationTime = DateTime.Parse(DateTime.Now.ToString("d MMM yyyy HH:mm:ss")), NewsType = NewsType.AddPhoto, IdContent = photo.Id };
                    db.News.Add(newNew);
                    db.SaveChanges();
                }
            }


            return RedirectToAction("Details/" + id, "Album");
        }

        [HttpGet]
        public ActionResult AddPhotoPartial(int id)
        {
            ViewBag.Al_Id = id;
            return View();
        }

        [HttpGet]
        public ActionResult EditPhotoPartial(int id)
        {
            ViewBag.Ph_Id = id;
            Photo photo = db.Photos.Find(id);
            return View(photo);
        }

        [HttpGet]
        public ActionResult PhotoInMapPartial(int id)
        {
            Photo photo = db.Photos.Find(id);
            return View(photo);
        }

        // GET: /Photo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return View(photo);
        }

        // POST: /Photo/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Photo json)
        {
            Photo photo = db.Photos.Find(json.Id);
            photo.Adress = json.Adress;
            photo.GeoLat = json.GeoLat;
            photo.GeoLong = json.GeoLong;

            db.Entry(photo).State = EntityState.Modified;
            db.SaveChanges();
            return null;
        }

        // GET: /Photo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: /Photo/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            int al_id = photo.Album.Id;
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Details/" + al_id, "Album");
        }

        public ActionResult CommentsPartial(int? id)
        {
            Photo photo = db.Photos.Find(id);
            return PartialView(photo);
        }

        public ActionResult CommentsSend(int? id)
        {

            Photo photo = db.Photos.Find(id);

            return PartialView(photo);
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
