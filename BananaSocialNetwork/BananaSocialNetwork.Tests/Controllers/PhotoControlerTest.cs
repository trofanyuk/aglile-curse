using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;

namespace BananaSocialNetwork.Tests.Controllers
{
    /// <summary>
    /// Summary description for PhotoControlerTest
    /// </summary>
    [TestClass]
    public class PhotoControlerTest
    {
        [TestMethod]
        public void CreateViewResultNot()
        {
            PhotoController controller = new PhotoController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
