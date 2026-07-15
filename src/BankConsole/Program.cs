using CSharpBankProject.src.BankConsole.Menus;

namespace CSharpBankProject.src.BankConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Main menu logic
            int menuChoice = 0;
            while (menuChoice != 3)
            { 
                ConsoleInterface consoleMenu = new ConsoleInterface();
                menuChoice = consoleMenu.MainMenu();
                switch (menuChoice)
                {
                    case 1:
                        Console.Clear();
                        // Handle account creation
                        break;
                    case 2:
                        Console.Clear();
                        // Handle account login
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("See you later!");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
