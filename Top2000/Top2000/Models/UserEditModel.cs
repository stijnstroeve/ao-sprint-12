﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Top2000.Models
{
    public class UserEditModel
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RoleKey { get; set; }
    }
}