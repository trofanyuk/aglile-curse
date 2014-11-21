using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;

namespace BananaSocialNetwork.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            SearchController controller = new SearchController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
