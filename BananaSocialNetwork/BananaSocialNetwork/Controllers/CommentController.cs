using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BananaSocialNetwork.Models;

namespace BananaSocialNetwork.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult OneComment(Comment comment)
        {
            return View(comment);
        }


        [HttpPost]

        public ActionResult Create(string text, int Id)
        {
            Comment comment = new Comment();
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            Photo photo = db.Photos.Find(Id);

            comment.DateCreate = DateTime.Parse(DateTime.Now.ToString("d MMM yyyy HH:mm:ss"));
            comment.Text = text;
            comment.User = user;
            comment.Photo = photo;

            db.Comments.Add(comment);
            db.SaveChanges();

            News newNew = new News() { User = user, CreationTime = DateTime.Parse(DateTime.Now.ToString("d MMM yyyy HH:mm:ss")), NewsType = NewsType.AddComent, IdContent = comment.Id };
            db.News.Add(newNew);
            db.SaveChanges();


            return View("OneComment", comment);
        }

        public ActionResult CommentList(int Id)
        {

            Photo photo = db.Photos.Find(Id);
            photo.Coments = db.Comments.Where(m => m.Photo.Id == photo.Id).ToList();

            return View(photo);

        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,DateCreate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }


        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return null;
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
