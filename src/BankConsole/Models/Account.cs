using CSharpBankProject.src.BankConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Models
{
    internal class Account : User
    {
        public Guid AccountId { get; init; }
        public int AccountNumber { get; private set; }
        public decimal Balance { get; set; }
        public int Pin { get; private set; }

        public Account(string name, string username, string password, Guid AccountId, int AccountNumber, decimal Balance, int Pin, User user) : base(name, username, password)
        {
            Guid id = user.Id;
            this.AccountId = id;
            this.AccountNumber = AccountNumber;
            this.Balance = Balance;
            this.Pin = Pin;
        }

        // Create an instance of the AccountRepository class to access the account data "dependency injection"
        protected AccountRepository accountRepository = new AccountRepository();

        enum TransactionType
        {
            Deposit,
            Withdraw,
            Transfer
        }

        public void Deposit(decimal amount, int pin)
        {
            bool hasSufficientBalance = accountRepository.VerifyAccountBalance(AccountNumber, amount);
            if(hasSufficientBalance)
            {
                bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
                if(pinVerified)
                {
                    accountRepository.UpdateAccountBalance(TransactionType.Deposit.ToString(), AccountNumber, null, amount);
                } else {
                    // Handle incorrect PIN
                }
            }
        }

        public void Withdraw(decimal amount, int pin)
        {
            bool hasSufficientBalance = accountRepository.VerifyAccountBalance(AccountNumber, amount);
            if (hasSufficientBalance)
            {
                bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
                if (pinVerified)
                {
                    accountRepository.UpdateAccountBalance(TransactionType.Deposit.ToString(), AccountNumber, null, amount);
                }
                else
                {
                    // Handle incorrect PIN
                }
            }
        }

        public void Transfer(Account? recipientAccount, decimal amount, int pin)
        {
            if (recipientAccount == null)
            {
                // Handle null recipient account
                return;
            }

            bool hasSufficientBalance = accountRepository.VerifyAccountBalance(AccountNumber, amount);
            if (hasSufficientBalance)
            {
                bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
                if (pinVerified)
                {
                    accountRepository.UpdateAccountBalance(TransactionType.Transfer.ToString(), AccountNumber, recipientAccount.AccountNumber, amount);
                }
                else
                {
                    // Handle incorrect PIN
                }
            }
        }
    }
}
