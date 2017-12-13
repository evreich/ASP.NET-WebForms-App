﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstWebFormsApp.Models
{
    public class Author
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Id { get; }

        public Author(int _id, string _firstName, string _lastName)
        {
            Id = _id;
            FirstName = _firstName;
            LastName = _lastName;
        }
    }
}