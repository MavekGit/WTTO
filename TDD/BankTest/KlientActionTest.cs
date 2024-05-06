using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApp; 

namespace BankAppTests
{
    [TestClass]
    public class KlientActionTests
    {
        [TestMethod]
        public void Test_AccountRecharge_InvalidInput()
        {
            // Arrange
            Klient klient = new Klient();
            int userId = 1;
            double initialBalance = 100;
            User user = new User { Id = userId, AccountBalance = initialBalance };
            Admin.userList.Add(user);
            string input = "sto"; 

            // Act
            using (System.IO.StringReader sr = new System.IO.StringReader(input))
            {
                Console.SetIn(sr);
                klient.accountRecharge(userId);
            }

            // Assert
            Assert.AreEqual(initialBalance, user.AccountBalance); 
        }

        [TestMethod]
        public void Test_MoneyTransfer_NotEnoughFunds()
        {
            // Arrange
            Klient klient = new Klient();
            int userId = 2;
            double initialBalance = 100;
            User user = new User { Id = userId, AccountBalance = initialBalance };
            User receiver = new User { Id = 100, AccountBalance = initialBalance, AccountNumber = 12345678 };
            Admin.userList.Add(user);
            string input = "200\n12345678"; 

            // Act
            using (System.IO.StringReader sr = new System.IO.StringReader(input))
            {
                Console.SetIn(sr);
                klient.moneyTransfer(userId);
            }

            Assert.AreNotEqual(300, receiver.AccountBalance);
        }

        [TestMethod]
        public void Test_MoneyTransfer_InvalidReceiver()
        {
            // Arrange
            Klient klient = new Klient();
            int userId = 3;
            double initialBalance = 100;
            User user = new User { Id = userId, AccountBalance = initialBalance };
            User receiver = new User { Id = 101, AccountBalance = initialBalance };
            Admin.userList.Add(user);
            string input = "100\n11111111"; 

            // Act
            using (System.IO.StringReader sr = new System.IO.StringReader(input))
            {
                Console.SetIn(sr);
                klient.moneyTransfer(userId);
            }

            // Assert
            Assert.AreNotEqual(200, receiver.AccountBalance);        }

        [TestMethod]
        public void Test_AccountRecharge_Success()
        {
            // Arrange
            Klient klient = new Klient();
            int userId = 4;
            float initialBalance = 100;
            User user = new User { Id = userId, AccountBalance = initialBalance };
            Admin.userList.Add(user);
            string input = "100";

            // Act
            using (System.IO.StringReader sr = new System.IO.StringReader(input))
            {
                Console.SetIn(sr);
                klient.accountRecharge(userId);
            }

            // Assert
            Console.WriteLine(user.AccountBalance);

            Assert.AreEqual(200, user.AccountBalance); 

        }

        [TestMethod]
        public void Test_TakeCredit_Success()
        {
            // Arrange
            Klient klient = new Klient();
            int userId = 5;
            double initialBalance = 0, initialCredit = 0;
            User user = new User { Id = userId, AccountBalance = initialBalance, Credit = initialCredit };
            Admin.userList.Add(user);
            string input = "100"; 

            // Act
            using (System.IO.StringReader sr = new System.IO.StringReader(input))
            {
                Console.SetIn(sr);
                klient.takeCredit(userId);
            }

            // Assert
            Console.WriteLine(user.AccountBalance);
            Console.WriteLine(user.Credit);
            Assert.AreEqual(100, user.AccountBalance); 
            Assert.AreEqual(110, user.Credit); 
        }

        [TestMethod]
        public void Test_PayUpCredit_NotEnoughFunds()
        {
            // Arrange
            Klient klient = new Klient();
            int userId = 6;
            double initialBalance = 0, initialCredit = 100;
            User user = new User { Id = userId, AccountBalance = initialBalance, Credit = initialCredit };
            Admin.userList.Add(user);
            string input = "100"; 

            // Act
            using (System.IO.StringReader sr = new System.IO.StringReader(input))
            {
                Console.SetIn(sr);
                klient.payUpCredit(userId);
            }

            // Assert
            Assert.AreEqual(100, user.Credit); 
        }


        [TestMethod]
        public void Test_PayUpCredit_Success()
        {
            // Arrange
            Klient klient = new Klient();
            int userId = 7;
            double initialBalance = 100, initialCredit = 100;
            User user = new User { Id = userId, AccountBalance = initialBalance, Credit = initialCredit };
            Admin.userList.Add(user);
            string input = "100";

            // Act
            using (System.IO.StringReader sr = new System.IO.StringReader(input))
            {
                Console.SetIn(sr);
                klient.payUpCredit(userId);
            }

            // Assert
            Assert.AreEqual(0, user.Credit);
        }


    }
}
