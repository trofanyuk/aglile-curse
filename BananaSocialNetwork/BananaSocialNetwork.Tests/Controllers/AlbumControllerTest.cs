﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;

namespace BananaSocialNetwork.Tests.Controllers
{
    [TestClass]
    public class AlbumControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            AlbumController controller = new AlbumController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}