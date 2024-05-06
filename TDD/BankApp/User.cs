using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class User
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public double AccountBalance { get; set; }
        public double Credit {  get; set; }
        public List<String> OperationHistory { get; set; }


        public User()
        {
            AccountBalance = 0.0;
            Credit = 0.0;
            OperationHistory = new List<string>(); 
        }



    }
}
