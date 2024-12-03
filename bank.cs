using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Welcome to the Banking Management System");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. Withdraw Money");
            Console.WriteLine("4. Check Balance");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    bank.CreateAccount();
                    break;
                case "2":
                    bank.Deposit();
                    break;
                case "3":
                    bank.Withdraw();
                    break;
                case "4":
                    bank.CheckBalance();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Thank you for using the Banking Management System.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }
}

class Bank
{
    private List<Account> accounts = new List<Account>();

    public void CreateAccount()
    {
        Console.Write("Enter account holder's name: ");
        string name = Console.ReadLine();
        Console.Write("Enter initial deposit amount: ");
        decimal initialDeposit;

        while (!decimal.TryParse(Console.ReadLine(), out initialDeposit) || initialDeposit < 0)
        {
            Console.Write("Invalid amount. Please enter a positive number: ");
        }

        Account newAccount = new Account(name, initialDeposit);
        accounts.Add(newAccount);
        Console.WriteLine("Account created successfully for {0}. Account Number: {1}", name, newAccount.AccountNumber);
    }

    public void Deposit()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        Account account = FindAccount(accountNumber);

        if (account != null)
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount;

            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Invalid amount. Please enter a positive number: ");
            }

            account.Deposit(amount);
            Console.WriteLine("Successfully deposited {0} to account {1}. New balance: {2}", amount, accountNumber, account.Balance);
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void Withdraw()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        Account account = FindAccount(accountNumber);

        if (account != null)
        {
            Console.Write("Enter amount to withdraw: ");
            decimal amount;

            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Invalid amount. Please enter a positive number: ");
            }

            if (account.Withdraw(amount))
            {
                Console.WriteLine("Successfully withdrew {0} from account {1}. New balance: {2}", amount, accountNumber, account.Balance);
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void CheckBalance()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        Account account = FindAccount(accountNumber);

        if (account != null)
        {
            Console.WriteLine("Account Balance for {0}: {1}", accountNumber, account.Balance);
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    private Account FindAccount(string accountNumber)
    {
        return accounts.Find(a => a.AccountNumber == accountNumber);
    }
}

class Account
{
    private static int accountCounter = 0;
    private string accountNumber;
    private string accountHolderName;
    private decimal balance;

    public string AccountNumber
    {
        get { return accountNumber; }
    }

    public string AccountHolderName
    {
        get { return accountHolderName; }
    }

    public decimal Balance
    {
        get { return balance; }
        private set { balance = value; }
    }

    public Account(string accountHolderName, decimal initialDeposit)
    {
        accountCounter++;
        this.accountNumber = "ACC" + accountCounter.ToString("D4");
        this.accountHolderName = accountHolderName;
        Balance = initialDeposit;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}
