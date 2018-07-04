using System.Collections.Generic;
using NUnit.Framework;
using solid.dip.after;

namespace solid.dip05_2.after.Test
{
	[TestFixture]
	public class DIPOrderAfterTest
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
		public void TestCustomerOrderWithSQLServerLogger()
		{
			IInternetCustomerRead _internetCustomer = new InternetCustomer();
			Logger _logger = new SQLServerLogger();

			Order o = new Order(_internetCustomer, _logger);
			Customer cust = o.GetCustomerById(5);
			o._orderItems = oi;

			decimal cost = o.CalculateTotal(cust);

			Assert.AreEqual(8.63m, cost);
		}

		[Test]
		public void TestCustomerOrderWithOracleLogger()
		{
			IInternetCustomerRead _internetCustomer = new InternetCustomer();
			Logger _logger = new OracleLogger();

			Order o = new Order(_internetCustomer, _logger);
			Customer cust = o.GetCustomerById(5);
			o._orderItems = oi;

			decimal cost = o.CalculateTotal(cust);

			Assert.AreEqual(8.63m, cost);
		}
	}
}
