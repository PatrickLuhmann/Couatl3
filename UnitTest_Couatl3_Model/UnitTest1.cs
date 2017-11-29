﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Couatl3_Model;
using System.Diagnostics;

namespace UnitTest_Couatl3_Model
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void CreateAccount()
		{
			using (var db = new CouatlContext())
			{
				// I don't know if I need to do this.
				// TODO: If this is necessary, put it in the "run before each test" method.
				db.Database.Migrate();

				int numAccountsBefore = db.Accounts.Count();

				Account newAcct = new Account
				{
					Institution = "Bank Of Tenochtitlan",
					Name = "Standard Brokerage Account",
					Closed = false
				};
				db.Accounts.Add(newAcct);

				int numRecordsChanged = db.SaveChanges();
				int numAccountsAfter = db.Accounts.Count();

				Assert.AreEqual(1, numRecordsChanged);
				Assert.AreEqual(numAccountsAfter, numAccountsBefore + 1);
				Assert.AreNotEqual(0, newAcct.AccountId);
			}
		}

		[TestMethod]
		public void AddTransaction()
		{
			using (var db = new CouatlContext())
			{
				db.Database.Migrate();
				
				Account newAcct = new Account
				{
					Institution = "Bank Of Tenochtitlan",
					Name = "Standard Brokerage Account",
					Closed = false
				};

				Transaction newXact = new Transaction
				{
					Type = 1,
					SecurityId = 100,
					Quantity = 12.34M,
					Value = 56.78M,
					Fee = 9.01M,
					Date = DateTime.Now,
					Account = newAcct
				};
				db.Transactions.Add(newXact);
				var count = db.SaveChanges();
				Debug.WriteLine("{0} records saved to database", count);
				Debug.WriteLine("ID of new account: {0}", newAcct.AccountId);
				Debug.WriteLine("ID of new transaction: {0}", newXact.TransactionId);

				Assert.AreEqual(2, count);
				Assert.AreEqual(newAcct.AccountId, newXact.AccountId);

				// Add another transaction with the already-existing account.
				newXact = new Transaction
				{
					Type = 2,
					SecurityId = 200,
					Quantity = 12.34M,
					Value = 56.78M,
					Fee = 9.01M,
					Date = DateTime.Now,
					Account = newAcct
				};
				db.Transactions.Add(newXact);
				count = db.SaveChanges();
				Debug.WriteLine("{0} records saved to database", count);
				Debug.WriteLine("ID of new account: {0}", newAcct.AccountId);
				Debug.WriteLine("ID of new transaction: {0}", newXact.TransactionId);

				Assert.AreEqual(1, count);
				Assert.AreEqual(newAcct.AccountId, newXact.AccountId);
			}
		}

		[TestMethod]
		public void AddLotAssignment()
		{
			using (var db = new CouatlContext())
			{
				db.Database.Migrate();

				Account newAcct = new Account
				{
					Institution = "Bank Of Tenochtitlan",
					Name = "Standard Brokerage Account",
					Closed = false
				};

				Transaction newXact1 = new Transaction
				{
					Type = 1,
					SecurityId = 100,
					Quantity = 12.34M,
					Value = 56.78M,
					Fee = 9.01M,
					Date = DateTime.Now,
					Account = newAcct
				};
				db.Transactions.Add(newXact1);

				Transaction newXact2 = new Transaction
				{
					Type = 2,
					SecurityId = 200,
					Quantity = 12.34M,
					Value = 56.78M,
					Fee = 9.01M,
					Date = DateTime.Now,
					Account = newAcct
				};
				db.Transactions.Add(newXact2);

				LotAssignment newLot = new LotAssignment
				{
					Quantity = 1234.5M,
					BuyTransaction = newXact1,
					SellTransaction = newXact2
				};
				db.LotAssignments.Add(newLot);
				var count = db.SaveChanges();
				Debug.WriteLine("{0} records saved to database", count);
				Debug.WriteLine("ID of new buy transaction: {0}", newXact1.TransactionId);
				Debug.WriteLine("ID of new sell transaction: {0}", newXact2.TransactionId);
				Debug.WriteLine("ID of new lot assignment: {0}", newLot.LotAssignmentId);

				Assert.AreEqual(4, count);
				Assert.AreEqual(newAcct.AccountId, newXact1.AccountId);
				Assert.AreEqual(newAcct.AccountId, newXact2.AccountId);
				Assert.AreEqual(newXact1, newLot.BuyTransaction);
				Assert.AreEqual(newXact2, newLot.SellTransaction);
			}
		}
	}
}
