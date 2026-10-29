using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Banking_Transaction_Engine
{
    public class BankAccount
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string OwnerName { get; set; } = string.Empty;

        [MaxLength(11, ErrorMessage = "Private Number must be exactly 11")]
        [MinLength(11, ErrorMessage = "Private Number must be exactly 11")]
        [Required]
        public string PrivateNumber { get; set; }

        public decimal Balance { get; private set; } = 0;
        public bool IsFrozen { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<BankTransaction> BankTransactions { get; set; } = new();


        public void Deposit(decimal amount)
        {
            if (!CanDeposit(amount))
                throw new InvalidOperationException("Deposit not allowed");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (!CanWithdraw(amount))
                throw new InvalidOperationException("Withdrawal not allowed");



            Balance -= amount;
        }

        public bool CanDeposit(decimal amount)
        {
            return !IsFrozen && amount > 0;
        }

        public bool CanWithdraw(decimal amount)
        {
            return !IsFrozen && amount > 0 && Balance >= amount;
        }

        public void Freeze()
        {
            if (!IsFrozen)
            {
                IsFrozen = true;
            }
        }

        public void UnFreeze()
        {
            if (IsFrozen)
            {
                IsFrozen = false;
            }
        }
    }
}
