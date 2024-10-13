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
                
                if (Login(out decimal[] accounts, out string userName))
                {
                    
                    ShowMainMenu(accounts, userName);
                }
                else
                {
                    Console.WriteLine("Max antal inloggningsförsök nått. Försök igen senare.");
                    break; 
                }
            }
        }



        //Fixa login [Klar]
        //Fixa moneytransfer [Klar]
        //Fixa så man återvänder till huvudmenyn efter att man loggar ut [Klar]
        //Fixa så att alla konton har olika namn [Klar]
        //Fixa så alla konton har pengar och öre [Klar]
        //Fixa så det behövs lösenord för när jag vill dra ut pengar [Klar]
        static bool Login(out decimal[] accounts, out string userName)
        {
            accounts = new decimal[5]; // Initialize accounts with five slots
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
                    return true; 
                }
                else
                {
                    failedLoginAttempts++;
                    Console.WriteLine($"Fel inloggning. Försök kvar: {maxLoginAttempts - failedLoginAttempts}");
                }
            }
            return false;
        }

        static void InitializeAccounts(string userName, decimal[] accounts)
        {
            // Konton
            switch (userName)
            {
                case "Filip Oldin":
                    accounts[0] = 30000.30m; // Lönekonto
                    accounts[1] = 20000.10m; // Sparkonto                       
                    accounts[4] = 5000.00m;  // Gamingkonto
                    break;
                case "Anna Holgersson":
                    accounts[0] = 25000.25m; // Lönekonto
                    accounts[1] = 5000.50m;   // Sparkonto
                    accounts[2] = 3000.75m;   // Semesterkonto
                    accounts[4] = 1500.00m;    //"Gamingkonto"
                    break;
                case "Tobbe Rikardsson":
                    accounts[0] = 40000.34m; // Lönekonto
                    accounts[2] = 2000.00m;    // Sparkonto
                    accounts[3] = 500.00m;     // Barnkonto
                    accounts[4] = 1200.00m;    // Gamingkonto"
                    break;
                case "Kent Käll":
                    accounts[1] = 1000.78m;   // Sparkonto
                    accounts[2] = 500.00m;     // Semesterkonto
                    accounts[4] = 3000.00m;    // Gamingkonto"
                    break;
                case "Eva Hobert":
                    accounts[0] = 18000.53m; // Lönekonto
                    accounts[1] = 15000.73m;  // Semesterkonto
                    accounts[3] = 500.00m;     // Barnkonto

                    break;
                default:
                    for (int i = 0; i < accounts.Length; i++)
                    {
                        accounts[i] = 0; 
                    }
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
                            return; 
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
            string[] accountNames = { "Lönekonto", "Sparkonto", "Semesterkonto", "Barnkonto", "Gamingkonto" };

            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] > 0) // Visar hur mycket sitter på kontona
                {
                    Console.WriteLine($"{accountNames[i]}: {accounts[i]:0.00} kr");
                }
            }
        }

        static void TransferFunds(decimal[] accounts)
        {
            Console.WriteLine("Överföring mellan konton");
            string[] accountNames = { "Lönekonto", "Sparkonto", "Semesterkonto", "Barnkonto", "Gamingkonto" };

            Console.WriteLine("Välj ett konto att överföra från:");
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] > 0) 
                {
                    Console.WriteLine($"{i + 1}. {accountNames[i]} - Saldo: {accounts[i]:0.00} kr");
                }
            }

            if (int.TryParse(Console.ReadLine(), out int fromAccount) && fromAccount >= 1 && fromAccount <= accounts.Length && accounts[fromAccount - 1] > 0)
            {
                fromAccount--; 
                Console.WriteLine("Välj ett konto att överföra till:");
                for (int i = 0; i < accounts.Length; i++)
                {
                    if (accounts[i] > 0 && i != fromAccount) 
                    {
                        Console.WriteLine($"{i + 1}. {accountNames[i]} - Saldo: {accounts[i]:0.00} kr");
                    }
                }

                if (int.TryParse(Console.ReadLine(), out int toAccount) && toAccount >= 1 && toAccount <= accounts.Length && toAccount - 1 != fromAccount)
                {
                    toAccount--;
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
            string[] accountNames = { "Lönekonto", "Sparkonto", "Semesterkonto", "Barnkonto", "Gamingkonto" };

            Console.WriteLine("Välj konto att ta ut pengar från:");
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] > 0) 
                {
                    Console.WriteLine($"{i + 1}. {accountNames[i]} - Saldo: {accounts[i]:0.00} kr");
                }
            }

            if (int.TryParse(Console.ReadLine(), out int accountChoice) && accountChoice >= 1 && accountChoice <= accounts.Length && accounts[accountChoice - 1] > 0)
            {
                accountChoice--; 
                Console.Write("Skriv ditt lösenord för att bekräfta uttaget: ");
                string password = Console.ReadLine();

                if (CheckLogin(userName, password))
                {
                    Console.Write("Skriv hur mycket du skulle vilja ta ut: ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount) && withdrawalAmount > 0)
                    {
                        if (withdrawalAmount <= accounts[accountChoice]) 
                        {
                            accounts[accountChoice] -= withdrawalAmount; 
                            Console.WriteLine($"Uttag av {withdrawalAmount} kr genomfört från {accountNames[accountChoice]}. Nytt saldo: {accounts[accountChoice]:0.00} kr");
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
            { "Filip Oldin", "123" },
            { "Anna Holgersson", "1234" },
            { "Tobbe Rikardsson", "12345" },
            { "Kent Käll", "123456" },
            { "Eva Hobert", "1234567" }
        };

            return users.TryGetValue(userName, out string correctPassword) && correctPassword == password;
        }
    }
}


