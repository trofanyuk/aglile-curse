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
using System.Web.Helpers;
using System.Net;
using System.Data.Entity;

namespace BananaSocialNetwork.Controllers
{
    public class AlbumController : Controller
    {
        //ApplicationUserManager userManager;
        // GET: /Album/

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();

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

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Album json)
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            Album album = new Album()
            {
                Name = json.Name,
                Adress = json.Adress,
                DateCreate = DateTime.Now,
                GeoLat = json.GeoLat,
                GeoLong = json.GeoLong,
                User = user
            };

            db.Albums.Add(album);
            (user.Albums as List<Album>).Add(album);
            db.SaveChanges();

            return View("Index", "Profile");
        }

        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Album album = db.Albums.Find(id);
            album.Photos = db.Photos.Where(m => m.Album.Id == album.Id);    

            return View(album);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddAlbumPartial()
        {
            return PartialView();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

       
        [HttpPost]
        public ActionResult Edit(Album json)
        {
            Album album = db.Albums.Find(json.Id);
            album.Adress = json.Adress;
            album.GeoLat = json.GeoLat;
            album.GeoLong = json.GeoLong;
            album.Name = json.Name;

            db.Entry(album).State = EntityState.Modified;
            db.SaveChanges();
            return null;
        }

        [HttpGet]
        public ActionResult EditAlbumPartial(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
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