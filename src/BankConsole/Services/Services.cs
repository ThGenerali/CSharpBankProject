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
            if (serviceRepository.verifyNameAndSurname(name, surname))
            {
                Console.WriteLine($"Are you sure you want to register with the name: {name.Trim()} {surname.Trim()}? (y/n)");
                char confirmation = Console.ReadKey().KeyChar;
                confirmation = char.ToLower(confirmation);
                if (confirmation == 'y')
                {

                    string username = $"{name.Trim()} {surname.Trim()}";
                    Console.WriteLine("Please enter your password: ");
                    string? password = Console.ReadLine();
                    Console.WriteLine("Please confirm your password: ");
                    string? confirmPassword = Console.ReadLine();
                    if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmPassword) && serviceRepository.VerifyRegisterPassword(password, confirmPassword))
                    {
                        bool validPin = false;
                        while (!validPin)
                        {
                            Console.WriteLine("Please enter your4-digit PIN: ");
                            int pin = int.Parse(Console.ReadLine());
                            if (serviceRepository.Verify4DigitPin(pin))
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
                    else { throw new ArgumentException("Passwords do not match. Registration cancelled."); }
                }
                else
                {
                    throw new Exception("Registration cancelled.");
                }
            }
            else
            {
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
            return userAccount;
        }

        public void TransactionMenu()
        {
            Console.WriteLine(@"
1. Deposit
2. Withdraw
3. Transfer
Please select a transaction type:
");
            int transactionType = int.Parse(Console.ReadLine());
            switch (transactionType)
            {
                case 1:
                    Console.WriteLine("You selected Deposit.");
                    Console.WriteLine("Please enter the amount: ");
                    decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Please enter your PIN: ");
                    int depositPin = Convert.ToInt32(Console.ReadLine());
                    serviceRepository.Deposit(depositAmount, depositPin);
                    break;
                case 2:
                    Console.WriteLine("You selected Withdraw.");
                    Console.WriteLine("Please enter the amount: ");
                    decimal withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Please enter your PIN: ");
                    int withdrawPin = Convert.ToInt32(Console.ReadLine());
                    serviceRepository.Withdraw(withdrawAmount, withdrawPin);
                    break;
                case 3:
                    Console.WriteLine("You selected Transfer.");
                    Console.WriteLine("Please enter the target account number: ");
                    int targetAccountNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Please enter the amount: ");
                    decimal transferAmount = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Please enter your PIN: ");
                    int transferPin = Convert.ToInt32(Console.ReadLine());
                    serviceRepository.Transfer(targetAccountNumber, transferAmount, transferPin);
                    break;
                default:
                    throw new InvalidOperationException("Invalid selection. Please try again.");
            }
        }
        public void ChangePinMenu()
        {
            Console.WriteLine("Please enter your current PIN: ");
            int currentPin = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter your new PIN: ");
            int newPin = int.Parse(Console.ReadLine());
        }

        public void ShowAccountDetailsMenu()
        {
            Console.WriteLine("Please enter your PIN to view account details: ");
            int pin = int.Parse(Console.ReadLine());
            var accountDetails = serviceRepository.DisplayAccountInfo(pin);
            Console.WriteLine($"Account Details:\nName: {accountDetails[0]}\nAccount Number: {accountDetails[1]}\nBalance: {accountDetails[2]}");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}
