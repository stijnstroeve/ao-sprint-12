using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Top2000.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}