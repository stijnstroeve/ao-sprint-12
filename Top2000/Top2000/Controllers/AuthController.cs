using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top2000.Models;

namespace Top2000.DAL
{
    public class AuthController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel registerModel)
        {

            return View();
        }
    }
}