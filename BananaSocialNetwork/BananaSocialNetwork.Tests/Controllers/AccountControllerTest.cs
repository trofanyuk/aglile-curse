using System;
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
        public void LoginViewResultNotNull() //ActionResult Login(string returnUrl)
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.Login("/Account/Login") as ViewResult;
            Assert.AreEqual(controller.ViewBag.ReturnUrl, "/Account/Login");
            Assert.IsNotNull(result);
        }

       // [TestMethod]
        //public void LoginViewModelNotNull() //async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    AccountController controller = new AccountController();
        //    LoginViewModel model = new LoginViewModel();
        //    Task<ActionResult> result = controller.Login(model, "/Account/Login") as Task<ActionResult>;
        //    Assert.IsNotNull(result);
        //}

        //Register
        [TestMethod]
        public void RegisterViewResultNotNull() // ActionResult Register()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.Register() as ViewResult;
            Assert.IsNotNull(result);
        }

        //E-mail
        //[TestMethod] 
        //public Task<ViewResult> ConfirmEmailUserIdIsNull() //async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    AccountController controller = new AccountController();
        //    string userId = null, code = "code";
        //    await controller.ConfirmEmail(userId, code);
        //    Assert.Equals(controller)
        //}
        [TestMethod]
        public void ConfirmedEmailViewResultNotNull() //ActionResult ConfirmedEmail()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ConfirmedEmail() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, "ConfirmedEmail");
        }

        //ForgotPasswor
        [TestMethod]
        public void ForgotPasswordViewResultNotNull() //ActionResult ForgotPassword()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ForgotPassword() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ForgotPasswordConfirmationViewResultNotNull() //ActionResult ForgotPasswordConfirmation()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ForgotPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }

        //ResetPassword
        [TestMethod]
        public void ResetPasswordCodeNull() //ActionResult ResetPassword(string code)
        {
            AccountController controller = new AccountController();
            string code = null;
            ViewResult result = controller.ResetPassword(code) as ViewResult;
            Assert.AreEqual(result.ViewName, "Error");
        }

        [TestMethod]
        public void ResetPasswordCodeNotNull() //ActionResult ResetPassword(string code)
        {
            AccountController controller = new AccountController();
            string code = "code";
            ViewResult result = controller.ResetPassword(code) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ResetPasswordConfirmationViewResultNotNull() //ActionResult ResetPasswordConfirmation()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ResetPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }

        //Other
        [TestMethod]
        public void ExternalLoginFailureViewResultNotNull() //ActionResult ExternalLoginFailure()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ExternalLoginFailure() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}