using CSharpBankProject.src.BankConsole.Data;
using CSharpBankProject.src.BankConsole.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace CSharpBankProject.src.BankConsole.Services
{
    internal class Services
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        private ServiceRepository serviceRepository = new ServiceRepository();

        public void RegisterUser()
        {
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();
            name = textInfo.ToTitleCase(name.ToLower());
            Console.Clear();
            Console.WriteLine("Please enter your surname: ");
            string surname = Console.ReadLine();
            surname = textInfo.ToTitleCase(surname.ToLower());
            Console.Clear();
            if (serviceRepository.verifyNameAndSurname(name, surname))
            {
                Console.WriteLine($"Are you sure you want to register with the name: {name.Trim()} {surname.Trim()}? (y/n)");
                char confirmation = Console.ReadKey().KeyChar;
                Console.Clear();
                confirmation = char.ToLower(confirmation);
                if (confirmation == 'y')
                {
                    string username = $"{name.Trim()} {surname.Trim()}";
                    Console.WriteLine("Please enter your password: ");
                    string password = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Please confirm your password: ");
                    string confirmPassword = Console.ReadLine();
                    Console.Clear();
                    if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmPassword) && serviceRepository.VerifyRegisterPassword(password, confirmPassword))
                    {
                        bool validPin = false;
                        while (!validPin)
                        {
                            Console.WriteLine("Please enter your4-digit PIN: ");
                            int pin = int.Parse(Console.ReadLine());
                            Console.Clear();
                            if (serviceRepository.Verify4DigitPin(pin))
                            {
                                validPin = true;
                                serviceRepository.RegisterUser(name, username, password, pin);
                                Console.WriteLine($"The user {username} has been registred!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid PIN. Please enter a 4-digit PIN.");
                            }
                        }
                    } else { Console.WriteLine("Passwords do not match. Registration cancelled."); }
                } else { Console.WriteLine("Registration cancelled."); }
            } else { Console.WriteLine("Name or Surname cannot be empty. Please try again."); }
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
        }

        public (string UserName, string Balance) LoginMenu()
        {
            Console.WriteLine("Please enter your name and surname:");
            string nameLogin = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Please enter your password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Logging in...");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            var userAccount = serviceRepository.Login(nameLogin, password);
            if (userAccount == default)
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                System.Threading.Thread.Sleep(1500);
                Console.Clear();
                return default;
            } else
            {
                Console.WriteLine("Login successful.");
                System.Threading.Thread.Sleep(1500);
                Console.Clear();
                return userAccount;
            }
        }

        public void Transaction()
        {
            Console.WriteLine(@"
1. Deposit
2. Withdraw
3. Transfer
4. Cancel Transaction
Please select a transaction type:
");
            int transactionType = int.Parse(Console.ReadKey().KeyChar.ToString());
            Console.Clear();
            switch (transactionType)
            {
                case 1:
                    Console.WriteLine("You selected Deposit.");
                    Console.WriteLine("Please enter the amount: ");
                    decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Please enter your PIN: ");
                    int depositPin = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Processing deposit...");
                    System.Threading.Thread.Sleep(2000);
                    serviceRepository.Deposit(depositAmount, depositPin);
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 2:
                    Console.WriteLine("You selected Withdraw.");
                    Console.WriteLine("Please enter the amount: ");
                    decimal withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Please enter your PIN: ");
                    int withdrawPin = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Processing withdrawal...");
                    System.Threading.Thread.Sleep(2000);
                    serviceRepository.Withdraw(withdrawAmount, withdrawPin);
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    break;
                case 3:
                    Console.WriteLine("You selected Transfer.");
                    Console.WriteLine("Please enter the target account number: ");
                    int targetAccountNumber = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    var targetAccountName = serviceRepository.GetUserNameByAccountNumber(targetAccountNumber);
                    if (!string.IsNullOrEmpty(targetAccountName))
                    {
                        Console.WriteLine($"You are transferring to {targetAccountName}. Are you sure? (y/n)");
                        char confirmation = Console.ReadKey().KeyChar;
                        Console.Clear();
                        if (char.ToLower(confirmation) != 'y')
                        {
                            Console.WriteLine("Transfer cancelled.");
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                            break;
                        }
                        Console.WriteLine("Please enter the amount: ");
                        decimal transferAmount = Convert.ToDecimal(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Please enter your PIN: ");
                        int transferPin = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Processing transfer...");
                        System.Threading.Thread.Sleep(2000);
                        serviceRepository.Transfer(targetAccountNumber, transferAmount, transferPin);
                        Console.Clear();
                    } else { 
                        Console.WriteLine("Account not found! Transferation canceled."); 
                        break;
                    }
                        break;
                case 4:
                    Console.WriteLine("Transaction cancelled.");
                    System.Threading.Thread.Sleep(1000);
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    break;
            }
        }
        public void ChangePinMenu()
        {
            Console.WriteLine("Please enter your current PIN: ");
            int currentPin = int.TryParse(Console.ReadLine(), out int tempCurrentPin) ? tempCurrentPin : 0;
            Console.WriteLine("Please enter your new PIN: ");
            int newPin = int.TryParse(Console.ReadLine(), out int tempNewPin) ? tempNewPin : 0;
            if (serviceRepository.Verify4DigitPin(newPin) && serviceRepository.Verify4DigitPin(currentPin))
            {
                Console.WriteLine("Processing PIN change...");
                System.Threading.Thread.Sleep(1000);
                serviceRepository.UpdatePin(currentPin, newPin);
            } else { Console.WriteLine("Invalid PIN. Please ensure you enter your current and new PIN correctly."); }
            Thread.Sleep(1500);
        }

        public void ShowAccountDetailsMenu()
        {
            Console.WriteLine("Please enter your PIN to view account details: ");
            int pin = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Retrieving account details...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            var accountDetails = serviceRepository.DisplayAccountInfo(pin);
            if (accountDetails != default) {Console.WriteLine($"Account Details:\n\nName: {accountDetails.UserName}\nAccount Number: {accountDetails.AccountNumber}\nBalance: {accountDetails.Balance}");
            } else { Console.WriteLine("Invalid PIN. Unable to retrieve account details."); }
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();      
        }

        public string GetUpdatedBalance()
        {
            return serviceRepository.GetUpdatedBalance();
        }
    }
}
