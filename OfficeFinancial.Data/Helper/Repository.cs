using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Dapper;
using System.Linq;

namespace OfficeFinancial.Data.Helper
{
    public static class Repository
    {
        internal static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            }
        }

        public static IEnumerable<T> GetAll<T>() where T : class
        {
            IEnumerable<T> results;

            using (IDbConnection connection = Connection)
            {
                results = connection.Query<T>(SqlQueryHelper.GetSelectQuery<T>());
            }

            return results;
        }

        public static IEnumerable<T> GetAllByParam<T>(Expression<Func<T, bool>> expression) where T : class
        {
            IEnumerable<T> results;

            using (IDbConnection connection = Connection)
            {
                results = connection.Query<T>(SqlQueryHelper.GetSelectQuery<T>(expression).Sql);
            }

            return results;
        }

        public static T GetByParam<T>(Expression<Func<T, bool>> expression) where T : class
        {
            T result;

            using (IDbConnection connection = Connection)
            {
                result = connection.Query<T>(SqlQueryHelper.GetSelectQuery<T>(expression).Sql).FirstOrDefault();
            }

            return result;
        }

        public static int Add<T>(object entityToAdd) where T : class
        {
            using (IDbConnection connction = Connection)
            {
                return connction.Execute(SqlQueryHelper.GetInsertQuery<T>(), entityToAdd);
            }
        }

        public static int Update<T>(object entityToUpdate) where T : class
        {
            using (IDbConnection connction = Connection)
            {
                var keys = connction.GetPrimaryKeys(typeof (T).Name);

                return connction.Execute(SqlQueryHelper.GetUpdateQuery<T>(), entityToUpdate);
            }
        }

        public static int Delete<T>(object entityToDelete) where T : class
        {
            int result;

            using (IDbConnection connection = Connection)
            {
                result = connection.Execute(SqlQueryHelper.GetDeleteQuery<T>(), entityToDelete);
            }

            return result;
        }
    }
}
