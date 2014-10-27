using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;

namespace BananaSocialNetwork.Tests.Controllers
{
    [TestClass]
    public class ProfileControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull() // ActionResult Index()
        {
            ProfileController controller = new ProfileController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetailsViewResultNotNull() //ActionResult Details(int id)
        {
            ProfileController controller = new ProfileController();
            ViewResult result = controller.Details(0) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateViewResultNotNull() //ActionResult Create()
        {
            ProfileController controller = new ProfileController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditViewResultNotNull() //ActionResult Edit()
        {
            ProfileController controller = new ProfileController();
            ViewResult result = controller.Edit(0) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteViewResultNotNull() //ActionResult Delete()
        {
            ProfileController controller = new ProfileController();
            ViewResult result = controller.Delete(0) as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
