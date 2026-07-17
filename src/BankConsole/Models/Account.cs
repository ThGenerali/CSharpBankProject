using CSharpBankProject.src.BankConsole.Data;
using CSharpBankProject.src.BankConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Models
{
    internal class Account : User 
    {
        public Guid AccountId { get; init; }
        public int AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public int Pin { get; private set; }

        public Account(string name, string username, string password, Guid AccountId, int AccountNumber, decimal Balance, int Pin, User user) : base(name, username, password)
        {
            Guid id = user.Id;
            this.AccountId = id;
            this.AccountNumber = AccountNumber;
            this.Balance = Balance;
            this.Pin = Pin;
        }

        // Create an instance of the AccountRepository class to access the account data "dependency injection"
        protected AccountRepository accountRepository = new AccountRepository();
        

        public void Deposit(decimal amount)
        {

        }

        public void Withdraw(decimal amount)
        {

        }

        public void Transfer(decimal amount, Account recipientAccount)
        {
        }
}
