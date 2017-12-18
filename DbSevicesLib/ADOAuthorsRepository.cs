using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSevicesLib
{
    public class ADOAuthorsRepository
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["BookContext"].ConnectionString;

        public ADOAuthorsRepository()
        {
        }

        public DataTable GetAuthors()
        {
            string cmdText = "SELECT Id, FirstName + ' ' + LastName AS Name " +
                             "FROM Authors ";

            var dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdText, _connStr))
            {
                adapter.Fill(dt);
            }

            return dt;
        }
    }
}
