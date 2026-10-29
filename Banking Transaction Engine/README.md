# Banking Transaction Engine

A C# console application that simulates core banking operations using object-oriented programming, in-memory data storage, and LINQ.

The project focuses on implementing banking business logic, account management, transaction processing, and validation without relying on external frameworks or databases.

## Features

- **Account Management** – Create bank accounts with unique private numbers.
- **Deposits** – Add money to an account with amount validation.
- **Withdrawals** – Withdraw money while preventing insufficient balance.
- **Money Transfers** – Transfer funds between accounts with balance and account-status validation.
- **Balance Inquiry** – Check an account's current balance.
- **Account Freezing** – Freeze and unfreeze accounts to control financial operations.
- **Transaction History** – View deposits, withdrawals, and incoming/outgoing transfers in chronological order.
- **Error Handling** – Handle invalid inputs, missing accounts, and unsuccessful operations.

## Technologies

- C#
- .NET
- LINQ
- Object-Oriented Programming (OOP)
- In-Memory Collections

## Project Structure

```text
Banking-Transaction-Engine/
├── BankAccount.cs
├── BankTransaction.cs
├── BankingService.cs
├── Program.cs
└── README.md
```

## Getting Started

### Prerequisites

- .NET SDK
- Visual Studio, Visual Studio Code, or another C# IDE

### Installation

Clone the repository:

```bash
git clone https://github.com/YOUR_USERNAME/Banking-Transaction-Engine.git
```

Navigate to the project directory:

```bash
cd Banking-Transaction-Engine
```

Run the application from the directory containing the `.csproj` file:

```bash
dotnet run
```

## Console Menu

```text
===== BANKING SYSTEM =====
1. Create Account
2. Deposit
3. Withdraw
4. Transfer
5. Check Balance
6. Freeze Account
7. Transaction History
0. Exit
```

## Business Rules

- Each account must have a unique private number.
- Deposits and withdrawals must use positive amounts.
- Withdrawals cannot exceed the available balance.
- Transfers require valid sender and receiver accounts.
- Transfers between the same account are prohibited.
- Frozen accounts cannot deposit, withdraw, or transfer funds.
- Successful financial operations are recorded in transaction history.

## Transaction History

The application records the following transaction types:

| Transaction | Description |
|---|---|
| Deposit | Money added to an account |
| Withdrawal | Money withdrawn from an account |
| Transfer | Money moved between two accounts |

Each transaction includes a unique identifier, amount, transaction type, timestamp, and relevant account identifiers.

## Architecture

The application separates responsibilities into three main components:

**BankAccount**

Manages account information, balances, frozen status, and validation of individual financial operations.

**BankingService**

Handles account creation, transaction processing, transfers, account lookups, and transaction history.

**BankTransaction**

Represents a financial transaction and stores the information required for transaction tracking.

## Limitations

This project is intended for learning and demonstration purposes.

- Data is stored in memory and is lost when the application closes.
- There is no database or persistent storage.
- There is no authentication or authorization.
- Transfers are not guaranteed to be atomic under concurrent operations.
- The application is not intended for real financial transactions.

## Purpose

This project was developed to strengthen practical C# programming skills, including object-oriented design, encapsulation, collections, LINQ, validation, and financial transaction logic.

## License

This project is available for educational and portfolio purposes.
