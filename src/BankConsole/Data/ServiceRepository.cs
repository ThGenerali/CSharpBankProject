using CSharpBankProject.src.BankConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Data
{
    internal class ServiceRepository
    {
        private UserRepository userRepository { get; }
        private AccountRepository accountRepository { get; }

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
        public Account Login(string username, string password)
        {
            var userName = userRepository.VerifyUsername(username);
            if (userName != null)
            {
                if(userRepository.VerifyPassword(username, password))
                {
                    Console.WriteLine("Login successful.");
                    var user = userRepository.GetUser(username);
                   return accountRepository.GetAccountByUserId(user.Id);
                }
                else
                {
                    throw new UnauthorizedAccessException("Incorrect password. Login failed.");
                }
            }
            else
            {
                throw new Exception("User not found. Login failed.");
            }
        }

        public void Transaction(int accountNumber, int pin, string transactionType, decimal amount, int? recipientAccountNumber = null)
        {

        }

    }
}
