using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Dapper;

namespace OfficeFinancial.Data.Helper
{
   internal class DbConnectionHelper
   {
       private static readonly string ConnectionString;

       static DbConnectionHelper()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection
            {
                ConnectionString = ConnectionString
            };
        }
    }
}
