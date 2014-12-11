using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BananaSocialNetwork.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

    }
}