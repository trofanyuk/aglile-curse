using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;
using BananaSocialNetwork.Models;
using System.Net;

namespace BananaSocialNetwork.Tests.Controllers
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CommentControllerTest
    {
       [TestMethod]
        public void CreateViewResultNotNull()
        {
           CommentController controller = new CommentController();
           ViewResult result = controller.Create() as ViewResult;
           Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OneCommentViewResultNotNull()
        {
            CommentController controller = new CommentController();
            Comment comment = new Comment();
            ViewResult result = controller.OneComment(comment) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void EditIdNull()
        {
            CommentController controller = new CommentController();
            int? id = null;
            HttpStatusCodeResult result = controller.Edit(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeleteIdNull()
        {
            CommentController controller = new CommentController();
            int? id = null;
            HttpStatusCodeResult result = controller.Delete(id) as HttpStatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
