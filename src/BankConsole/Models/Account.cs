using CSharpBankProject.src.BankConsole.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Models
{
    internal class Account
    {
        public int AccountNumber { get; init; }
        public decimal Balance { get; set; }
        public int Pin { get; private set; }
        public User User { get; }
        private AccountRepository AccountRepository { get; }
        public string BalanceCurrency => this.Balance.ToString("0.00");

        public Account(int accountNumber, decimal balance, int pin, User user, AccountRepository accountRepository)
        { 
            this.AccountNumber = accountNumber;
            this.Balance = balance;
            this.Pin = pin;
            this.User = user;
            this.AccountRepository = accountRepository;
        }

        enum TransactionType
        {
            Deposit,
            Withdraw,
            Transfer
        }

        public void Deposit(decimal amount, int pin)
        {
            bool pinVerified = AccountRepository.VerifyAccountPin(User.Id, pin);
            if (pinVerified)
            {
                AccountRepository.UpdateAccountBalance(TransactionType.Deposit.ToString(), AccountNumber, amount);
                Console.WriteLine($"Deposit of {amount} successful. New balance: {BalanceCurrency}");
            }
            else
            {
                // Handle incorrect PIN
                Console.WriteLine("Incorrect PIN. Deposit failed.");
            }

        }

        public void Withdraw(decimal amount, int pin)
        {
            bool hasSufficientBalance = AccountRepository.VerifyAccountBalance(User.Id, amount);
            if (hasSufficientBalance)
            {
                bool pinVerified = AccountRepository.VerifyAccountPin(User.Id, pin);
                if (pinVerified)
                {
                    AccountRepository.UpdateAccountBalance(TransactionType.Withdraw.ToString(), AccountNumber, amount);
                    Console.WriteLine($"Withdrawal of {amount} successful. New balance: {BalanceCurrency}");
                }
                else
                {
                    // Handle incorrect PIN
                    Console.WriteLine("Incorrect PIN. Withdrawal failed.");

                }
            }
            else
            {
                // Handle insufficient balance
                Console.WriteLine("Insufficient balance. Withdrawal failed.");
            }
        }

        public void Transfer(int recipientAccount, decimal amount, int pin)
        {
            bool hasSufficientBalance = AccountRepository.VerifyAccountBalance(User.Id, amount);
            if (hasSufficientBalance)
            {
                bool pinVerified = AccountRepository.VerifyAccountPin(User.Id, pin);
                if (pinVerified)
                {
                    var recipientExists = AccountRepository.VerifyAccountExists(recipientAccount);
                    if (recipientExists)
                    {
                        var recipient = AccountRepository.FindAccountByAccountNumber(recipientAccount);
                        AccountRepository.UpdateAccountBalance(TransactionType.Transfer.ToString(), AccountNumber, amount, recipientAccount);
                        Console.WriteLine($"Transfer of {amount} to {recipient.User.Username}'s account successful. New balance: {BalanceCurrency}");
                    }
                    else
                    {
                        Console.WriteLine("Recipient account does not exist. Transfer failed.");
                    }
                }
                else
                {
                    // Handle incorrect PIN
                    Console.WriteLine("Incorrect PIN. Transfer failed.");
                }
            }
            else
            {
                // Handle insufficient balance
                Console.WriteLine("Insufficient balance. Transfer failed.");
            }

        }

        public void UpdatePin(int newPin, int currentPin)
        {
            bool pinVerified = AccountRepository.VerifyAccountPin(User.Id, currentPin);
            if (pinVerified)
            {
                this.Pin = newPin;
                Console.WriteLine("PIN updated successfully.");
            }
            else
            {
                Console.WriteLine("Incorrect current PIN. PIN update failed.");
            }
        }

        public (string UserName, int AccountNumber, string Balance) DisplayAccountInfo(int pin)
        {
            bool pinVerified = AccountRepository.VerifyAccountPin(User.Id, pin);
            if (pinVerified)
            {
                return (User.Name, AccountNumber, BalanceCurrency);
            }
            else
            {
                return default;
            }
        }
    }
}
