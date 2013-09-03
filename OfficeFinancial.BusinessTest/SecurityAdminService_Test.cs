using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OfficeFinancial.Entities.SecurityAdminEnities;
using OfficeFinancial.Business;

namespace OfficeFinancial.BusinessTest
{
    [TestClass]
    public class SecurityAdminService_Test
    {
        [TestMethod]
        public void AddUserTest()
        {
            //arrange
            var userToAdd = new tUser { ID = "Shahin", Password = "Sa1234", LanguageCode = "US", FontName = "Test", FontSize = 12, Notes = "Test", Theme = "Default" };

            // act 
           var result= SecurityAdminService.AddUser(userToAdd);

            // assert
           Assert.IsTrue(result);
        }
    }
}
