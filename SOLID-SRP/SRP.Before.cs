using System.Collections.Generic;
using System.Linq;

namespace solid.srp01_1.before
{

	public class Customer
	{
		public string StateCode;
		public string ZipCode;
		public string County;
	}

	public class Order
	{
		public List<OrderItem> _orderItems = new List<OrderItem>();

		public decimal CalculateTotal(Customer customer)
		{
			decimal total = _orderItems.Sum((item) =>
			{
				return item.Cost * item.Quantity;
			});

			decimal tax;
			if (customer.StateCode == "TX")
				tax = total * .08m;

			else if (customer.StateCode == "FL")
				tax = total * .09m;

			else
				tax = .03m;

			total = total + tax;
			return total;
		}
	}

	public class OrderItem
	{
		public int Quantity;
		public string Code;
		public decimal Cost;
	}
}
