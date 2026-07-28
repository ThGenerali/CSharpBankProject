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
        public Guid AccountId { get; init; }
        public int AccountNumber { get; init; }
        public decimal Balance { get; set; }
        public int Pin { get; private set; }
        public User User { get; }
        private AccountRepository accountRepository;
        public string BalanceCurrency => this.Balance.ToString("0.00");

        public Account(Guid accountId, int accountNumber, decimal balance, int pin, User user)
        {
            this.AccountId = user.Id;
            this.AccountNumber = accountNumber;
            this.Balance = balance;
            this.Pin = pin;
            this.User = user;
        }

        enum TransactionType
        {
            Deposit,
            Withdraw,
            Transfer
        }

        public void Deposit(decimal amount, int pin)
        {
            bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
            if (pinVerified)
            {
                accountRepository.UpdateAccountBalance(TransactionType.Deposit.ToString(), AccountNumber, amount);
                Console.WriteLine($"Deposit of {amount} successful. New balance: {BalanceCurrency}");
            }
            else
            {
                // Handle incorrect PIN
                throw new UnauthorizedAccessException("Incorrect PIN. Deposit failed.");
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
                    accountRepository.UpdateAccountBalance(TransactionType.Withdraw.ToString(), AccountNumber, amount);
                    Console.WriteLine($"Withdrawal of {amount} successful. New balance: {BalanceCurrency}");
                }
                else
                {
                    // Handle incorrect PIN
                    throw new UnauthorizedAccessException("Incorrect PIN. Withdrawal failed.");

                }
            }
            else
            {
                // Handle insufficient balance
                throw new InvalidOperationException("Insufficient balance. Withdrawal failed.");
            }
        }

        public void Transfer(int recipientAccount, decimal amount, int pin)
        {
            bool hasSufficientBalance = accountRepository.VerifyAccountBalance(AccountNumber, amount);
            if (hasSufficientBalance)
            {
                bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
                if (pinVerified)
                {
                    var recipientExists = accountRepository.VerifyAccountExists(recipientAccount);
                    if (recipientExists)
                    {
                        var recipient = accountRepository.FindAccountByAccountNumber(recipientAccount);
                        accountRepository.UpdateAccountBalance(TransactionType.Transfer.ToString(), AccountNumber, amount, recipientAccount);
                        Console.WriteLine($"Transfer of {amount} to {recipient.User.Username}'s account successful. New balance: {BalanceCurrency}");
                    }
                    else
                    {
                        throw new ArgumentException("Recipient account does not exist. Transfer failed.");
                    }
                }
                else
                {
                    // Handle incorrect PIN
                    throw new UnauthorizedAccessException("Incorrect PIN. Transfer failed.");
                }
            }
            else
            {
                // Handle insufficient balance
                throw new InvalidOperationException("Insufficient balance. Transfer failed.");
            }

        }

        public void UpdatePin(int newPin, int currentPin)
        {
            bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, currentPin);
            if (pinVerified)
            {
                this.Pin = newPin;
                Console.WriteLine("PIN updated successfully.");
            }
            else
            {
                throw new UnauthorizedAccessException("Incorrect current PIN. PIN update failed.");
            }
        }

        public (string UserName, int AccountNumber, string Balance) DisplayAccountInfo(int pin)
        {
            bool pinVerified = accountRepository.VerifyAccountPin(AccountNumber, pin);
            if (pinVerified)
            {
                return (User.Name, AccountNumber, BalanceCurrency);
            }
            else
            {
                throw new UnauthorizedAccessException("Incorrect PIN. Cannot display account information.");
            }
        }
    }
}
