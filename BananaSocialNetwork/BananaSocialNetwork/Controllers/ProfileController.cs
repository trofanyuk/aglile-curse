using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BananaSocialNetwork.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace BananaSocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
       
        //
        // GET: /Default/
        public ActionResult Index()
        {
            User user = db.Users.ToList().Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            user.Albums = db.Albums.ToList().Where(m => m.User.Id == user.Id).OrderByDescending(t => t.DateCreate);
            user.Friends = db.Friends.ToList().Where(m => m.user.Id == user.Id);

            for (int i = 0; i < user.Albums.Count(); i++)
            {
                Album album = user.Albums.ElementAt(i);
                album.Photos = db.Photos.ToList().Where(m => m.Album.Id == album.Id);
            }
            return View(user);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {

            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit");
        }

        [Authorize]
        public string GetAutorizeUser()
        {
            string login = User.Identity.GetUserName();
            var user = (from usr in db.Users where usr.Email == login select usr).First();
            var jsonUser = JsonConvert.SerializeObject(user);
            return jsonUser;
        }

        private void SaveFile(string fileName, string contentType, Stream inputStream, string userEmail)
        {
            string name = @"/server_imgs/" + userEmail + fileName;
            string fileNamePath = Server.MapPath(name);
            using (var fileStream = System.IO.File.Create(fileNamePath))
            {
                inputStream.CopyTo(fileStream);
            }
        }

        [HttpPost]
        public ActionResult AddAvatar(HttpPostedFileBase file)
        {

            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            if (file != null)
            {
                SaveFile(file.FileName, file.ContentType, file.InputStream, user.Email);
                string name = @"/server_imgs/" + user.Email + file.FileName;
                user.AvatatPath = name;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Edit");
        }

        //public ActionResult Delete(string id)
        //{
           
        //    User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Friends friends = db.Friends.Where(m=> m.user.Id == user.Id && m.friend.Id == id).First();
        //    if (friends == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(friends);
        //}

        // POST: /Fr/Delete/5
        
       
        public ActionResult Delete(string id)
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            Friends friends = db.Friends.Where(m => m.user.Id == user.Id && m.friend.Id == id).FirstOrDefault();

            db.Friends.Remove(friends);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}