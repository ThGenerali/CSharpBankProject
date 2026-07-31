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
                Console.Clear();
                switch (menuChoice)
                {
                    case 1:
                        var loginService = services.LoginMenu();
                        int accountMenuChoice = loginService != default ? consoleMenu.AccountMenu(loginService.UserName, loginService.Balance) : 4;
                        Console.Clear();
                        while (accountMenuChoice != 4)
                        {
                            switch (accountMenuChoice)
                            {
                                case 1:
                                    services.Transaction();
                                    break;
                                case 2:
                                    services.ShowAccountDetailsMenu();
                                    break;
                                case 3:
                                    services.ChangePinMenu();
                                    Console.Clear();
                                    break;
                                case 4:
                                    Console.WriteLine("Logging out...");
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please try again.");
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                            }
                            accountMenuChoice = consoleMenu.AccountMenu(loginService.UserName, services.GetUpdatedBalance());
                            Console.Clear();
                        }
                        break;
                    case 2:
                        services.RegisterUser();
                        break;
                    case 3:
                        Console.WriteLine("See you later!");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            }
        }

    }
}
