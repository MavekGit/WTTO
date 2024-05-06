using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankApp
{
    public class Admin
    {
        public static List<User> userList = new List<User>();
        public void adminLogin(int userId)
        {

            string adminAction = "";

            int breakLoop = 0;
            while(adminAction != "5" && breakLoop != 20)
            {
                breakLoop++;
                Console.WriteLine("");
                Console.WriteLine("Witaj, na koncie administratora");
                Console.WriteLine("Wciśnij 1, aby dodać użytkownika");
                Console.WriteLine("Wciśnij 2, aby usunać użytkownika");
                Console.WriteLine("Wciśnij 3, aby modyfikować ustawienia klientów");
                Console.WriteLine("Wciśnij 4, obejrzeć raport operacji wybranego użytkownika");
                Console.WriteLine("Wciśnij 5, aby wylogować się");

                adminAction = Console.ReadLine();


                switch (adminAction)
                {
                    case "1":

                        createUser();

                        break;

                    case "2":
                        deleteUser();
                        break;

                    case "3":
                        modifyUserSettings();
                        break;

                    case "4":
                        adminViewOperationHistory();
                        break;

                    case "5":
                        Console.WriteLine("Wylogowuje... ");
                        break;

                    default:
                        Console.WriteLine("Błąd");
                        break;
                }
            }
        }


        public static void createUser()
        {
            int phoneNum;

            Console.Write("Podaj imię: ");
            string name = Console.ReadLine();
            Console.Write("Podaj nazwisko: ");
            string lastName = Console.ReadLine();
            Console.Write("Podaj numer telefonu: ");
            string phoneNumber = Console.ReadLine();

            if (!int.TryParse(phoneNumber, out phoneNum))
            {
                Console.WriteLine("Podany numer telefonu nie jest prawidłowy");
                return;
            }
            Console.Write("Podaj login: ");
            string login = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();
            bool admin = false;

            int accountNumber = createAccountNumber();

            int id = userList.Count + 2;
            bool uniqueIdFound = false;
         
            while (!uniqueIdFound)
            {
             
                if (userList.All(user => user.Id != id))
                {
                    uniqueIdFound = true;
                }
                else
                {
                    id++;
                }
            }

            User newUser = new User
            {
                Id = id,
                Name = name,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Login = login,
                Password = password,
                Admin = admin
            };

            userList.Add(newUser);

        }

        public void deleteUser()
        {
            Console.WriteLine("Lista użytkowników:");
            foreach (User user in userList)
            {
                Console.WriteLine($"ID: {user.Id}, Imię: {user.Name}, Nazwisko: {user.LastName}, Numer konta: {user.AccountNumber} ");
            }

            string StringId;
            int deleteUserId;
            Console.WriteLine("Wpisz id użytkownika którego chcesz usunąć");
            
            StringId = Console.ReadLine();

            if (int.TryParse(StringId, out deleteUserId))
            {
                userList.RemoveAll(user => user.Id == deleteUserId);

            }
            else
            {
                Console.WriteLine("Błąd");
            }
        }

        public void modifyUserSettings()
        {
            Console.WriteLine("Lista użytkowników:");
            foreach (User user in userList)
            {
                Console.WriteLine($"ID: {user.Id}, Imię: {user.Name}, Nazwisko: {user.LastName}, Numer konta: {user.AccountNumber} ");
            }

            Console.WriteLine("Wpisz ID użytkownika, którego ustawienia chcesz zmodyfikować:");
            string StringId = Console.ReadLine();
            if (int.TryParse(StringId, out int modifyUserId))
            {
                User user = userList.Find(u => u.Id == modifyUserId);

                if (user != null)
                {
                    Console.WriteLine($"ID: {user.Id}, Imię: {user.Name}, Nazwisko: {user.LastName}, Numer konta: {user.AccountNumber} ");
                    Console.WriteLine("Co chcesz zmodyfikować?");
                    Console.WriteLine("1. Imię");
                    Console.WriteLine("2. Nazwisko");
                    Console.WriteLine("3. Numer telefonu");
                    Console.WriteLine("4. Login");
                    Console.WriteLine("5. Hasło");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Podaj nowe imię:");
                            user.Name = Console.ReadLine();
                            break;
                        case "2":
                            Console.WriteLine("Podaj nowe nazwisko:");
                            user.LastName = Console.ReadLine();
                            break;
                        case "3":
                            Console.WriteLine("Podaj nowy numer telefonu:");
                            user.PhoneNumber = Console.ReadLine();
                            break;
                        case "4":
                            Console.WriteLine("Podaj nowy login:");
                            user.Login = Console.ReadLine();
                            break;
                        case "5":
                            Console.WriteLine("Podaj nowe hasło:");
                            user.Password = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Użytkownik o podanym id nie istnieje.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowe ID użytkownika.");
            }
        }

        public void adminViewOperationHistory()
        {
            Console.WriteLine("Lista użytkowników:");
            foreach (User user in userList)
            {
                Console.WriteLine($"ID: {user.Id}, Imię: {user.Name}, Nazwisko: {user.LastName}, Numer konta: {user.AccountNumber} ");
            }

            string StringId;
            int viewUserId,i = 1;
            Console.WriteLine("Wpis id użytkownika którego raport operacji chcesz przeglądnąć");

            StringId = Console.ReadLine();

            if (int.TryParse(StringId, out viewUserId))
            {
                User user = userList.Find(user => user.Id == viewUserId);

                if (user.OperationHistory != null)
                {

                    foreach (string operation in user.OperationHistory)
                    {
                        Console.WriteLine($"{i}. " + operation);
                        i++;
                    }
                }
  
            }
            else 
            {
                Console.WriteLine("Błąd");
            }
        }

        public static int createAccountNumber()
        {
            Random random = new Random();
            int accountNumber = 0;
            bool uniqueAccountNumberFound = false;

            while (!uniqueAccountNumberFound)
            {

                accountNumber = random.Next(10000000, 100000000);
   
                accountNumber = createParityBit(accountNumber);

                if (userList.All(user => user.AccountNumber != accountNumber))
                {
                    uniqueAccountNumberFound = true;
                }
            }

            return accountNumber;
        }

        public static int createParityBit(int accountNumber)
        {
            int sumEven = 0;
            int sumOdd = 0;
            int tempAccountNumber = accountNumber;

            for (int i = 1; tempAccountNumber != 0; i++)
            {
                int digit = tempAccountNumber % 10;
                tempAccountNumber /= 10;

                if (i % 2 == 0)
                {
                    sumEven += digit;
                }
                else
                {
                    sumOdd += digit;
                }
            }

  
            int parityBit = (sumEven - sumOdd) % 10;
            if (parityBit < 0)
            {
                parityBit += 10; 
            }

            accountNumber = accountNumber * 10 + parityBit;

            return accountNumber;
        }

    }
}
