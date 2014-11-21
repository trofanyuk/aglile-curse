using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;

namespace BananaSocialNetwork.Tests.Controllers
{
    [TestClass]
    public class AlbumControllerTest
    {
        [TestMethod]
        public void CreateViewResultNotNull()
        {
            AlbumController controller = new AlbumController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void  AddAlbumPartialNotNull()
        {
            AlbumController controller = new AlbumController();
            PartialViewResult result = controller.AddAlbumPartial() as PartialViewResult;
            Assert.IsNotNull(result);
        }
    }
}
