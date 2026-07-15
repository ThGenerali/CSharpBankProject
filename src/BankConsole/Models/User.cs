using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Models
{
    internal class User
    {
        private Guid Id { get; init; }
        public List<string> NameUsername { get; set; }
        private string Password { get; set; }

        public User(string name, string username, string password)
        {
            this.Id = Guid.NewGuid();
            this.NameUsername = new List<string> { name, username };
            this.Password = password;
        }
    }
}
