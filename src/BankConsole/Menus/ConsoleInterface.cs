using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Menus
{
    public class ConsoleInterface
    {
        void Title()
        {
            Console.WriteLine(@"

░██████╗░░██████╗██╗░░██╗░█████╗░██████╗░██████╗░  ██████╗░░█████╗░███╗░░██╗██╗░░██╗
██╔════╝░██╔════╝██║░░██║██╔══██╗██╔══██╗██╔══██╗  ██╔══██╗██╔══██╗████╗░██║██║░██╔╝
██║░░██╗░╚█████╗░███████║███████║██████╔╝██████╔╝  ██████╦╝███████║██╔██╗██║█████═╝░
██║░░╚██╗░╚═══██╗██╔══██║██╔══██║██╔══██╗██╔═══╝░  ██╔══██╗██╔══██║██║╚████║██╔═██╗░
╚██████╔╝██████╔╝██║░░██║██║░░██║██║░░██║██║░░░░░  ██████╦╝██║░░██║██║░╚███║██║░╚██╗
░╚═════╝░╚═════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░░░░  ╚═════╝░╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝
\n");
        }

        public int MainMenu()
        {
            Title();
            Console.WriteLine(@"
Welcome to CSharp Bank!
1. Login
2. Register
3. Exit
");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }

        public int AccountMenu(string username, decimal balance)
        {
            Title();
            Console.WriteLine(@$"
Welcome {username}!
Your current balance is {balance.ToString("0.00")}
Would you like to do?
1.Transactions
2.View Account Details
3.Change PIN
4.Logout
");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }

    }
}
