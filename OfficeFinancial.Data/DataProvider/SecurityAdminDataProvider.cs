using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

using Dapper;

using OfficeFinancial.Entities.SecurityAdminEnities;
using OfficeFinancial.Common;

namespace OfficeFinancial.Data.DataProvider
{
    public class SecurityAdminDataProvider
    {
        public static tUser GetUserByID(string userID)
        {
            tUser selectedUser;
            try
            {
                using (IDbConnection connection = Helper.DbConnectionHelper.GetConnection())
                {
                    var sql = "SELECT * FROM tUser where ID=@ID";

                    selectedUser = connection.Query<tUser>(sql, new { ID = userID }).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in GetUserByID. " + ex,
                //    new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                throw;
                
            }
            //catch (InvalidOperationException ex)
            //{
            //    LoggerHelper.Write(TraceEventType.Error, "Error in GetUserByID. " + ex,
            //        new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    LoggerHelper.Write(TraceEventType.Error, "Error in GetUserByID. " + ex,
            //        new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });

            //    throw new DatabaseException("Error in data logic of GetUserByID", ex);
            //}
            return selectedUser;
        }

        public static IEnumerable<tUser> GetAll()
        {
            IEnumerable<tUser> users;
            try
            {
                using (IDbConnection connection = Helper.DbConnectionHelper.GetConnection())
                {

                    var sql = "SELECT * FROM tUser";

                    users = connection.Query<tUser>(sql);

                }
            }
            catch (SqlException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in GetAll. " + ex,
                //    new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                throw;
            }
            catch (InvalidOperationException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in GetAll. " + ex,
                //    new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                throw;
            }
            return users;
        }

        public static bool InsertUser(tUser userToInsert)
        {
            try
            {
                using (IDbConnection connection = Helper.DbConnectionHelper.GetConnection())
                {
                    string sql = "INSERT INTO [OfficeFinancial].[dbo].[tUser]([ID],[Password],[Notes] ,[LanguageCode] ,[Theme] ,[FontName]   ,[FontSize])";
                    sql += " VALUES(@ID,@Password, @Notes,@LanguageCode, @Theme,@FontName ,@FontSize)";

                    connection.Execute(sql, userToInsert);

                }
            }
            catch (SqlException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in InsertUser. " + ex,
                //    new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                throw;
            }
            catch (InvalidOperationException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in InsertUser. " + ex,
                //    new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                throw;
            }
            return true;
        }

        public static bool ChangePassword(string userId, string oldPassword, string newPassword)
        {
            try
            {
                using (IDbConnection connection = Helper.DbConnectionHelper.GetConnection())
                {
                    var userInDb = GetUserByID(userId);

                    if (userId == null)
                        throw new ArgumentException("Your provided user id does not exist ", "userId");

                    if (!userInDb.Password.Equals(oldPassword, StringComparison.CurrentCulture))
                        throw new ArgumentException("Your provided old password does not match ", "oldPassword");

                    string sql = "UPDATE [dbo].[tUser] SET [Password]=@Password WHERE ID=@ID";


                    connection.Execute(sql, new { ID = userId, Password = newPassword });

                }
            }
            catch (SqlException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in ChangePassword. " + ex,
                //    new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                throw;
            }
            catch (InvalidOperationException ex)
            {
                //LoggerHelper.Write(TraceEventType.Error, "Error in ChangePassword. " + ex,
                //    new string[] { Constants.LOGGING_CATEGORY_DEV, Constants.LOGGING_CATEGORY_PRODUCTION });
                throw;
            }
            return true;
        }
    }
}
