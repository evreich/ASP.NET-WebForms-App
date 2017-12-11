using FirstWebFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FirstWebFormsApp.DB
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

                    select new Book(row["Title"].ToString(),
                                    row["Author"].ToString(),
                                    row["Genre"].ToString(),
                                    (int)row["Date"])

                    ).ToList();
        }

        public List<Book> GetBooks()
        {
            string cmdText = "SELECT b.Title AS Title, " +
                                    "a.FirstName + ' ' + a.LastName AS Author, " +
                                    "g.Title AS Genre, " +
                                    "YEAR(b.DateRealise) AS Date " +
                             "FROM Books b " +
                             "JOIN Authors a ON b.AuthorId=a.Id " +
                             "JOIN Genres g ON b.GenreId=g.Id";

            var dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdText, _connStr);
            adapter.Fill(dt);
            adapter.Dispose();

            return ConvertDTtoBooks(dt);
        }

    }
}