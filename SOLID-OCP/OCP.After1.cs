using System.Collections.Generic;
using System.Linq;

namespace solid.ocp.after1
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

			if (customer.StateCode == "TX")
				total += new TXTax().CalculateTax(total);

			else if (customer.StateCode == "FL")
				total += new FLTax().CalculateTax(total);

			else
				total += new NULLTax().CalculateTax(total);

			return total;
		}
	}

	public class OrderItem
	{
		public int Quantity;
		public string Code;
		public decimal Cost;
	}

	public interface ITax
	{
		decimal CalculateTax(decimal Total);
	}

	public class TXTax : ITax
	{
		public decimal CalculateTax(decimal Total)
		{
			return Total * .08m;
		}
	}

	public class FLTax : ITax
	{
		public decimal CalculateTax(decimal Total)
		{
			return Total * .09m;
		}
	}

	public class NULLTax : ITax
	{
		public decimal CalculateTax(decimal Total)
		{
			return .03m;
		}
	}
}
