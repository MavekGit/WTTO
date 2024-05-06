using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApp;

namespace BankAppTests
{
    [TestClass]
    public class AdminActionTest
    {
        [TestMethod]
        public void Test_CreateUser_Success()
        {
            // Arrange
            Admin admin = new Admin();
            string input = "Jan\nJan\n123\nJanJan\n1111";
            int userCount = Admin.userList.Count;

            // Act
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                Admin.createUser();
            }

            // Assert
            Assert.AreEqual(userCount + 1, Admin.userList.Count);

            
            User Klient = Admin.userList[Admin.userList.Count - 1];
            Assert.AreEqual("Jan", Klient.Name);
            Assert.AreEqual("Jan", Klient.LastName);
            Assert.AreEqual("123", Klient.PhoneNumber);
            Assert.AreEqual("JanJan", Klient.Login);
            Assert.AreEqual("1111", Klient.Password);
            Assert.IsFalse(Klient.Admin);
        }

        [TestMethod]
        public void Test_ModifyUserSettings_InvalidInput()
        {
            // Arrange
            Admin admin = new Admin();
            int userId = 2;
            User user = new User { Id = userId, Name = "Adam", LastName = "Nowak", PhoneNumber = "123456789", Login = "AdamNowak", Password = "abcd" };
            Admin.userList.Add(user);
            string input = "Janusz\n";

            // Act
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                admin.modifyUserSettings();
            }

            // Assert
            User modifiedUser = Admin.userList.Find(u => u.Id == userId);
            Assert.IsNotNull(modifiedUser);
            Assert.AreEqual("Adam", modifiedUser.Name);
            Assert.AreEqual("Nowak", modifiedUser.LastName);
            Assert.AreEqual("123456789", modifiedUser.PhoneNumber);
            Assert.AreEqual("AdamNowak", modifiedUser.Login);
            Assert.AreEqual("abcd", modifiedUser.Password);
        }

        [TestMethod]
        public void Test_ModifyUserSettings_Success()
        {
            // Arrange
            Admin admin = new Admin();
            int userId = 2;
            User user = new User { Id = userId, Name = "Adam", LastName = "Nowak", PhoneNumber = "123456789", Login = "AdamNowak", Password = "abcd" };
            Admin.userList.Add(user);
            string input = "2\n1\nJanusz\n";

            // Act
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                admin.modifyUserSettings();
            }

            // Assert
            User modifiedUser = Admin.userList.Find(u => u.Id == userId);
            Assert.IsNotNull(modifiedUser);
            Assert.AreEqual("Janusz", modifiedUser.Name);
            Assert.AreEqual("Nowak", modifiedUser.LastName);
            Assert.AreEqual("123456789", modifiedUser.PhoneNumber);
            Assert.AreEqual("AdamNowak", modifiedUser.Login);
            Assert.AreEqual("abcd", modifiedUser.Password);
        }

        [TestMethod]
        public void Test_DeleteUser_InalidUser()
        {
            // Arrange
            Admin admin = new Admin();
            string input = "20\n";
            int userCount = Admin.userList.Count;
            Console.WriteLine(userCount);
            // Act
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                admin.deleteUser();
            }

            Console.WriteLine(userCount);
            // Assert
            Assert.AreEqual(userCount, Admin.userList.Count);

        }

        [TestMethod]
        public void Test_DeleteUser_Success()
        {
            // Arrange
            Admin admin = new Admin();
            string input = "2\n";
            int userCount = Admin.userList.Count;
            Console.WriteLine(userCount);
            // Act
            using (StringReader sr = new StringReader(input))
            {
                Console.SetIn(sr);
                admin.deleteUser();
            }

            Console.WriteLine(userCount);
            // Assert
            Assert.AreEqual(userCount - 1, Admin.userList.Count);
 
        }


       
    }
}
