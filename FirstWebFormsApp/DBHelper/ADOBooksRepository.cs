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

        private List<Book> ConvertDTtoBooks(DataTable dt)
        {
            return (from DataRow row in dt.Rows

                    select new Book((int)row["Id"],
                                    row["TitleBook"].ToString(),
                                    row["Author"].ToString(),
                                    row["Genre"].ToString(),
                                    (DateTime)row["Date"])

                    ).ToList();
        }

        public List<Book> GetBooks()
        {
            string cmdText = "SELECT b.Id, " +
                                    "b.TitleBook AS TitleBook, " +
                                    "a.FirstName + ' ' + a.LastName AS Author, " +
                                    "g.Title AS Genre, " +
                                    "b.DateRealise AS Date " +
                             "FROM Books b " +
                             "JOIN Authors a ON b.AuthorId=a.Id " +
                             "JOIN Genres g ON b.GenreId=g.Id";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdText, _connStr))
            {
                adapter.Fill(dt);
            }

            return ConvertDTtoBooks(dt);
        }

        public void AddBook(Book book)
        {
            string sqlExpression = String.Format("INSERT INTO Books (TitleBook, AuthorId, GenreId, DateRealise) " +
                                                 "VALUES ('{0}', {1}, {2}, CONVERT(datetime, '{3}', 103) )",
                                                  book.TitleBook, book.AuthorId, book.GenreId, book.DateRealise); ;

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
                                                  book.TitleBook, book.AuthorId, book.GenreId, book.DateRealise, book.Id); ;

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
                                                  id); ;

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