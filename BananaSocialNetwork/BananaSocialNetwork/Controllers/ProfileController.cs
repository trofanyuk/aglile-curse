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


namespace BananaSocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Default1/
        public ActionResult Index()
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            user.Albums = db.Albums.Where(m => m.User.Id == user.Id ).OrderByDescending(t=>t.DateCreate);

            for (int i = 0; i < user.Albums.Count(); i++ )
            {
                var album = user.Albums.ElementAt(i);
                album.CountPhotos = db.Photos.Where(m => m.Album.Id == album.Id).Count();
            }
            //ViewBag.Albums = user.Albums;
            return View(user);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            // Находим в базе студента
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
            //Изменяем данные об игроке в таблице
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddAlbumPartial()
        {
            return PartialView();
        }
        [Authorize]
        public string GetAutorizeUser()
        {
            string login = User.Identity.GetUserName();
            var user = (from usr in db.Users where usr.Email == login select usr).First();
            var jsonUser = JsonConvert.SerializeObject(user);
            return jsonUser;
        }
	}
}