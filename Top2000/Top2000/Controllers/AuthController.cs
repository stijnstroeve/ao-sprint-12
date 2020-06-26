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
        [LoginDisallowed] // You can access this page when you are logged in
        public ActionResult Login()
        {
            return View();
        }

        // GET: Register
        [LoginDisallowed] // You can access this page when you are logged in
        public ActionResult Register()
        {
            return View();
        }

        // GET: Logout
        [LoginRequired] // You can access this page when you are not logged in
        public ActionResult Logout()
        {
            // Abandon the session and redirect to the login page
            Session.Abandon();
            return RedirectToAction("Login", "Auth");
        }

        // POST: Register
        [HttpPost]
        [LoginDisallowed] // You post this when you are logged in
        public ActionResult Register(RegisterModel registerModel)
        {
            // Make sure the model is filled
            if (registerModel.Email == null)
            {
                // Email was not filled in
                ViewBag.Error = "E-mail niet ingevuld.";
                return View();
            }
            else if (registerModel.FirstName == null)
            {
                // First name was not filled in
                ViewBag.Error = "Voornaam niet ingevuld.";
                return View();
            }
            else if (registerModel.LastName == null)
            {
                // Last name was not filled in
                ViewBag.Error = "Achternaam niet ingevuld.";
                return View();
            }
            else if (registerModel.Password == null)
            {
                // Password was not filled in
                ViewBag.Error = "Wachtwoord niet ingevuld.";
                return View();
            }

            using (var db = new EntityContext())
            {
                // Check if user with email already exists
                User duplicateUser = db.User
                    .Where(x => x.Email == registerModel.Email)
                    .FirstOrDefault();

                // If the user with the mail was already found, I must be a duplicate
                if(duplicateUser != null)
                {
                    ViewBag.Error = "Gebruiker met deze e-mail bestaat al.";
                    return View();
                }

                // Create the new user
                User registerdUser = new User
                {
                    Email = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    PasswordHash = PasswordHelper.ComputeSha256Hash(registerModel.Password),
                    RoleID = 1 // Default role = user
                };

                // Save the new user to the database
                db.User.Add(registerdUser);
                db.SaveChanges();

                ViewBag.Success = "Je hebt succesvol een account aangemaakt!";
                return View();
            }
        }

        // POST: Login
        [HttpPost]
        [LoginDisallowed] // You post this when you are logged in
        public ActionResult Login(LoginModel loginModel)
        {
            // Make sure the model is filled
            if(loginModel.Email == null)
            {
                // Email was not filled in
                ViewBag.Error = "E-mail niet ingevuld.";
                return View();
            } else if(loginModel.Password == null)
            {
                // Password was not filled in
                ViewBag.Error = "Wachtwoord niet ingevuld.";
                return View();
            }

            using (var db = new EntityContext())
            {
                // Generate the password by hashing the given string
                string password = PasswordHelper.ComputeSha256Hash(loginModel.Password);

                // Check if the user can be found in the database
                User user = db.User
                    .Where(x => x.Email == loginModel.Email)
                    .Where(x => x.PasswordHash == password)
                    .FirstOrDefault();

                // Check if the user was even found
                if(user == null)
                {
                    // No user was found with this email or password.
                    ViewBag.Error = "Ongeldige gebruikersnaam of wachtwoord.";
                    return View();
                }

                string fullName = $"{user.FirstName} {user.LastName}";

                // Create the user identity for the session
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

                // Set the principal in the session
                Session["principal"] = principal;

                return RedirectToAction("Index", "YearList");
            }
        }
    }
}