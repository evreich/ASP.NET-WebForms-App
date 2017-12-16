﻿using FirstWebFormsApp.Models;
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

        public DataTable GetGenres()
        {
            string cmdText = "SELECT Id, Title " +
                             "FROM Genres ";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdText, _connStr))
            {
                adapter.Fill(dt);
            }

            return dt;
        }
    }
}