﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}