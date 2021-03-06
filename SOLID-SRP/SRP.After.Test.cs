﻿using System.Collections.Generic;
using NUnit.Framework;

namespace solid.srp01_2.after.Test
{
	[TestFixture]
	public class SRPOrderAfterTest
	{
		List<OrderItem> oi;

		[SetUp]
		public void Setup()
		{
			oi = new List<OrderItem>()
			{
				new OrderItem {Quantity = 5, Cost = 1.00m, Code = "Apples"},
				new OrderItem {Quantity = 4, Cost = 0.90m, Code = "Oranges"},
			};
		}

		[Test]
		public void TestTexasCustomerOrder()
		{
			Customer c1 = new Customer { StateCode = "TX", County = "whocares", ZipCode = "zippy" };

			Order o = new Order();
			o._orderItems = oi;

			decimal cost = o.CalculateTotal(c1);

			Assert.AreEqual(9.288m, cost);	
		}

		[Test]
		public void TestFloridaCustomerOrder()
		{
			Customer c1 = new Customer { StateCode = "FL", County = "whocares", ZipCode = "zippy" };

			Order o = new Order();
			o._orderItems = oi;

			decimal cost = o.CalculateTotal(c1);

			Assert.AreEqual(9.374m, cost);
		}

		[Test]
		public void TestOtherCustomerOrder()
		{
			Customer c1 = new Customer { StateCode = "WA", County = "whocares", ZipCode = "zippy" };

			Order o = new Order();
			o._orderItems = oi;

			decimal cost = o.CalculateTotal(c1);

			Assert.AreEqual(8.63m, cost);
		}
	}
}
