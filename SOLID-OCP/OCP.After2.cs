﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace solid.ocp.after2
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

			ITax tax = new TaxFactory().GetTaxObject(customer.StateCode);

			total += tax.CalculateTax(total);

			return total;
		}
	}

	public class OrderItem
	{
		public int Quantity;
		public string Code;
		public decimal Cost;
	}

	public interface ITaxFactory
	{
		ITax GetTaxObject(string StateCode);
	}

	public class TaxFactory : ITaxFactory
	{
		public ITax GetTaxObject(string StateCode)
		{
			Assembly currentAssembly = Assembly.GetExecutingAssembly();
			var TaxFactoryType = typeof(TaxFactory);
			var currentType = currentAssembly.GetTypes().SingleOrDefault(t => t.FullName == (TaxFactoryType.Namespace + "." + StateCode + "Tax"));

			if (currentType != null)
				return (ITax)Activator.CreateInstance(currentType);

			return new NULLTax();
		}
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
