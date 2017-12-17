using FirstWebFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FirstWebFormsApp.DBHelper
{
    class ADOBooksRepository
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["BookContext"].ConnectionString;

        public ADOBooksRepository()
        {
        }

        public List<Book> GetBooks(int pageIndex, int pageSize, string title, string genre)
        {
            List<Book> books = new List<Book>();
            SqlCommand comm = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetBooksByFiltersOnPage"
            };
            if (genre == "Все")
                genre = "";

            comm.Parameters.Add(new SqlParameter("PageIndex", pageIndex));
            comm.Parameters.Add(new SqlParameter("PageSize", pageSize));
            comm.Parameters.Add(new SqlParameter("TitleBook", title));
            comm.Parameters.Add(new SqlParameter("Genre", genre));
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                comm.Connection = conn;
                using (var readerBook = comm.ExecuteReader())
                {
                    while (readerBook.Read())
                    {
                        Book book = new Book
                        (
                            readerBook.GetInt32(readerBook.GetOrdinal("Id")),
                            readerBook.GetString(readerBook.GetOrdinal("TitleBook")),
                            readerBook.GetString(readerBook.GetOrdinal("Genre")),
                            readerBook.GetString(readerBook.GetOrdinal("Author")),
                            readerBook.GetDateTime(readerBook.GetOrdinal("Date"))
                        );

                        books.Add(book);
                    }
                }
            }

            return books;
        }
 
        public int GetBooksCount(string title, string genre)
        {
            int res = 0;
            if (genre == "Все")
                genre = "";

            string sqlExpression = String.Format("SELECT COUNT(*) AS Count_Books " +
                                                 "FROM Books " +
                                                 "JOIN Genres ON Books.GenreId=Genres.Id " +
                                                 "WHERE TitleBook LIKE '%'+'{0}'+'%' AND Genres.Title LIKE '%'+'{1}'", title, genre);

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        res = reader.GetInt32(0);
                    }
                }
                return res;
            }
        }

        public void AddBook(Book book)
        {
            string sqlExpression = String.Format("INSERT INTO Books (TitleBook, AuthorId, GenreId, DateRealise) " +
                                                 "VALUES ('{0}', {1}, {2}, CONVERT(datetime, '{3}', 103) )",
                                                  book.TitleBook, book.AuthorId, book.GenreId, book.DateRealise); 

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public void EditBook(Book book)
        {
            string sqlExpression = String.Format("UPDATE Books " +
                                                 "SET TitleBook = '{0}', AuthorId = {1}, GenreId = {2}, DateRealise = CONVERT(datetime, '{3}', 103)" +
                                                 "WHERE Id = {4}",
                                                  book.TitleBook, book.AuthorId, book.GenreId, book.DateRealise, book.Id); 

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public Book GetBook(int id)
        {
            Book resBook;
            string sqlExpression = String.Format("SELECT b.Id, b.TitleBook, b.AuthorId, b.GenreId, b.DateRealise " +
                                                 "FROM Books b " +
                                                 "WHERE b.Id = {0}",
                                                  id); 

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        resBook = new Book { Id = reader.GetInt32(0), TitleBook = reader.GetString(1),
                                             AuthorId = reader.GetInt32(2), GenreId = reader.GetInt32(3),
                                             DateRealise = reader.GetDateTime(4) };
                    }
                }
                return resBook;
            }
        }
    }
}