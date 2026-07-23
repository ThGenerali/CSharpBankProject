using CSharpBankProject.src.BankConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Models
{
    internal class User 
    {
        public Guid Id { get; init; }
        public string Name { get; }
        public string Username { get; init; }
        public string Password { get; private set; }

        public User(string name, string username, string password)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Username = username;
            this.Password = password;
        }

        UserRepository userRepository = new UserRepository();

        public void UpdatePassword(string newPassword, string currentPassword)
        {
            if (userRepository.VerifyPassword(this.Username, currentPassword))
            {
                this.Password = newPassword;
            } else {
                throw new UnauthorizedAccessException("Current password is incorrect. Password update failed.");
            }
        } 
        
        public void PrintUserInfo(string password)
        {
            if (userRepository.VerifyPassword(this.Username, password))
            {
                List<string> userInfo = userRepository.GetUserInfo(this.Id);
                foreach (var info in userInfo)
                {
                    Console.WriteLine(info);
                }
            } else {
                throw new UnauthorizedAccessException("Incorrect password. Cannot display user information.");
            }
        }

    }
}
