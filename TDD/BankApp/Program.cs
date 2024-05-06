using BankApp;
using System;
using System.ComponentModel;

public class Program
{
    
    public static void Main(string[] args)
    {

        User AdminUser = new User();

        AdminUser.Id = 0;
        AdminUser.Login = "admin";
        AdminUser.Password = "admin";
        AdminUser.Admin = true;

        Admin.userList.Add(AdminUser);        
        
        User KlientUser = new User();
        KlientUser.Id = 1;
        KlientUser.AccountNumber = 12345678;
        KlientUser.Name = "Jan";
        KlientUser.LastName = "Kowalski";
        KlientUser.Login = "JanKowalski";
        KlientUser.Password = "1234";
        KlientUser.Admin = false;

        User KlientUserTest = new User();
        KlientUserTest.Id = 2;
        KlientUserTest.AccountNumber = 87654321;
        KlientUserTest.Name = "Maria";
        KlientUserTest.LastName = "Nowak";
        KlientUserTest.Login = "MariaNowak";
        KlientUserTest.Password = "1111";
        KlientUserTest.Admin = false;

        Admin.userList.Add(KlientUser);
        Admin.userList.Add(KlientUserTest);

        string entryCode = "1000";
        int loopBreak = 0;

        while (entryCode != "2" && loopBreak != 20)
        {
            loopBreak++;
            Console.WriteLine("");
            Console.WriteLine("Witaj w Aplikacji Banku\n");
            Console.WriteLine("Wciśnij 1, aby się zalogować");
            Console.WriteLine("Wciśnij 2, żeby wyjść");

            entryCode = Console.ReadLine();

            if (entryCode == "1")
            {
                login();
            }

            else if(entryCode == "2")
            {
                Console.WriteLine("Zakończono program.");
            }
            else
            {
                Console.WriteLine("Niepoprawna operacja");
            }
        }
    }

    private static void login()
    {
        

        Console.WriteLine("Logowanie...");

        bool adminLoginSuccess = false, klientLoginSuccess = false;
        String login = "",password = "";
        int userId = -1000;

        Console.WriteLine("Podaj Login");
        login = Console.ReadLine();
        Console.WriteLine("Podaj hasło");
        password = Console.ReadLine();

        
        foreach (User user in Admin.userList)
        {
            if (login == user.Login && password == user.Password && user.Admin == true)
            {
                userId = user.Id;
                adminLoginSuccess = true;
            }
            else if (login == user.Login && password == user.Password && user.Admin == false)
            {
                userId = user.Id;
                klientLoginSuccess = true;
            }
        }

        if(adminLoginSuccess)
        {
            Admin adminLogin = new Admin();

            adminLogin.adminLogin(userId);

        }
        else if(klientLoginSuccess)
        {
            Klient klientLogin = new Klient();
            klientLogin.klientLogin(userId);
        }
        else
        {
            Console.WriteLine("Błąd logowania");
        }

        login = "";
        password = "";
        userId = -1000;
        adminLoginSuccess = false;
        klientLoginSuccess = false;
    }


  

}
