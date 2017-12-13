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
    public class ADOGenresRepository
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["BookContext"].ConnectionString;

        public ADOGenresRepository()
        {
        }

        private List<Genre> ConvertDTtoGenres(DataTable dt)
        {
            return (from DataRow row in dt.Rows

                    select new Genre((int)row["Id"],
                                    row["Title"].ToString())

                    ).ToList();
        }

        public List<Genre> GetGenres()
        {
            string cmdText = "SELECT Id, Title " +
                             "FROM Genres ";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdText, _connStr))
            {
                adapter.Fill(dt);
            }

            return ConvertDTtoGenres(dt);
        }
    }
}