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
    public class ADOAuthorsRepository
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["BookContext"].ConnectionString;

        public ADOAuthorsRepository()
        {
        }

        private List<Author> ConvertDTtoAuthors(DataTable dt)
        {
            return (from DataRow row in dt.Rows

                    select new Author((int)row["Id"],
                                    row["FirstName"].ToString(),
                                    row["LastName"].ToString())

                    ).ToList();
        }

        public List<Author> GetAuthors()
        {
            string cmdText = "SELECT Id, FirstName, LastName " +
                             "FROM Authors ";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdText, _connStr))
            {
                adapter.Fill(dt);
            }

            return ConvertDTtoAuthors(dt);
        }
    }
}