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
        public ActionResult Index(string userId)
        {
           
            User user = db.Users.ToList().Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            if(user.Id != userId && userId != null)
            {
                user = db.Users.ToList().Where(m => m.Id == userId).First();
            }
            user.Albums = db.Albums.ToList().Where(m => m.User.Id == user.Id).OrderByDescending(t => t.DateCreate);
            user.Friends = db.Friends.ToList().Where(m => m.user.Id == user.Id || m.friend.Id == user.Id && m.confirm == false);
            user.Subscribers = db.Subscribers.ToList().Where(m => m.user.Id == user.Id);


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

            friends = db.Friends.Where(m => m.user.Id == id && m.friend.Id == user.Id).FirstOrDefault();
            db.Friends.Remove(friends);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ConfirmFriend(string idFriend)
        {  
            User friend = db.Users.Where(m => m.Id == idFriend).FirstOrDefault();
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            Friends friends = new Friends(user, friend);
            friends.confirm = true;
            db.Friends.Add(friends);
            db.SaveChanges();

            friends = db.Friends.Where(m => m.user.Id == idFriend && m.friend.Id == user.Id).First();
            friends.confirm = true;
            db.Entry(friends).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult AddSubscribers(string idFriend)
        {
            User friend = db.Users.Where(m => m.Id == idFriend).FirstOrDefault();
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            Subscribers subscribers = new Subscribers(user, friend);
            db.Subscribers.Add(subscribers);
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }

        public ActionResult ShowUpdates()
        {
            List<Photo> photos = new List<Photo>();


                User user = db.Users.ToList().Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
                user.Subscribers = db.Subscribers.ToList().Where(m => m.user.Id == user.Id);

                //user.Subscribers = db.Subscribers.Where(m => m.user.Id == user.Id && m.subscriber.Id != user.Id);

              
               // Subscribers subscribers = db.Subscribers.Where(m => m.Id == 1).First();

               


                foreach (Subscribers sub in user.Subscribers)
                {

                    sub.subscriber.Albums = db.Albums.Where(m => m.User.Id == sub.subscriber.Id);
                    for (int i = 0; i < sub.subscriber.Albums.Count(); i++)
                    {
                        Album album = sub.subscriber.Albums.ElementAt(i);
                        List<Photo> photosUser = db.Photos.ToList().Where(m => m.Album.Id == album.Id).ToList();
                        foreach (Photo photo in photosUser)
                        {
                            photos.Add(photo);
                        }
                    }
                   
                }
            return View(photos);
        }


        public ActionResult ShowFriends()
        {
            User user = db.Users.ToList().Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            user.Friends = db.Friends.ToList().Where(m => m.user.Id == user.Id || m.friend.Id == user.Id  /*&& m.confirm == true*/);


            return View(user);
        }
    }
}