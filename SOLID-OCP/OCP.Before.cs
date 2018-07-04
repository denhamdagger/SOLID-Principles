using System.Collections.Generic;
using System.Linq;

namespace solid.ocp.before
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

			Tax t = new Tax();

			total = total + t.CalculateTax(customer.StateCode, total);

			return total;
		}
	}

	public class OrderItem
	{
		public int Quantity;
		public string Code;
		public decimal Cost;
	}

	public class Tax
	{
		public decimal CalculateTax(string StateCode, decimal Total)
		{
			decimal tax;

			if (StateCode == "TX")
				tax = Total * .08m;

			else if (StateCode == "FL")
				tax = Total * .09m;

			else
				tax = .03m;

			return tax;
		}
	}
}
