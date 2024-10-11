namespace Individuellt_Projekt_V._3
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                // Handle login process
                if (Login(out decimal[] accounts, out string userName))
                {
                    // If login is successful, show the main menu
                    ShowMainMenu(accounts, userName);
                }
                else
                {
                    Console.WriteLine("Max antal inloggningsförsök nått. Försök igen senare.");
                    break; // Exit if login fails after maximum attempts
                }
            }
        }



        //Fixa login [Klar]
        //Fixa moneytransfer [Klar]
        //Fixa så man återvänder till huvudmenyn efter att man loggar ut [Klar]
        //Fixa så att alla konton har olika namn
        //Fixa så alla konton har pengar och öre [Klar]
        //Fixa så det behövs lösenord för när jag vill dra ut pengar [Klar]
        static bool Login(out decimal[] accounts, out string userName)
        {
            accounts = new decimal[3]; // Initialize accounts
            userName = "";
            string password;
            int failedLoginAttempts = 0;
            const int maxLoginAttempts = 3;

            while (failedLoginAttempts < maxLoginAttempts)
            {
                Console.WriteLine("Inloggningsmeny");
                Console.WriteLine("---------------------------------");
                Console.Write("Skriv in ditt användarnamn: ");
                userName = Console.ReadLine();
                Console.WriteLine("---------------------------------");
                Console.Write("Skriv in ditt lösenord: ");
                password = Console.ReadLine();

                if (CheckLogin(userName, password))
                {
                    InitializeAccounts(userName, accounts);
                    Console.Clear();
                    Console.WriteLine("Du är inloggad!");
                    return true; // Successful login
                }
                else
                {
                    failedLoginAttempts++;
                    Console.WriteLine($"Fel inloggning. Försök kvar: {maxLoginAttempts - failedLoginAttempts}");
                }
            }
            return false; // Return false if login fails
        }

        static void InitializeAccounts(string userName, decimal[] accounts)
        {
            // Initialize accounts based on user
            switch (userName)
            {
                case "Filip Oldin":
                    accounts[0] = 30000.30m; // Lönekonto
                    accounts[1] = 20000.10m; // Sparkonto
                    break;
                case "Anna Holgersson":
                    accounts[0] = 25000.25m; // Lönekonto
                    break; // Only one account for Anna
                case "Tobbe Rikardsson":
                    accounts[0] = 40000.34m; // Lönekonto
                    accounts[1] = 8000.45m;   // Semesterkonto
                    break;
                case "Kent Käll":
                    accounts[0] = 15000.65m; // Lönekonto
                    accounts[1] = 1000.78m;   // Sparkonto
                    break;
                case "Eva Hobert":
                    accounts[0] = 18000.53m; // Lönekonto
                    accounts[1] = 15000.73m;  // Semesterkonto
                    break;
                default:
                    accounts[0] = accounts[1] = accounts[2] = accounts[3] = accounts[4] = 0; // Default case, no money
                    break;
            }
        }

        static void ShowMainMenu(decimal[] accounts, string userName)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Skriv in en siffra till den del av kontot du vill till:");
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");
                Console.WriteLine("5. Stäng ner programmet");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            DisplayBalances(accounts);
                            break;
                        case 2:
                            TransferFunds(accounts);
                            break;
                        case 3:
                            WithdrawMoney(accounts, userName);
                            break;
                        case 4:
                            Console.WriteLine("Loggar ut! Välkommen åter");
                            Console.Clear();
                            return; // Exit the main menu loop to allow for new login
                        case 5:
                            Console.WriteLine("Stänger programmet...");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Ogiltig svar, försök igen");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val, försök igen.");
                }

                Console.WriteLine("\nTryck enter för att återgå till huvudmenyn");
                Console.ReadKey();
            }
        }

        static void DisplayBalances(decimal[] accounts)
        {
            Console.Clear();
            string[] accountNames = { "Lönekonto", "Sparkonto", "Semesterkonto" };

            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] > 0) // Only display accounts with a positive balance
                {
                    Console.WriteLine($"{accountNames[i]}: {accounts[i]} kr");
                }
            }
        }

        static void TransferFunds(decimal[] accounts)
        {
            Console.WriteLine("Överföring mellan konton");
            string[] accountNames = { "Lönekonto", "Sparkonto", "Semesterkonto" };

            Console.WriteLine("Välj ett konto att överföra från:");
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] > 0) // Only show accounts with a positive balance
                {
                    Console.WriteLine($"{i + 1}. {accountNames[i]} - Saldo: {accounts[i]} kr");
                }
            }

            if (int.TryParse(Console.ReadLine(), out int fromAccount) && fromAccount >= 1 && fromAccount <= accounts.Length && accounts[fromAccount - 1] > 0)
            {
                fromAccount--; // Convert to zero-based index
                Console.WriteLine("Välj ett konto att överföra till:");
                for (int i = 0; i < accounts.Length; i++)
                {
                    if (accounts[i] > 0 && i != fromAccount) // Show only different accounts with positive balance
                    {
                        Console.WriteLine($"{i + 1}. {accountNames[i]} - Saldo: {accounts[i]} kr");
                    }
                }

                if (int.TryParse(Console.ReadLine(), out int toAccount) && toAccount >= 1 && toAccount <= accounts.Length && toAccount - 1 != fromAccount)
                {
                    toAccount--; // Convert to zero-based index
                    Console.WriteLine("Ange ett belopp att överföra:");

                    if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount) && transferAmount > 0)
                    {
                        TransferMoney(fromAccount, toAccount, transferAmount, accounts);
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt belopp angivet. Ange ett positivt belopp.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt konto för överföring.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt konto för överföring.");
            }
            Console.WriteLine("\nTryck enter för att återgå till huvudmenyn");
            Console.ReadKey();
        }

        static void WithdrawMoney(decimal[] accounts, string userName)
        {
            Console.WriteLine("Ta ut pengar");
            string[] accountNames = { "Lönekonto", "Sparkonto", "Semesterkonto" };

            Console.WriteLine("Välj konto att ta ut pengar från:");
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] > 0) // Only show accounts with a positive balance
                {
                    Console.WriteLine($"{i + 1}. {accountNames[i]} - Saldo: {accounts[i]} kr");
                }
            }

            if (int.TryParse(Console.ReadLine(), out int accountChoice) && accountChoice >= 1 && accountChoice <= accounts.Length && accounts[accountChoice - 1] > 0)
            {
                accountChoice--; // Convert to zero-based index
                Console.Write("Skriv ditt lösenord för att bekräfta uttaget: ");
                string password = Console.ReadLine();

                if (CheckLogin(userName, password))
                {
                    Console.Write("Skriv hur mycket du skulle vilja ta ut: ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount) && withdrawalAmount > 0)
                    {
                        if (withdrawalAmount <= accounts[accountChoice]) // Withdraw from the selected account
                        {
                            accounts[accountChoice] -= withdrawalAmount; // Deduct from the chosen account
                            Console.WriteLine($"Uttag av {withdrawalAmount} kr genomfört från {accountNames[accountChoice]}. Nytt saldo: {accounts[accountChoice]} kr");
                        }
                        else
                        {
                            Console.WriteLine("Otillräckligt saldo för uttag.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt belopp angivet. Ange ett positivt belopp.");
                    }
                }
                else
                {
                    Console.WriteLine("Fel lösenord. Uttaget avbröts.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt konto för uttag.");
            }
            Console.WriteLine("\nTryck enter för att återgå till huvudmenyn");
            Console.ReadKey();
        }

        public static void TransferMoney(int fromAccount, int toAccount, decimal amount, decimal[] accounts)
        {
            if (accounts[fromAccount] >= amount)
            {
                accounts[fromAccount] -= amount;
                accounts[toAccount] += amount;
                Console.WriteLine($"Överföring av {amount} kr från konto {fromAccount + 1} till konto {toAccount + 1} lyckades");
            }
            else
            {
                Console.WriteLine("Otillräckligt saldo för överföring");
            }
        }

        static bool CheckLogin(string userName, string password)
        {
            var users = new Dictionary<string, string>
        {
            { "Filip Oldin", "hemlis123" },
            { "Anna Holgersson", "hemlis1234" },
            { "Tobbe Rikardsson", "hemlis12345" },
            { "Kent Käll", "hemlis123456" },
            { "Eva Hobert", "hemlis1234567" }
        };

            return users.TryGetValue(userName, out string correctPassword) && correctPassword == password;
        }
    }


}

