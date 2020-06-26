using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top2000.Attributes;
using Top2000.Helpers;
using Top2000.Models;
using Top2000.ViewModels;

namespace Top2000.Controllers
{
    [LoginRequired]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Delete/1
        public ActionResult Delete(int id)
        {
            using (var db = new EntityContext())
            {
                // First retrieve the details of the user from the db
                var userDetails = db.User
                    .Where(user => user.UserID == id)
                    .FirstOrDefault();
                
                // Remove the user from the database set
                db.User.Remove(userDetails);

                // Save the changes to the database
                db.SaveChanges();

                return RedirectToAction("List");
            }
        }

        // GET: User/Edit/1
        public ActionResult Edit(int id)
        {
            using (var db = new EntityContext())
            {
                // First retrieve the details of the user from the db
                var userDetails = db.User
                    .Where(user => user.UserID == id)
                    .Select(user => new UserEditModel
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Password = "",
                        RoleKey = user.Role.RoleKey
                    })
                    .FirstOrDefault();

                // Add the roles to the viewbag so they can be displayed easily
                var roles = db.Role.ToList();
                ViewBag.Roles = roles;

                return View(userDetails);
            }
        }

        // POST: User/Edit/1
        [HttpPost]
        public ActionResult Edit(UserEditModel editModel)
        {
            try
            {
                using (var db = new EntityContext())
                {
                    User user = db.User
                        .Where(x => x.UserID == editModel.ID)
                        .FirstOrDefault();

                    // Update default details
                    if(editModel.FirstName != null)
                    {
                        user.FirstName = editModel.FirstName;
                    }
                    if (editModel.LastName != null)
                    {
                        user.LastName = editModel.LastName;
                    }

                    // Special details
                    if (editModel.Email != null && editModel.Email != user.Email)
                    {
                        User duplicateUser = db.User
                            .Where(x => x.Email == editModel.Email)
                            .FirstOrDefault();

                        // Make sure the email is not already used
                        if (duplicateUser != null)
                        {
                            // The email is already in use
                            ViewBag.Error = "Gebruiker met deze e-mail bestaat al.";
                            return View(editModel);
                        } else
                        {
                            user.Email = editModel.Email;
                        }

                    }

                    if (editModel.Password != null)
                    {
                        // If the password should be updated, rehash it and put it in the db
                        user.PasswordHash = PasswordHelper.ComputeSha256Hash(editModel.Password);
                    }

                    // Check if the role should be updated
                    if (editModel.RoleKey != user.Role.RoleKey)
                    {
                        // If the role should be updated, update the role to its role id
                        var role = db.Role.Where(x => x.RoleKey == editModel.RoleKey).FirstOrDefault();

                        // Make sure the role exists so the user wont be assigned to a non existing role
                        if (role == null)
                        {
                            ViewBag.Error = "De rol bestaat niet!";
                            return View(editModel);
                        }

                        // Update the actual role
                        user.RoleID = role.RoleID;
                    }

                    // Save the updates user to the database
                    db.SaveChanges();

                    return RedirectToAction("List");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: User/Details/1
        public ActionResult Details(int id)
        {
            using (var db = new EntityContext())
            {
                var userDetails = db.User
                    .Where(user => user.UserID == id)
                    .Select(user => new UserViewModel
                    {
                        ID = user.UserID,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role.RoleName
                    })
                    .FirstOrDefault();
                return View(userDetails);
            }
        }

        // GET: User/List
        public ActionResult List()
        {
            using (var db = new EntityContext())
            {
                // List all users from the database and map it to a view model
                var users = db.User
                    .Select(user => new UserViewModel
                    {
                        ID = user.UserID,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role.RoleName
                    })
                    .ToList();

                return View(users);
            }
        }

    }
}