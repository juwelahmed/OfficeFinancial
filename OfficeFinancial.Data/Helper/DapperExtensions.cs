using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace OfficeFinancial.Data.Helper
{
    public static class DapperExtensions
    {
        public static dynamic[] GetPrimaryKeys(this IDbConnection connection, string tableName)
        {
            string query =
                string.Format(
                    "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_NAME), 'IsPrimaryKey') = 1 AND TABLE_NAME = '{0}'",
                    tableName);
            var result = connection.Query(query).ToArray();

            return result;
        }

        //public static T Add<T>(this IDbConnection cnn, string tableName, dynamic param)
        //{
        //    IEnumerable<T> result = SqlMapper.Query<T>(cnn, SqlQueryHelper.GetInsertQuery(tableName, param), param);
        //    return result.First();
        //}

        //public static void Update(this IDbConnection cnn, string tableName, dynamic param)
        //{
        //    SqlMapper.Execute(cnn, SqlQueryHelper.GetUpdateQuery(tableName, param), param);
        //}
    }
}
