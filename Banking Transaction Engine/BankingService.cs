using System;
using System.Collections.Generic;
using System.Text;

namespace Banking_Transaction_Engine
{
    public class BankingService
    {
        private readonly List<BankAccount> accounts = new List<BankAccount>();

        public (string message, bool isSuccess) CreateAccount(string ownerName, string privateNumber)
        {

            if (string.IsNullOrWhiteSpace(ownerName) || string.IsNullOrWhiteSpace(privateNumber))
            {
                return ($"ownerName and privateNumber is required", false);
            }

            var newAccount = new BankAccount()
            {
                OwnerName = ownerName,
                PrivateNumber = privateNumber,
            };

            if (accounts.Any(a => a.PrivateNumber == privateNumber))
            {
                return ($"Account with {privateNumber} already exists", false);
            }

            accounts.Add(newAccount);

            return
                (
                message: "Account created",
                isSuccess: true
                );
        }

        public (BankAccount?, string message, bool isSuccess) Deposit(string privateNumber, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(privateNumber))
            {
                return (null, $"privateNumber is required", false);
            }

            if (amount <= 0)
            {
                return (null, $"Amount can't be negative or zero", false);
            }

            var result = GetAccount(privateNumber);

            if (!result.isSuccess || result.Item1 == null)
            {
                return
                (
                null,
                message: "Money deposit failed",
                isSuccess: false
                );
            }

            // item1 is "BankAccount?"
            result.Item1.Deposit(amount);

            result.Item1.BankTransactions.Add(new BankTransaction()
            {
             FromAccountId = result.Item1.Id,
             Amount = amount,
             Type = TransactionType.Deposit
            });

            return
                (
                result.Item1,
                message: "Money deposit successful",
                isSuccess: true
                );
        }

        public (BankAccount?, string message, bool isSuccess) Withdraw(string privateNumber, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(privateNumber))
            {
                return (null, $"privateNumber is required", false);
            }

            if (amount <= 0)
            {
                return (null, $"Amount can't be negative or zero", false);
            }

            var result = GetAccount(privateNumber);

            if (!result.isSuccess || result.Item1 == null)
            {
                return
                (
                null,
                message: "Money withdraw failed",
                isSuccess: false
                );
            }

            // item1 is "BankAccount?"
            result.Item1.Withdraw(amount);

            result.Item1.BankTransactions.Add(new BankTransaction()
            {
                FromAccountId = result.Item1.Id,
                Amount = amount,
                Type = TransactionType.Withdrawal
            });

            return
                (
                result.Item1,
                message: "Money withdraw successful",
                isSuccess: true
                );
        }

        public (List<BankAccount>? accounts, string message, bool isSuccess) Transfer(
    string privateNumber,
    string transferToPrivateNumber,
    decimal amount)
        {
            if (string.IsNullOrWhiteSpace(privateNumber) ||
                string.IsNullOrWhiteSpace(transferToPrivateNumber))
            {
                return (null, "Both private numbers are required", false);
            }

            if (privateNumber == transferToPrivateNumber)
            {
                return (null, "Cannot transfer money to the same account", false);
            }

            if (amount <= 0)
            {
                return (null, "Amount can't be negative or zero", false);
            }

            var sender = GetAccount(privateNumber);
            var receiver = GetAccount(transferToPrivateNumber);

            if (!sender.isSuccess || sender.Item1 == null ||
                !receiver.isSuccess || receiver.Item1 == null)
            {
                return (null, "Money transfer failed", false);
            }

            if (!sender.Item1.CanWithdraw(amount) || !receiver.Item1.CanDeposit(amount))
            {
                return (null, "Transfer failed", false);
            }

            sender.Item1.Withdraw(amount);
            receiver.Item1.Deposit(amount);

            sender.Item1.BankTransactions.Add(new BankTransaction()
            {
                FromAccountId = sender.Item1.Id,
                ToAccountId = receiver.Item1.Id,
                Amount = amount,
                Type = TransactionType.Transfer
            });

            receiver.Item1.BankTransactions.Add(new BankTransaction()
            {
                FromAccountId = sender.Item1.Id,
                Amount = amount,
                Type = TransactionType.Transfer
            });

            return (
                new List<BankAccount>
                {
            sender.Item1,
            receiver.Item1
                },
                "Money transfer successful",
                true
            );
        }

        public (string message, bool isSuccess) ChangeFreezeStatus(string privateNumber, bool freeze)
        {
            if (string.IsNullOrWhiteSpace(privateNumber))
            {
                return ("Private number is required", false);
            }

            var account = accounts.FirstOrDefault(a => a.PrivateNumber == privateNumber);

            if (account == null)
            {
                return ("Account not found", false);
            }

            if (account.IsFrozen && freeze)
            {
                return ("Account already frozen", true);
            }

            if (!account.IsFrozen && !freeze)
            {
                return ("Account already unfrozen", true);
            }

            if (freeze)
            {
                account.Freeze();
                return ("Account frozen successfully", true);
            }

            account.UnFreeze();
            return ("Account unfrozen successfully", true);
        }

        public (List<BankTransaction>? transactions, string message, bool isSuccess) GetTransactionHistory(string privateNumber)
        {
            var result = GetAccount(privateNumber);

            if (!result.isSuccess || result.Item1 == null)
            {
                return (null, result.message, false);
            }

            var transactions = result.Item1.BankTransactions
                .OrderByDescending(t => t.CreatedAt)
                .ToList();

            return (transactions, "Transaction history retrieved successfully", true);
        }
        public (BankAccount?, string message, bool isSuccess) GetAccount(string privateNumber)
        {
            if (string.IsNullOrWhiteSpace(privateNumber))
            {
                return (null, $"privateNumber is required", false);
            }

            BankAccount account = accounts.FirstOrDefault(a => a.PrivateNumber == privateNumber && !a.IsFrozen);

            if (account is null)
            {
                return (null, "Account not found", false) ;
            }

            return (account, "Account found", true);
        }
    }
}
