using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTunnel.Domain.Interfaces;
using CodeTunnel.Domain.Repositories;
using CodeTunnel.Domain.Entities;
using System.Web.Security;

namespace CodeTunnel.UI.Controllers
{
    /// <summary>
    /// Controller for handling functions related to users.
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// Repository for retrieving and persisting users.
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Constructor that accepts dependencies.
        /// </summary>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Returns the login page.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect the user back to after successful login.</param>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect(returnUrl);
            ViewBag.Invalid = false;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Allows a visitor to login.
        /// </summary>
        /// <param name="username">The username of the account to authenticate as.</param>
        /// <param name="password">The password associated with the account.</param>
        /// <param name="returnUrl">The URL to redirect the user back to after successful login.</param>
        /// <param name="remember">True if the authentication cookie should persist between sessions.</param>
        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl, bool remember)
        {
            var user = _userRepository.GetUser(username);
            if (user != null && user.Password == password)
            {
                FormsAuthentication.SetAuthCookie(user.Username, remember);
                return Redirect(returnUrl);
            }
            ViewBag.Invalid = true;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Allows the user to logout.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect the user bakc to after successful logout.</param>
        public RedirectResult Logout(string returnUrl)
        {
            FormsAuthentication.SignOut();
            return Redirect(returnUrl);
        }
    }
}
