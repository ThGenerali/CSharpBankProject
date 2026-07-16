using CSharpBankProject.src.BankConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Models
{
    internal class User : UserRepository
    {
        private Guid Id { get; init; }
        public List<string> NameUsername { get; set; }
        public string Password { get; private set; }

        public User(string name, string username, string password)
        {
            this.Id = Guid.NewGuid();
            this.NameUsername = new List<string> { name, username };
            this.Password = password;
        }

        public void UserLogin(string username, string password)
        { }

        public void UserRegister(string name, string username, string password)
        { }

    }
}
