using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSocialNetwork.Models;
using System.Web.Script.Serialization;

namespace BananaSocialNetwork.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string SearchUser(string search, string type)
        {
            string usersName;
            List<User> users = new List<User>();
            if (type.Equals("By name"))
            {
                usersName = search.Trim(' ');



                if (usersName.IndexOf(' ') == -1)
                {
                    users = db.Users.Where(m => m.Firstname == usersName || m.Surname == usersName).ToList<User>();
                }
                else
                {
                    string[] searchStr = usersName.Split(' ');
                    string FirstName = searchStr[0];
                    string SureName = searchStr[1];

                    users = db.Users.Where(m => m.Firstname == FirstName && m.Surname == SureName).ToList<User>();
                }
            }
            else if (type.Equals("By email"))
            {
                users = db.Users.Where(m => m.UserName == search).ToList<User>();
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(users);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SearchUserView(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Type type = typeof(List<User>);
            object users = js.Deserialize(json, type);
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string AllUsers()
        {
            List<User> allUsers = new List<User>();
            allUsers.AddRange(db.Users.ToList());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allUsers);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string AllNews()
        {
            List<News> allNews = new List<News>();
            allNews.AddRange(db.News.ToList());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allNews);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string NewsUser(string iduser)
        {
            List<News> allNews = new List<News>();
            allNews.AddRange(db.News.ToList().Where(m => m.User.Id == iduser));
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allNews);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult NewsUserView(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Type type = typeof(List<News>);
            object news = js.Deserialize(json, type);
            return View(news);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public string AllAlbums()
        {
            List<Album> allAlbums = new List<Album>();
            allAlbums.AddRange(db.Albums.ToList());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allAlbums);
            return result;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public string AlbumsUser(string iduser)
        {
            List<Album> allAlbums = new List<Album>();
            allAlbums.AddRange(db.Albums.ToList().Where(m => m.User.Id == iduser));
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allAlbums);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AlbumsUserView(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Type type = typeof(List<Album>);
            object news = js.Deserialize(json, type);
            return View(news);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string AllPhotos()
        {
            List<Photo> allPhotos = new List<Photo>();
            allPhotos.AddRange(db.Photos.ToList());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allPhotos);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string PhotosAlbum(int idalbum)
        {
            List<Photo> allPhotos = new List<Photo>();
            allPhotos.AddRange(db.Photos.ToList().Where(m => m.Album.Id == idalbum));
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allPhotos);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string PhotosUser(string iduser)
        {
            List<Photo> allPhotos = new List<Photo>();
            allPhotos.AddRange(db.Photos.ToList().Where(m => m.Album.User.Id == iduser));
            JavaScriptSerializer js = new JavaScriptSerializer();
            var result = js.Serialize(allPhotos);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult PhotosAlbumView(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Type type = typeof(List<Photo>);
            object news = js.Deserialize(json, type);
            return View(news);
        }
    }
}