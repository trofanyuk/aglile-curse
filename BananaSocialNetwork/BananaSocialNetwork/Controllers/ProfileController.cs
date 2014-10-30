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
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Default1/
        public ActionResult Index()
        {
            User user = db.Users.Where(m => m.Email == HttpContext.User.Identity.Name).FirstOrDefault();
           user.Albums = db.Albums.Where(m => m.User.Id == user.Id ).OrderByDescending(t=>t.DateCreate).Take(5);
            ViewBag.Albums = user.Albums;
            return View(user);
        }
	}
}