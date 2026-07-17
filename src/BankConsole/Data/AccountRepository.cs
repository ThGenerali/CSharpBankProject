using CSharpBankProject.src.BankConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Data
{
    internal class AccountRepository : UserRepository
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


    }
}
