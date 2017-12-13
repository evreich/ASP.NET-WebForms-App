using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstWebFormsApp.Models
{
    public class Book
    {

        [Required(ErrorMessage = "Введите наименование книги")]
        [RegularExpression(@"^[A-Za-zА-Яа-я\s\d]+", ErrorMessage = "В имени допускаются только буквы, цифры и пробелы")]
        public string TitleBook { get; set; }
        [Required(ErrorMessage = "Введите дату создания")]
        public DateTime DateRealise { get; set; }
        [Required(ErrorMessage = "Выберите жанр")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Выберите автора")]
        public int AuthorId { get; set; }

        public int Id { get; set; }
        public string Genre { get; }
        public string Author { get; }

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