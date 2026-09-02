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
        private UserRepository userRepository = new UserRepository();
        private AccountRepository accountRepository = new AccountRepository();


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
            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname)) { return true; }
            throw new InvalidOperationException("Name and surname must not be empty.");
        }

        //VerifyRegisterInfo method checks if the provided registration information (name, username, and password) is valid for creating a new user account. It verifies that the name and username are not empty and that the password meets certain criteria (e.g., minimum length).
        public bool VerifyRegisterPassword(string password, string confirmPassword)
        {
            if (confirmPassword == password) { return true; }
            throw new InvalidOperationException("Passwords do not match.");
        }

        public void RegisterUser(string name, string username, string password, int pin)
        {
            Guid userId = Guid.NewGuid();
            User user = new User(userId, name, username, password, userRepository);
            userRepository.AddUser(user.Id, user);
            CreateAccount(user, pin);
        }

        public void CreateAccount(User user, int pin)
        {
            int accountNumber = accountRepository.GenerateAccountNumber();
            Account account = new Account(accountNumber, 0m, pin, user, accountRepository);
            accountRepository.AddAccount(account.User.Id, account);
        }

        public string GetUserNameByAccountNumber(int accountNumber)
        {
            var account = accountRepository.FindAccountByAccountNumber(accountNumber);
            if (account != null)
            {
                var user = userRepository.GetUserByUserName(account.User.Username);
                return user.Username;
            }
            throw new KeyNotFoundException("Account not found.");
        }


        public bool Verify4DigitPin(int pin)
        {
            if (pin.ToString().Length == 4) { return true; }
            throw new InvalidOperationException("Invalid PIN. It must be 4 digits.");
        }


        public (string UserName, string Balance) Login(string username, string password)
        {
            var userName = userRepository.VerifyUsername(username.Trim());
            if (userName  && userRepository.VerifyPassword(username, password))
            {
                var user = userRepository.GetUserByUserName(username);
                accountSession = new AccountSession(accountRepository.GetAccountByUserId(user.Id));
                return (user.Username, accountSession.account.BalanceCurrency);
            }
            throw new UnauthorizedAccessException("Invalid Credentials.");
            
        }

        public void Deposit(decimal amount, int pin) { accountSession.account.Deposit(amount, pin); }

        public void Withdraw(decimal amount, int pin) { accountSession.account.Withdraw(amount, pin); }

        public void Transfer(int targetAccountNumber, decimal amount, int pin) { accountSession.account.Transfer(targetAccountNumber, amount, pin); }
        

        public void UpdatePin(int currentPin, int newPin) { accountSession.account.UpdatePin(currentPin, newPin); } 
        

        public (string UserName, int AccountNumber, string Balance) DisplayAccountInfo(int pin)
        {
            var accountInfo = accountSession.account.DisplayAccountInfo(pin);
            if (accountInfo != default)
            { return (accountInfo.UserName, accountInfo.AccountNumber, accountInfo.Balance); 
            } else { throw new UnauthorizedAccessException("Invalid PIN."); }
        }

        public string GetUpdatedBalance() { return accountSession.account.BalanceCurrency; }
    }
}
