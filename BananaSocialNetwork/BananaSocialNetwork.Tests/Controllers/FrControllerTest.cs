using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;
using System.Net;

namespace BananaSocialNetwork.Tests.Controllers
{
    [TestClass]
    public class FrControllerTest
    {
        [TestMethod]
        public void DetailsIdNull()
        {
            FrController controller = new FrController();
            int? id = null;
            HttpStatusCodeResult result = controller.Details(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void CreateViewResultNotNull()
        {
            FrController controller = new FrController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditIdNull()
        {
            FrController controller = new FrController();
            int? id = null;
            HttpStatusCodeResult result = controller.Edit(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public void DeleteIdNull()
        {
            FrController controller = new FrController();
            int? id = null;
            HttpStatusCodeResult result = controller.Delete(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
