﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace solid.dip.after
{
	public class Customer
	{
		public string Name;
		public int Id;
		public string StateCode;
		public string ZipCode;
		public string County;
	}

	public interface IInternetCustomerRead
	{
		Customer GetCustomerById(int Id);
	}

	public class InternetCustomer : IInternetCustomerRead
	{
		public Customer GetCustomerById(int Id)
		{
			// dummy data
			return new Customer { Name = "Fred", Id = Id, StateCode = "WA", County = "whocares", ZipCode = "zippy" };
		}
	}

	public class Order
	{
		public List<OrderItem> _orderItems = new List<OrderItem>();
		private readonly IInternetCustomerRead _internetCustomer;
		private readonly Logger _logger;

		public Order(IInternetCustomerRead InternetCustomer, Logger Log)
		{
			this._internetCustomer = InternetCustomer;
			this._logger = Log;
		}

		public Customer GetCustomerById(int Id)
		{
			return _internetCustomer.GetCustomerById(Id);
		}

		public decimal CalculateTotal(Customer customer)
		{
			decimal total = _orderItems.Sum((item) =>
			{
				return item.Cost * item.Quantity;
			});

			ITax tax = new TaxFactory().GetTaxObject(customer.StateCode);

			total += tax.CalculateTax(total);

			_logger.Log("Total Cost: " + total.ToString());

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

	public abstract class Logger
	{
		public void Log(string Message)
		{
			string output = string.Format("Log Message: {0}", Message);
			Console.WriteLine(output);
			WriteToDatabase(output);
		}

		public abstract void WriteToDatabase(string Message);
	}

	public class SQLServerLogger : Logger
	{
		public override void WriteToDatabase(string Message)
		{
			Console.WriteLine(string.Format("SQL Server Database Message: {0}", Message));
		}
	}

	public class OracleLogger : Logger
	{
		public override void WriteToDatabase(string Message)
		{
			Console.WriteLine(string.Format("Oracle Database Message: {0}", Message));
		}
	}
}
