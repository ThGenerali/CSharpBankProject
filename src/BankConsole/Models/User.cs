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
        private UserRepository UserRepository { get; }
        public User(Guid Id, string name, string username, string password, UserRepository userRepository)
        {
            this.Id = Id;
            this.Name = name;
            this.Username = username;
            this.Password = password;
            this.UserRepository = userRepository;
        }

        

        public void UpdatePassword(string newPassword, string currentPassword)
        {
            if (UserRepository.VerifyPassword(this.Username, currentPassword))
            {
                this.Password = newPassword;
            }
        } 
    }
}
