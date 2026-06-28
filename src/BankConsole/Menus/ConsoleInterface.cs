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

        int MainMenu()
        {
            Title();
            Console.WriteLine(@"
Welcome to CSharp Bank!
1. Login
2. Register
3. Exit
");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

        List<string> LoginMenu()
        {
            Title();
            Console.WriteLine("Please enter your login credentials");
            string login = Console.ReadLine();
            Console.WriteLine("\nPlease enter your password");
            string password = Console.ReadLine();
            List<string> credentials = new List<string> { login, password };
            return credentials; 

        }

        List<string> RegisterMenu()
        {
            Title();
            Console.WriteLine("Please enter your registration details");
            string username = Console.ReadLine();
            Console.WriteLine("\nPlease enter your password");
            string password = Console.ReadLine();
            List<string> registrationDetails = new List<string> { username, password };
            return registrationDetails;
        }

        int AccountMenu(string username, decimal balance)
        {
            Title();
            Console.WriteLine(@$"
Welcome {username}!
Your current balance is {balance:C}
Would you like to do?
1.Deposit
2.Withdraw
3.Transfer
");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

    }
}
