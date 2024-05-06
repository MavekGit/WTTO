using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApp;

namespace BankAppTests
{
    [TestClass]
    public class UserTests
    {


        [TestMethod]
        public void TestProperties_InicializeParameters_Success()
        {
            // Arrange
            User user = new User();
            int id = 10;
            string name = "Janusz";
            string lastName = "Szunaj";
            string phoneNumber = "123456789";
            string login = "JanuszSzunaj";
            string password = "0987";
            bool admin = false;
            user.Id = id;
            user.Name = name;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.Login = login;
            user.Password = password;
            user.Admin = admin;


            // Assert
            Assert.AreEqual(id, user.Id);
            Assert.IsNotNull(user.AccountNumber);
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(lastName, user.LastName);
            Assert.AreEqual(phoneNumber, user.PhoneNumber);
            Assert.AreEqual(login, user.Login);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(admin, user.Admin);
            Assert.AreEqual(0, user.AccountBalance);
            Assert.AreEqual(0, user.Credit);
        }






    }
}
