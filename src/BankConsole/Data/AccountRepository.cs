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
            if (VerifyAccountExists(accountNumber))
            {
                return Accounts.Values.FirstOrDefault(a => a.AccountNumber == accountNumber);
            }
            throw new KeyNotFoundException("Account not found.");
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
        public void UpdateAccountBalance(string transactionType, int accountNumber, decimal amount, int? recipientAccountNumber = null)
        {
            var account = FindAccountByAccountNumber(accountNumber);
            var recipientAccount = recipientAccountNumber.HasValue ? FindAccountByAccountNumber(recipientAccountNumber.Value) : null;
            if (account != null )  
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
            }
            while (VerifyAccountExists(accountNumber));
            return accountNumber;
        }

        public Account GetAccountByUserId(Guid userId)
        {
            Accounts.TryGetValue(userId, out Account account);
            return account;
        }
    }
}
