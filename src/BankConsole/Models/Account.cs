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

        public bool Deposit(decimal amount, int pin)
        {
            bool hasSufficientBalance = accountRepository.VerifyAccountBalance(AccountNumber, amount);
            if(hasSufficientBalance)
            {
                bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
                if(pinVerified)
                {
                    accountRepository.UpdateAccountBalance(TransactionType.Deposit.ToString(), AccountNumber, null, amount);
                    return true;
                } else {
                    // Handle incorrect PIN
                    Console.WriteLine("Incorrect PIN. Deposit failed.");
                    return false;
                }
            } else {
                // Handle insufficient balance
                Console.WriteLine("Insufficient balance. Deposit failed.");
                return false;
            }
        }

        public bool Withdraw(decimal amount, int pin)
        {
            bool hasSufficientBalance = accountRepository.VerifyAccountBalance(AccountNumber, amount);
            if (hasSufficientBalance)
            {
                bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
                if (pinVerified)
                {
                    accountRepository.UpdateAccountBalance(TransactionType.Withdraw.ToString(), AccountNumber, null, amount);
                    return true;
                }
                else
                {
                    // Handle incorrect PIN
                    Console.WriteLine("Incorrect PIN. Withdrawal failed.");
                    return false;
                }
            } else {
                // Handle insufficient balance
                Console.WriteLine("Insufficient balance. Withdrawal failed.");
                return false;
            }
        }

        public bool Transfer(Account? recipientAccount, decimal amount, int pin)
        {
            if (recipientAccount == null)
            {
                // Handle null recipient account
                Console.WriteLine($"{nameof(recipientAccount)} Account didn't find in the system.");
            }

            bool hasSufficientBalance = accountRepository.VerifyAccountBalance(AccountNumber, amount);
            if (hasSufficientBalance)
            {
                bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
                if (pinVerified)
                {
                    accountRepository.UpdateAccountBalance(TransactionType.Transfer.ToString(), AccountNumber, recipientAccount.AccountNumber, amount);
                    recipientAccount.Balance += amount; // Update the recipient's balance
                    return true;
                }
                else
                {
                    // Handle incorrect PIN
                    Console.WriteLine("Incorrect PIN. Transfer failed.");
                    return false;
                }
            } else {
                // Handle insufficient balance
                Console.WriteLine("Insufficient balance. Transfer failed.");
                return false;
            }
            
        }
    }
}
