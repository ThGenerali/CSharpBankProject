using CSharpBankProject.src.BankConsole.Data;
using System;
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
        private AccountRepository accountRepository { get; }

        public Account(Guid accountId, int accountNumber, decimal balance, int pin, User user, AccountRepository accountRepository = null)
        {
            Guid id = user.Id;
            this.AccountId = id;
            this.AccountNumber = accountNumber;
            this.Balance = balance;
            this.Pin = pin;
            this.User = user;
            this.accountRepository = accountRepository ?? new AccountRepository();
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
                accountRepository.UpdateAccountBalance(TransactionType.Deposit.ToString(), AccountNumber, null, amount);
                Console.WriteLine($"Deposit of {amount} successful. New balance: {Balance}");
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
                    accountRepository.UpdateAccountBalance(TransactionType.Withdraw.ToString(), AccountNumber, null, amount);
                    Console.WriteLine($"Withdrawal of {amount} successful. New balance: {Balance}");
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
                        accountRepository.UpdateAccountBalance(TransactionType.Transfer.ToString(), AccountNumber, recipientAccount, amount);
                        Console.WriteLine($"Transfer of {amount} to account {recipient.Balance} successful. New balance: {Balance}");
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
    }
}
