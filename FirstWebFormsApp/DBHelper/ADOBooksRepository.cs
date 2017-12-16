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

        static public readonly string TITLE_FIELD = "TitleBook";
        static public readonly string AUTHOR_FIELD = "Author";
        static public readonly string GENRE_FIELD = "Genre";
        static public readonly string DATEREALISE_FIELD = "Date";

        public ADOBooksRepository()
        {
        }
        
        public List<Book> GetBooks(int pageIndex, int pageSize)
        {
            List<Book> books = new List<Book>();
            SqlCommand comm = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetBooksOnPage"
            };
            comm.Parameters.Add(new SqlParameter("PageIndex", pageIndex));
            comm.Parameters.Add(new SqlParameter("PageSize", pageSize));
            using(SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                comm.Connection = conn;
                using (var readerBook = comm.ExecuteReader())
                {
                    while(readerBook.Read())
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

        public List<Book> GetSortedBooks(int pageIndex, int pageSize, string orderField, bool isReverse)
        {
            string direction = !isReverse ? "ASC" : "DESC" ;

            List<Book> books = new List<Book>();
            SqlCommand comm = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetSortedBooksOnPage"
            };
            comm.Parameters.Add(new SqlParameter("PageIndex", pageIndex));
            comm.Parameters.Add(new SqlParameter("PageSize", pageSize));
            comm.Parameters.Add(new SqlParameter("OrderedField", orderField));
            comm.Parameters.Add(new SqlParameter("Direction", direction));
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

        public int GetBooksCount()
        {
            int res = 0;
            string sqlExpression = "SELECT COUNT(*) AS Count_Books " +
                                   "FROM Books";                                             

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