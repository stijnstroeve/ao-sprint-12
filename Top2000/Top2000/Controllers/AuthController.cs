using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Top2000.Attributes;
using Top2000.Helpers;
using Top2000.Models;

namespace Top2000.Controllers
{
    public class AuthController : Controller
    {
        // GET: Login
        [LoginDisallowed]
        public ActionResult Login()
        {
            return View();
        }

        // GET: Register
        [LoginDisallowed]
        public ActionResult Register()
        {
            return View();
        }

        // GET: Logout
        [LoginRequired]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "YearList");
        }

        // POST: Register
        [HttpPost]
        [LoginDisallowed]
        public ActionResult Register(RegisterModel registerModel)
        {
            // Make sure the model is filled
            if (registerModel.Email == null)
            {
                ViewBag.Error = "E-mail niet ingevuld.";
                return View();
            }
            else if (registerModel.FirstName == null)
            {
                ViewBag.Error = "Voornaam niet ingevuld.";
                return View();
            }
            else if (registerModel.LastName == null)
            {
                ViewBag.Error = "Achternaam niet ingevuld.";
                return View();
            }
            else if (registerModel.Password == null)
            {
                ViewBag.Error = "Wachtwoord niet ingevuld.";
                return View();
            }

            using (var db = new EntityContext())
            {

                // Check if user with email already exists
                User duplicateUser = db.User
                    .Where(x => x.Email == registerModel.Email)
                    .FirstOrDefault();

                if(duplicateUser != null)
                {
                    ViewBag.Error = "Gebruiker met deze e-mail bestaat al.";
                    return View();
                }

                User registerdUser = new User
                {
                    Email = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    PasswordHash = PasswordHelper.ComputeSha256Hash(registerModel.Password),
                    RoleID = 1 // Default role = user
                };

                // Save the user to the database
                db.User.Add(registerdUser);
                db.SaveChanges();

                ViewBag.Success = "Je hebt succesvol een account aangemaakt!";
                return View();
            }
        }

        // POST: Login
        [HttpPost]
        [LoginDisallowed]
        public ActionResult Login(LoginModel loginModel)
        {
            // Make sure the model is filled
            if(loginModel.Email == null)
            {
                ViewBag.Error = "E-mail niet ingevuld.";
                return View();
            } else if(loginModel.Password == null)
            {
                ViewBag.Error = "Wachtwoord niet ingevuld.";
                return View();
            }

            using (var db = new EntityContext())
            {
                string password = PasswordHelper.ComputeSha256Hash(loginModel.Password);

                User user = db.User
                    .Where(x => x.Email == loginModel.Email)
                    .Where(x => x.PasswordHash == password)
                    .FirstOrDefault();

                if(user == null)
                {
                    // No user was found with this email or password.
                    ViewBag.Error = "Ongeldige gebruikersnaam of wachtwoord.";
                    return View();
                }

                string fullName = $"{user.FirstName} {user.LastName}";

                // Create the user identity
                UserIdentity identity = new UserIdentity(
                    fullName,
                    "userAuth",
                    user.UserID,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.Role.RoleName
                );

                // Create the user principal
                var principal = new GenericPrincipal(
                    identity,
                    new string[] { user.Role.RoleKey }
                );

                Session["principal"] = principal;

                return RedirectToAction("Index", "YearList");
            }
        }
    }
}