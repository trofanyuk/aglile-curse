﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;
using System.Net;

namespace BananaSocialNetwork.Tests.Controllers
{
    /// <summary>
    /// Summary description for PhotoControlerTest
    /// </summary>
    [TestClass]
    public class PhotoControllerTest
    {
        [TestMethod]
        public void DetailsIdNull()
        {
            PhotoController controller = new PhotoController();
            int? id = null;
            HttpStatusCodeResult result = controller.Details(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void CreateViewResultNotNull()
        {
            PhotoController controller = new PhotoController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditIdNull()
        {
            PhotoController controller = new PhotoController();
            int? id = null;
            HttpStatusCodeResult result = controller.Edit(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public void DeleteIdNull()
        {
            PhotoController controller = new PhotoController();
            int? id = null;
            HttpStatusCodeResult result = controller.Delete(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void AddPhotoPartialViewResultNotNullViewBagEqualId()
        {
            PhotoController controller = new PhotoController();
            int id = 1;
            ViewResult result = controller.AddPhotoPartial(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewBag.Al_Id, id);
        }
    }
}
