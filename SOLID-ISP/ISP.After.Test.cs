using System.Collections.Generic;
using NUnit.Framework;
using solid.isp.after;

namespace solid.isp04_2.after.Test
{
	[TestFixture]
	public class ISPOrderAfterTest
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
		public void TestCustomerOrder()
		{
			Order o = new Order();
			Customer cust = o.GetCustomerById(5);
			o._orderItems = oi;

			decimal cost = o.CalculateTotal(cust);

			Assert.AreEqual(8.63m, cost);
		}
	}
}
