﻿using System;
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

namespace BananaSocialNetwork.Controllers
{
    public class AlbumController : Controller
    {
        //ApplicationUserManager userManager;
        // GET: /Album/
        
       ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            ApplicationUser user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            user.Albums = db.Albums.Where(m => m.User.Id == user.Id);
            if (user.Albums.Count() == 0)
            {
                return View("NotAlbums");
            }
            else
            {
                ViewBag.Albums = user.Albums;
                return View();
            }   
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Album album)
        {
            ApplicationUser user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            album.User = user;
            db.Albums.Add(album);
            (user.Albums as List<Album>).Add(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            Album album = db.Albums.Find(id);
            album.Photos = db.Photos.Where(m => m.Album.Id == album.Id);

            if (album.Photos.Count()== 0)
            {
                return View("~/Views/Photo/Create.cshtml");
            }

            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

	}
}