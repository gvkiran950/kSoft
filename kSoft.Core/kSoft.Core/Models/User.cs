﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace kSoft.Core.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
