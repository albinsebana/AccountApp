using AccountApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = LoadAccountDetails();
            if (account == null)
            {
                account = CreateAccount();
            }

            Console.WriteLine("Account created successfully.");

            int choice;
            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) Deposit");
                Console.WriteLine("2) Withdraw");
                Console.WriteLine("3) Display Balance");
                Console.WriteLine("4) Exit");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter amount to deposit: ");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        account.Balance += depositAmount;
                        SaveAccountDetails(account);
                        Console.WriteLine("Amount deposited successfully.");
                        break;
                    case 2:
                        Console.Write("Enter amount to withdraw: ");
                        double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                        if (withdrawAmount > account.Balance)
                        {
                            Console.WriteLine("Insufficient balance.");
                        }
                        else
                        {
                            account.Balance -= withdrawAmount;
                            SaveAccountDetails(account);
                            Console.WriteLine("Amount withdrawn successfully.");
                        }
                        break;
                    case 3:
                        Console.WriteLine($"Current balance: {account.Balance}");
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != 4);
        }

        static Account CreateAccount()
        {
            Console.Write("Enter Account Name: ");
            string accountName = Console.ReadLine();
            Console.Write("Enter Bank Name: ");
            string bankName = Console.ReadLine();
            Console.Write("Enter Opening Balance: ");
            double openingBalance = Convert.ToDouble(Console.ReadLine());

            Account account = new Account
            {
                AccountName = accountName,
                BankName = bankName,
                Balance = openingBalance
            };

            SaveAccountDetails(account);

            return account;
        }

        static void SaveAccountDetails(Account account)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("account.dat", FileMode.Create))
            {
                formatter.Serialize(fs, account);
            }
        }

        static Account LoadAccountDetails()
        {
            if (File.Exists("account.dat"))
            {
                IFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("account.dat", FileMode.Open))
                {
                    return (Account)formatter.Deserialize(fs);
                }
            }
            return null;
        }
    }
}