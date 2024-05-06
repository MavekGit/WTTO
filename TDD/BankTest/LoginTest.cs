using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApp;
using System;
using System.IO;

namespace BankAppTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Test_Login_Admin_Success()
        {
            // Arrange
            var input = new StringReader("1\nadmin\nadmin\n");
            Console.SetIn(input);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.Main(new string[0]);
            string expectedOutput = "Logowanie...\r\n";
            expectedOutput += "Podaj Login\r\nPodaj has³o\r\n";

            // Assert
            Assert.IsTrue(sw.ToString().Contains(expectedOutput));
        }

        [TestMethod]
        public void Test_Login_Klient_Success()
        {
            // Arrange
            var input = new StringReader("1\nJanKowalski\n1234\n");
            Console.SetIn(input);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.Main(new string[0]);
            string expectedOutput = "Logowanie...\r\n";
            expectedOutput += "Podaj Login\r\nPodaj has³o\r\n";

            // Assert
            Assert.IsTrue(sw.ToString().Contains(expectedOutput));
        }

        [TestMethod]
        public void Test_Login_InalidInput()
        {
            // Arrange
            var input = new StringReader("1\ninvalidUser\ninvalidPassword\n");
            Console.SetIn(input);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.Main(new string[0]);
            string expectedOutput = "Logowanie...\r\n";
            expectedOutput += "Podaj Login\r\nPodaj has³o\r\n";
            expectedOutput += "B³¹d logowania\r\n";

            // Assert
            Assert.IsTrue(sw.ToString().Contains(expectedOutput));
        }

       
    }
}
