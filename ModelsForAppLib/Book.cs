using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsForAppLib
{
    [JsonObject]
    public class Book
    {
        public string TitleBook { get; set; }
        public DateTime DateRealise { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }

        public int Id { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }

        public Book() { }

        public Book(int _id, string _title, string _genre, string _author, DateTime _date)
        {
            Id = _id;
            TitleBook = _title;
            Genre = _genre;
            Author = _author;
            DateRealise = _date;
        }

        public Book(int _id, string _title, int _genreId, int _authorId, DateTime _date)
        {
            Id = _id;
            TitleBook = _title;
            GenreId = _genreId;
            AuthorId = _authorId;
            DateRealise = _date;
        }
    }
}
