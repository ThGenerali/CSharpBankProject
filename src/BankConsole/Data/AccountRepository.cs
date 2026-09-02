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
            if (Accounts.Values.Any(a => a.AccountNumber == accountNumber))
            {
                return true;
            }
            throw new KeyNotFoundException("Account not found.");
        }

        //Verify if the PIN of the account with the given account number matches the provided PIN
        public bool VerifyAccountPin(Guid id, int pin)
        {
            int accountPin = GetAccountPin(id);
            if (accountPin == pin)
            {
                return true;
            }
            throw new UnauthorizedAccessException("Invalid PIN.");
        }

        //Verify if the account with the given account number has sufficient balance
        public bool VerifyAccountBalance(Guid id, decimal amount)
        {
            var account = FindAccountByUserId(id);
            if (account.Balance >= amount)
            {
                return true;
            }
            throw new InvalidOperationException("Insufficient balance.");
        }

        public Account FindAccountByAccountNumber(int accountNumber)
        {
            if(VerifyAccountExists(accountNumber))
            {
                return Accounts.Values.FirstOrDefault(a => a.AccountNumber == accountNumber);
            }
            //return some value because if the account does not exist, the method will throw an exception in VerifyAccountExists
            return null;
        }

        public Account FindAccountByUserId(Guid userId)
        {
            return Accounts.Values.FirstOrDefault(a => a.User.Id == userId);
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
                    case "Transfer" when recipientAccount != null:
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

        public int GetAccountPin(Guid id)
        {
            Account account = Accounts[id];
            return account.Pin;
        }
    }
}
