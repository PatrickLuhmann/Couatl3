﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Couatl3_Model
{
	public class CouatlContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Price> Prices { get; set; }
		public DbSet<Security> Securities { get; set; }
		public DbSet<LotAssignment> LotAssignments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// The path is relative to the main assembly (.exe).
			optionsBuilder.UseSqlite(@"Data Source=couatl3.db");
		}
	}

	public class Account
	{
		public int AccountId { get; set; }

		public string Institution { get; set; }
		public string Name { get; set; }
		public bool Closed { get; set; }
	}

	public class Transaction
	{
		public int TransactionId { get; set; }

		public int Type { get; set; }
		public int SecurityId { get; set; }
		public decimal Quantity { get; set; }
		public decimal Value { get; set; }
		public decimal Fee { get; set; }
		public DateTime Date { get; set; }

		public int AccountId { get; set; }
		public Account Account { get; set; }
	}

	public class Price
	{
		public int PriceId { get; set; }

		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public bool Closing { get; set; }

	}

	public class Security
	{
		public int SecurityId { get; set; }

		public string Name { get; set; }
		public string Symbol { get; set; }

	}

	public class LotAssignment
	{
		public int LotAssignmentId { get; set; }

		public Transaction BuyTransaction { get; set; }
		public Transaction SellTransaction { get; set; }
		public decimal Quantity { get; set; }
	}
}
