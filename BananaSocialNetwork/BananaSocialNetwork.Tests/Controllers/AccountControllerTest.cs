﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaSocialNetwork.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;
using BananaSocialNetwork.Models;
using System.Collections.Generic;

namespace BananaSocialNetwork.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        //Login
        [TestMethod]
        public void LoginViewResultNotNull() 
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.Login("/Account/Login") as ViewResult;
            Assert.AreEqual(controller.ViewBag.ReturnUrl, "/Account/Login");
            Assert.IsNotNull(result);
        }

       

        //Register
        [TestMethod]
        public void RegisterViewResultNotNull() 
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.Register() as ViewResult;
            Assert.IsNotNull(result);
        }

        
        [TestMethod]
        public void ConfirmedEmailViewResultNotNull() 
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ConfirmedEmail() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, "ConfirmedEmail");
        }

        //ForgotPasswor
        [TestMethod]
        public void ForgotPasswordViewResultNotNull() 
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ForgotPassword() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ForgotPasswordConfirmationViewResultNotNull() 
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ForgotPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }

        //ResetPassword
        [TestMethod]
        public void ResetPasswordCodeNull() 
        {
            AccountController controller = new AccountController();
            string code = null;
            ViewResult result = controller.ResetPassword(code) as ViewResult;
            Assert.AreEqual(result.ViewName, "Error");
        }

        [TestMethod]
        public void ResetPasswordCodeNotNull() 
        {
            AccountController controller = new AccountController();
            string code = "code";
            ViewResult result = controller.ResetPassword(code) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ResetPasswordConfirmationViewResultNotNull() 
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ResetPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }

        //Other
        [TestMethod]
        public void ExternalLoginFailureViewResultNotNull() 
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ExternalLoginFailure() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}