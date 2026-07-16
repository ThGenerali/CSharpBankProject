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
        public Guid Id { get; init; }
        public List<string> NameUsername { get; set; }
        public string Password { get; private set; }

        public User(string name, string username, string password)
        {
            this.Id = Guid.NewGuid();
            this.NameUsername = new List<string> { name, username };
            this.Password = password;
        }

        public void UserLogin(string username, string password)
        {
            //Will be called from the Login service and will call the UserRepository to check if the username and password match any existing user in the database.
        }

        public void UserRegister(string name, string username, string password)
        {
            //Will be called from the Register service and will call the UserRepository to add a new user to the database.
        }

    }
}
