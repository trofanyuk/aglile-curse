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
        public void IndexViewResultNotNull() 
        {
            ProfileController controller = new ProfileController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetailsViewResultNotNull() 
        {
            ProfileController controller = new ProfileController();
        }

        [TestMethod]
        public void CreateViewResultNotNull() 
        {
            ProfileController controller = new ProfileController();

        }

        [TestMethod]
        public void EditViewResultNotNull() 
        {
            ProfileController controller = new ProfileController();

        }

        [TestMethod]
        public void DeleteViewResultNotNull() 
        {
            ProfileController controller = new ProfileController();

        }
    }
}
