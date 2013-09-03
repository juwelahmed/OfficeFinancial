using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using OfficeFinancial.Entities.SecurityAdminEnities;
using OfficeFinancial.Business;
using OfficeFinancial.Data.DataProvider;

using OfficeFinancial.Common;

namespace OfficeFinancial.Business
{
    public class SecurityAdminService
    {
        public static tUser GetUserByID(string userID)
        {
            tUser user = null;
            try
            {
                user= SecurityAdminDataProvider.GetUserByID(userID);
            }
            catch (SqlException ex)
            {
                throw new BusinessApplicationException("Error in busines logic of GetUserByID", ex);
            }
            return user;
        }

        public static IEnumerable<tUser> GetAll()
        {
            return SecurityAdminDataProvider.GetAll();
        }

        
        public static bool AddUser(tUser userToAdd)
        {
            return SecurityAdminDataProvider.InsertUser(userToAdd);
        }

        public static bool ChangePassword(string userId, string oldPassword, string newPassword)
        {

            var userInDb = GetUserByID(userId);

            if (userId == null)
                throw new ArgumentException("Your provided user id does not exist ", "userId");

            if (!userInDb.Password.Equals(oldPassword, StringComparison.CurrentCulture))
                throw new ArgumentException("Your provided old password does not match ", "oldPassword");


            SecurityAdminDataProvider.ChangePassword(userId, oldPassword, newPassword);

            return true;

        }
    }
}

