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
using System.Web.Helpers;
using System.Net;
using System.Data.Entity;

namespace BananaSocialNetwork.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult SearchOnName(string search)
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            List<User> users;
            List<User> notFriends = new List<User>();


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

            foreach (User friend in users)
            {
                if (db.Friends.Where(m => m.user.Id == user.Id && m.friend.Id == friend.Id).FirstOrDefault() == null)
                {
                    notFriends.Add(friend);
                }

            }
            ViewBag.Users = notFriends;
            return View();
        }

        public ActionResult AddFriend(string idFriend)
        {
            User friend = db.Users.Where(m => m.Id == idFriend).FirstOrDefault();
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            Friends friends = new Friends(user, friend);
            db.Friends.Add(friends);       
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");


        }
    }
}