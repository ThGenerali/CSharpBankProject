using CSharpBankProject.src.BankConsole.Data;
using CSharpBankProject.src.BankConsole.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Services
{
    internal class Services
    {
        private ServiceRepository serviceRepository { get; }

        public void RegisterUser()
        {
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter your surname: ");
            string surname = Console.ReadLine();
            if(serviceRepository.verifyNameAndSurname(name, surname))
            {
                Console.WriteLine($"Are you sure you want to register with the name: {name} {surname}? (y/n)");
                char confirmation = Console.ReadKey().KeyChar;
                confirmation = char.ToLower(confirmation);
                if (confirmation == 'y')
                {
                    name.Trim();
                    string username = $"{name} {surname.Trim()}";
                    Console.WriteLine("Please enter your password: ");
                    string password = Console.ReadLine();
                    Console.WriteLine("Please confirm your password: ");
                    string confirmPassword = Console.ReadLine();
                    if(serviceRepository.VerifyRegisterPassword(password, confirmPassword))
                    {
                        bool validPin = false;
                        while (!validPin)
                        {
                            Console.WriteLine("Please enter your4-digit PIN: ");
                            int pin = int.Parse(Console.ReadLine());
                            if(serviceRepository.Verify4DigitPin(pin))
                            {
                                validPin = true;
                                serviceRepository.RegisterUser(name, username, password, pin);
                            }
                            else
                            {
                                Console.WriteLine("Invalid PIN. Please enter a 4-digit PIN.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Registration cancelled.");
                }
            } else {
                throw new ArgumentException("Name or Surname cannot be empty. Please try again.");
            }
        }

        public ArrayList[] LoginMenu()
        {
            Console.WriteLine(@"
Please enter your name and surname: 
(Remeber to add a space between them and don't at the final)");
            string nameLogin = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            string password = Console.ReadLine();
            var userAccount = serviceRepository.Login(nameLogin, password);
            return new ArrayList[] { new ArrayList { userAccount.User.Name, userAccount.Balance } };
        }

        public void TransactionMenu()
        {
            Console.WriteLine(@"
Please select a transaction type:
1. Deposit
2. Withdraw
3. Transfer");
            int transactionType = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Please enter your PIN: ");
            int pin = Convert.ToInt32(Console.ReadLine());
        }
    }
}
