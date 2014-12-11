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
using BananaSocialNetwork.IRepository;

namespace BananaSocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        IRepositoryUser repoUser;

        public ProfileController()
        {
            repoUser = new UserIRepository();
        }
        public ProfileController(IRepositoryUser r)
        {
            repoUser = r;
        }


        public ActionResult Index(string userId)
        {

            try
            {
                User user = db.Users.ToList().Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
                if (userId != null && user.Id != userId)
                {
                    user = db.Users.ToList().Where(m => m.Id == userId).First();
                }
                user.Albums = db.Albums.ToList().Where(m => m.User.Id == user.Id).OrderByDescending(t => t.DateCreate);
                user.Friends = db.Friends.ToList().Where(m => m.user.Id == user.Id || m.friend.Id == user.Id);
                user.Subscribers = db.Subscribers.ToList().Where(m => m.subscriber.Id == user.Id);


                for (int i = 0; i < user.Albums.Count(); i++)
                {
                    Album album = user.Albums.ElementAt(i);
                    album.Photos = db.Photos.ToList().Where(m => m.Album.Id == album.Id);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }

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
            var check = from ch in db.Friends.ToArray() where ch.user == friends.user && ch.friend == friends.friend select ch;
            if (check.Count() <= 0)
            {
                friends.confirm = true;
                db.Friends.Add(friends);
                db.SaveChanges();

                friends = db.Friends.Where(m => m.user.Id == idFriend && m.friend.Id == user.Id).First();
                friends.confirm = true;
                db.Entry(friends).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        public ActionResult AddSubscribers(string idFriend)
        {
            User friend = db.Users.Where(m => m.Id == idFriend).FirstOrDefault();
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            Subscribers subscribers = new Subscribers(user, friend);
            db.Subscribers.Add(subscribers);
            db.SaveChanges();

            return Redirect("Index?userId=" + idFriend);
        }

        public ActionResult ShowUpdates()
        {

            List<News> allNews = new List<News>();
            User user = db.Users.ToList().Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            user.Subscribers = db.Subscribers.ToList().Where(m => m.user.Id == user.Id);
            foreach (var usr in user.Subscribers)
            {
                allNews.AddRange(db.News.ToList().Where(m => m.User.Id == usr.subscriber.Id));
            }

            allNews.Sort(delegate(News news1, News news2)
            { return news2.CreationTime.CompareTo(news1.CreationTime); });

            return View(allNews);
        }



        public ActionResult ShowFriends(string userId)
        {
            User user = db.Users.ToList().Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            if (userId != null && user.Id != userId)
            {
                user = db.Users.ToList().Where(m => m.Id == userId).First();
            }
            user.Friends = db.Friends.ToList().Where(m => m.user.Id == user.Id || m.friend.Id == user.Id);


            return View(user);
        }

        public ActionResult DeleteSubscribers(string idFriend)
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            Subscribers friends = db.Subscribers.Where(m => m.user.Id == user.Id && m.subscriber.Id == idFriend).FirstOrDefault();

            db.Subscribers.Remove(friends);
            db.SaveChanges();

            return Redirect("Index?userId=" + idFriend);
        }

        public ActionResult ShowMapAlbums(string userId)
        {
            User user = db.Users.Where(m => m.Id == userId).First();

            user.Albums = db.Albums.Where(m => m.User.Id == user.Id);
            return View(user);

        }


    }
}