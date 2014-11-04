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
        public ActionResult Create(Photo photo, int id, HttpPostedFileBase[] file1)
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            Album album = db.Albums.Find(id);
            photo.Album = album;



            foreach (var file in file1)
            {

                SaveFile(file.FileName, file.ContentType, file.InputStream, user.Email, id);
                string name = @"/server_imgs/" + user.Email + Convert.ToString(id) + file.FileName;
                photo.PhotoPath = name;
                db.Photos.Add(photo);
                db.SaveChanges();

            }
           

          // return RedirectToAction("Index");


            return RedirectToAction("Index", "Album");
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PhotoPath,Adress,GeoLong,GeoLat")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
