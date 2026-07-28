using CSharpBankProject.src.BankConsole.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSharpBankProject.src.BankConsole.Data.ServiceRepository;

namespace CSharpBankProject.src.BankConsole.Data
{
    internal class ServiceRepository
    {
        private UserRepository userRepository;
        private AccountRepository accountRepository;


        public class AccountSession
        {
            public Account account { get; }
            public AccountSession(Account account)
            {
                this.account = account;
            }
        }

        public AccountSession accountSession;

        public bool verifyNameAndSurname(string name, string surname)
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname);
        }

        //VerifyRegisterInfo method checks if the provided registration information (name, username, and password) is valid for creating a new user account. It verifies that the name and username are not empty and that the password meets certain criteria (e.g., minimum length).
        public bool VerifyRegisterPassword(string password, string confirmPassword)
        {
            if (confirmPassword == password) { return true; }
            throw new ArgumentException("Passwords do not match. Registration cancelled.");
        }

        public void RegisterUser(string name, string username, string password, int pin)
        {
            User user = new User(name, username, password);
            userRepository.AddUser(user.Id, user);
            CreateAccount(user, pin);
        }

        public void CreateAccount(User user, int pin)
        {
            int accountNumber = accountRepository.GenerateAccountNumber();
            Account account = new Account(user.Id, accountNumber, 0m, pin, user);
            accountRepository.AddAccount(user.Id, account);
        }

        public bool Verify4DigitPin(int pin)
        {
            return pin.ToString().Length == 4;
        }
        public (string UserName, string Balance) Login(string username, string password)
        {
            var userName = userRepository.VerifyUsername(username);
            if (userName  && userRepository.VerifyPassword(username, password))
            {
                Console.WriteLine("Login successful.");
                var user = userRepository.GetUser(username);
                accountSession = new AccountSession(accountRepository.GetAccountByUserId(user.Id));
                return (user.Username, accountSession.account.BalanceCurrency);
            }
            else
            {
                throw new UnauthorizedAccessException("Credentials are incorrect. Login failed."); ;
            }
        }

        public void Deposit(decimal amount, int pin)
        {
            accountSession.account.Deposit(amount, pin);
        }

        public void Withdraw(decimal amount, int pin)
        {
            accountSession.account.Withdraw(amount, pin);
        }

        public void Transfer(int targetAccountNumber, decimal amount, int pin)
        {
            accountSession.account.Transfer(targetAccountNumber, amount, pin);
        }

        public void UpdatePin(int currentPin, int? newPin)
        {
            if (newPin != null)
            {
                accountSession.account.UpdatePin(currentPin, newPin.Value);
            }
            else
            {
                throw new ArgumentException("Cannot update the PIN into a null PIN.");
            }
        }

        public (string UserName, int AccountNumber, string Balance) DisplayAccountInfo(int pin)
        {
            var accountInfo = accountSession.account.DisplayAccountInfo(pin);
            return (accountInfo.UserName, accountInfo.AccountNumber, accountInfo.Balance);
        }
    }
}
