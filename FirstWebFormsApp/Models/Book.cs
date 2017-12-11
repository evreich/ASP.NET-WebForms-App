using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstWebFormsApp.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int DateRealise { get; set; }

        public Book(string _title, string _genre, string _author, int _date)
        {
            Title = _title;
            Genre = _genre;
            Author = _author;
            DateRealise = _date;
        }
    }
}