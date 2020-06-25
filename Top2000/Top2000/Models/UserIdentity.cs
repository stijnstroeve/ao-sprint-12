using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Top2000.Models
{
    public class UserIdentity : GenericIdentity, IIdentity
    {
        public int ID;
        public string Email;
        public string FirstName;
        public string LastName;
        public string Role;

        public UserIdentity(string name, string type, int id, string email, string firstName, string lastName, string role) : base(name, type)
        {
            ID = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}