using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace BankApp
{
    public class Klient
    {
        
        public void klientLogin(int userId)
        {

            String klientAction = "";
            int breakLoop = 0;

            while (klientAction != "6" && breakLoop != 20)
            {
                breakLoop++;
                User user = Admin.userList.Find(x => x.Id == userId);

                Console.WriteLine("");
                Console.WriteLine("Witaj, twój stan konta o numerze " + user.AccountNumber +  " to: " + user.AccountBalance + ", kredyt do spłacenia to: " + user.Credit);
                Console.WriteLine("Wciśnij 1, aby zasilić swoje konto");
                Console.WriteLine("Wciśnij 2, aby wysłać przelew");
                Console.WriteLine("Wciśnij 3, aby wziąć kredyt");
                Console.WriteLine("Wciśnij 4, spłacić kredyt");
                Console.WriteLine("Wciśnij 5, aby przeglądnać historię operacji");
                Console.WriteLine("Wciśnij 6, aby wylogować się");

                klientAction = Console.ReadLine();


                switch (klientAction)
                {
                    case "1":

                        accountRecharge(userId);

                        break;

                    case "2":
                        moneyTransfer(userId);
                        break;

                    case "3":
                        takeCredit(userId);
                        break;

                    case "4":
                        payUpCredit(userId);
                        break;

                    case "5":
                        viewOperationHistory(userId);
                        break;

                    case "6":
                        Console.WriteLine("Wylogowuje... ");
                        break;

                    default:
                        Console.WriteLine("Niepoprawna operacja");
                        break;
                }
            }
        }

        public void accountRecharge(int userId)
        {
            String number = "0";
            double amountToAdd = 0;

            Console.WriteLine("Jaką kwotę chcesz wpłacić?");
            number = Console.ReadLine();

            if(double.TryParse(number, out amountToAdd))
            {
                Console.WriteLine("Zasilam konto...");

                User user = Admin.userList.Find(x => x.Id == userId);
                user.AccountBalance += amountToAdd;

                Console.WriteLine("Dodano kwotę " + amountToAdd + " do konta");

                user.OperationHistory.Add("Zasilono konto o " + amountToAdd);
            }
            else
            {
                Console.WriteLine("Błąd");
            }

        }

        public void moneyTransfer(int userId)
        {
            string number = "0", inputAccNumber = "0";
            double amountToSub;
            int accNumber = 0;
            Console.WriteLine("Jaką kwotę chcesz przelać?");
            number = Console.ReadLine();

            Console.WriteLine("Na jakie konto chcesz przelać?");
            inputAccNumber = Console.ReadLine();

            if (double.TryParse(number, out amountToSub) && int.TryParse(inputAccNumber, out accNumber))
            {
                User receiver = Admin.userList.Find(x => x.AccountNumber == accNumber);

                if (receiver != null)
                {
                    User user = Admin.userList.Find(x => x.Id == userId);

                    if (user.AccountBalance >= amountToSub)
                    {
                        Console.WriteLine("Przelewam środki...");

                        receiver.AccountBalance += amountToSub;
                        user.AccountBalance -= amountToSub;

                        Console.WriteLine("Przelano kwotę " + amountToSub + " na konto o numerze " + receiver.AccountNumber);

                        user.OperationHistory.Add("Przelano kwotę " + amountToSub + " na konto o numerze " + receiver.AccountNumber);
                        receiver.OperationHistory.Add("Otrzymano przelew na kwotę " + amountToSub + " od użytkownika o numerze konta " + user.AccountNumber);
                    }
                    else
                    {
                        Console.WriteLine("Błąd");
                    }
                }
                else
                {
                    Console.WriteLine("Błąd");
                }
            }
            else
            {
                Console.WriteLine("Błąd");
            }
        }


        public void takeCredit(int userId)
        {
            String number = "0";
            double amountToAdd = 0;
            Console.WriteLine("Jaką kwotę chcesz pożyczyć?,oprocentowanie kredytu 10% ");
            number = Console.ReadLine();

            if (double.TryParse(number, out amountToAdd))
            {
                Console.WriteLine("Zasilam konto...");

                User user = Admin.userList.Find(x => x.Id == userId);
                user.AccountBalance += amountToAdd;

                Console.WriteLine("Dodano kwotę " + amountToAdd + " do konta");

                user.Credit += amountToAdd * 1.1;
                user.Credit = Math.Round(user.Credit, 2);

                user.OperationHistory.Add("Wzięto kredyt na " + amountToAdd + ", suma kredytów " +  user.Credit);

            }
            else
            {
                Console.WriteLine("Błąd");
            }
        }


        public void payUpCredit(int userId)
        {
            String number = "0";
            double amountToSub = 0;
            Console.WriteLine("Jaką kwotę chcesz spłacić ");
            number = Console.ReadLine();

            if (double.TryParse(number, out amountToSub))
            {
                
                User user = Admin.userList.Find(x => x.Id == userId);

                if(user.AccountBalance >= amountToSub && amountToSub <= user.Credit )
                {
                    user.AccountBalance -= amountToSub;
                    user.Credit -= amountToSub;

                    Console.WriteLine("Spłacono kwotę " + amountToSub + ", pozostało kredytu " + user.Credit);

                    user.OperationHistory.Add("Spłacono kwotę " + amountToSub + ", pozostało kredytu" + user.Credit);

                }
                else
                {
                    Console.WriteLine("Błąd");
                }

            }
            else
            {
                Console.WriteLine("Błąd");
            }
        }

        public void viewOperationHistory(int userId)
        {
            User user = Admin.userList.Find(x => x.Id == userId);
            int i = 1;

            if(user.OperationHistory != null)
            {

                foreach(String operation in user.OperationHistory)
                {
                    Console.WriteLine($"{i}. " + operation);
                    i++;
                }
            }

        }


    }
}
