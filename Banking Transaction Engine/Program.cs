using Banking_Transaction_Engine;

var bankingService = new BankingService();

while (true)
{
    Console.WriteLine("\n===== BANKING SYSTEM =====");
    Console.WriteLine("1. Create Account");
    Console.WriteLine("2. Deposit");
    Console.WriteLine("3. Withdraw");
    Console.WriteLine("4. Transfer");
    Console.WriteLine("5. Check Balance");
    Console.WriteLine("6. Freeze Account");
    Console.WriteLine("7. Transaction History");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            {
                Console.Write("Enter Your Name: ");
                var ownerName = Console.ReadLine();

                Console.Write("Enter Your Private Number (11): ");
                var privateNumber = Console.ReadLine();

                var result = bankingService.CreateAccount(ownerName, privateNumber);

                Console.WriteLine(result.message);
                break;
            }

        case "2":
            {
                Console.Write("Enter Your Private Number (11): ");
                var privateNumber = Console.ReadLine();

                var account = bankingService.GetAccount(privateNumber);

                if (account.Item1 == null)
                {
                    Console.WriteLine("Account not found.");
                    break;
                }

                Console.WriteLine($"Owner: {account.Item1.OwnerName}");
                Console.WriteLine($"Balance: {account.Item1.Balance}");

                Console.Write("Amount to deposit: ");

                if (!decimal.TryParse(Console.ReadLine(), out var amount))
                {
                    Console.WriteLine("Invalid amount.");
                    break;
                }

                var result = bankingService.Deposit(privateNumber, amount);

                Console.WriteLine(result.message);

                if (result.Item1 != null)
                    Console.WriteLine($"Balance: {result.Item1.Balance}");

                break;
            }

        case "3":
            {
                Console.Write("Enter Your Private Number (11): ");
                var privateNumber = Console.ReadLine();

                var account = bankingService.GetAccount(privateNumber);

                if (account.Item1 == null)
                {
                    Console.WriteLine("Account not found.");
                    break;
                }

                Console.WriteLine($"Owner: {account.Item1.OwnerName}");
                Console.WriteLine($"Balance: {account.Item1.Balance}");

                Console.Write("Amount to withdraw: ");

                if (!decimal.TryParse(Console.ReadLine(), out var amount))
                {
                    Console.WriteLine("Invalid amount.");
                    break;
                }

                var result = bankingService.Withdraw(privateNumber, amount);

                Console.WriteLine(result.message);

                if (result.Item1 != null)
                    Console.WriteLine($"Balance: {result.Item1.Balance}");

                break;
            }

        case "4":
            {
                Console.Write("Enter Your Private Number (11): ");
                var privateNumber = Console.ReadLine();

                Console.Write("Enter Receiver's Private Number (11): ");
                var transferToPrivateNumber = Console.ReadLine();

                Console.Write("Amount to transfer: ");

                if (!decimal.TryParse(Console.ReadLine(), out var amount))
                {
                    Console.WriteLine("Invalid amount.");
                    break;
                }

                var result = bankingService.Transfer(
                    privateNumber,
                    transferToPrivateNumber,
                    amount
                );

                Console.WriteLine(result.message);

                if (!result.isSuccess || result.Item1 == null)
                    break;

                var sender = result.Item1[0];
                var receiver = result.Item1[1];

                Console.WriteLine("\n===== TRANSFER DETAILS =====");
                Console.WriteLine($"Sender: {sender.OwnerName}");
                Console.WriteLine($"Receiver: {receiver.OwnerName}");
                Console.WriteLine($"Amount Transferred: {amount}");
                Console.WriteLine($"Your New Balance: {sender.Balance}");

                break;
            }

        case "5":
            {
                Console.Write("Enter Your Private Number (11): ");
                var privateNumber = Console.ReadLine();

                var result = bankingService.GetAccount(privateNumber);

                if (!result.isSuccess || result.Item1 == null)
                {
                    Console.WriteLine(result.message);
                    break;
                }

                Console.WriteLine("\n===== ACCOUNT BALANCE =====");
                Console.WriteLine($"Owner: {result.Item1.OwnerName}");
                Console.WriteLine($"Private Number: {result.Item1.PrivateNumber}");
                Console.WriteLine($"Balance: {result.Item1.Balance} GEL");

                break;
            }

        case "6":
            {
                Console.Write("Enter Your Private Number (11): ");
                var privateNumber = Console.ReadLine();

                Console.WriteLine("1. Freeze Account");
                Console.WriteLine("2. Unfreeze Account");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                if (option != "1" && option != "2")
                {
                    Console.WriteLine("Invalid option.");
                    break;
                }

                var result = bankingService.ChangeFreezeStatus(
                    privateNumber,
                    option == "1"
                );

                Console.WriteLine(result.message);
                break;
            }

        case "7":
            {
                Console.Write("Enter Your Private Number (11): ");
                var privateNumber = Console.ReadLine();

                var result = bankingService.GetTransactionHistory(privateNumber);

                if (!result.isSuccess || result.transactions == null)
                {
                    Console.WriteLine(result.message);
                    break;
                }

                if (result.transactions.Count == 0)
                {
                    Console.WriteLine("No transactions found.");
                    break;
                }

                Console.WriteLine("\n===== TRANSACTION HISTORY =====");

                foreach (var transaction in result.transactions)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine($"Transaction ID: {transaction.Id}");
                    Console.WriteLine($"Type: {transaction.Type}");
                    Console.WriteLine($"Amount: {transaction.Amount} GEL");
                    Console.WriteLine($"Date: {transaction.CreatedAt:dd/MM/yyyy HH:mm:ss} UTC");
                }

                break;
            }

        case "0":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}