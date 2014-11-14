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
            List<User> users;

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

            ViewBag.Users = users;
            return View();
        }
    }
}