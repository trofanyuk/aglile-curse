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
            List<User> users = new List<User>();
            if (type.Equals("By name"))
            {
                search = search.Trim(' ');

                if (search.IndexOf(' ') == -1)
                {
                    users = db.Users.Where(m => m.Firstname == search || m.Surname == search).ToList<User>();
                }
                else
                {
                    string[] searchStr = search.Split(' ');
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
    }
}