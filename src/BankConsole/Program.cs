using CSharpBankProject.src.BankConsole.Menus;
using CSharpBankProject.src.BankConsole.Services;
using System.Collections;

namespace CSharpBankProject.src.BankConsole
{
    internal class Program
    {
        private static Services.Services services = new Services.Services();
        private static ConsoleInterface consoleMenu = new ConsoleInterface();
        public static void Main(string[] args)
        {
            int menuChoice = 0;
            while (menuChoice != 3)
            { 
                menuChoice = consoleMenu.MainMenu();
                switch (menuChoice)
                {
                    case 1:
                        Console.Clear();
                        var loginService = services.LoginMenu();
                        int accountMenuChoice = 0;
                        while (accountMenuChoice != 4)
                        {
                            //Customer menu isn't updating balance after transaction, added a check to fix it next time.
                            accountMenuChoice = loginService != default ? consoleMenu.AccountMenu(loginService.UserName, loginService.Balance) : 4;
                            switch (accountMenuChoice)
                            {
                                case 1:
                                    Console.Clear();
                                    services.TransactionMenu();
                                    break;
                                case 2:
                                    Console.Clear();
                                    services.ShowAccountDetailsMenu();
                                    break;
                                case 3:
                                    Console.Clear();
                                    services.ChangePinMenu();
                                    break;
                                case 4:
                                    Console.Clear();
                                    Console.WriteLine("Logging out...");
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid choice. Please try again.");
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                            }
                        }
                        break;
                    case 2:
                        Console.Clear();
                        services.RegisterUser();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("See you later!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.");
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
            }
        }

    }
}
