using CSharpBankProject.src.BankConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Data
{
    internal class AccountRepository
    {
        //properties
        private Dictionary<Guid, Account> Accounts { get; set; }

        //methods
        //constructor
        public AccountRepository()
        {
            Accounts = new Dictionary<Guid, Account>();
        }

        //AddAccount
        public void AddAccount(Guid Id, Account account)
        {
            Accounts.Add(Id, account);
        }

        //Verify if an account with the given account number exists in the Accounts dictionary
        public bool VerifyAccountExists(int accountNumber)
        {
            return Accounts.Values.Any(a => a.AccountNumber == accountNumber);
        }

        //Find and return the account with the given account number from the Accounts dictionary
        public Account FindAccountByAccountNumber(int accountNumber)
        {
            return Accounts.Values.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        //Verify if the PIN of the account with the given account number matches the provided PIN
        public bool VerifyAccountPin(int accountNumber, int pin)
        {
            var account = FindAccountByAccountNumber(accountNumber);
            return account != null && account.Pin == pin;
        }

        //Verify if the account with the given account number has sufficient balance
        public bool VerifyAccountBalance(int accountNumber, decimal amount)
        {
            var account = FindAccountByAccountNumber(accountNumber);
            return account != null && account.Balance >= amount;
        }

        //Update the balance of the account with the given account number
        public void UpdateAccountBalance(string transactionType, int accountNumber, int? recipientAccountNumber, decimal amount)
        {
            var account = FindAccountByAccountNumber(accountNumber);
            var recipientAccount = recipientAccountNumber.HasValue ? FindAccountByAccountNumber(recipientAccountNumber.Value) : null;
            if (account != null)
            {
                switch (transactionType)
                {
                    case "Deposit":
                        account.Balance += amount;
                        break;
                    case "Withdraw":
                        account.Balance -= amount;
                        break;
                    case "Transfer":
                        account.Balance -= amount;
                        recipientAccount.Balance += amount;
                        break;
                }
            }
        }

        //Generate a unique account number for a new account
        //This method generates a random 6-digit account number and checks if it already exists in the Accounts dictionary. If it does, it generates a new number until a unique one is found.
        public int GenerateAccountNumber()
        {
            Random random = new Random();
            int accountNumber;
            // Check if the generated account number already exists
            do
            {
                accountNumber = random.Next(100000, 999999);
                return accountNumber;
            }
            while (VerifyAccountExists(accountNumber));

        }
        //Create a 4-digit PIN for the account
        //This method asks the user to enter a 4-digit PIN for the account. It checks if the entered PIN is valid (i.e., it is a 4-digit number) and returns it. If the entered PIN is invalid, it prompts the user to enter a valid PIN.
        public int CreatePin()
        {
            int pin;
            do
            {
                Console.Write("Enter a 4-digit PIN for your account: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out pin) && input.Length == 4)
                {
                    return pin;
                }
                else
                {
                    Console.WriteLine("Invalid PIN. Please enter a 4-digit number.");
                }
            } while (true);

        }
    }
}
