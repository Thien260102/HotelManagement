using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using HotelManagement.DataTransferObject.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.BusinessLogicLayer
{
	public class CustomerBLL
	{
		#region Methods

		public bool InsertCustomer(CustomerDTO customer)
		{
			switch (CustomerDAL.Instance.AddNewCustomer(customer))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add customer infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("CitizenID or Phone number is existed.");
					return false;
			}

			return false;
		}

		public bool UpdateCustomer(CustomerDTO customer, bool isCheck = false)
		{
			switch (CustomerDAL.Instance.UpdateCustomer(customer, isCheck))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Update customer infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("Phone number is existed.");
					return false;
			}

			return false;
		}

		public int GetCustomerId(string citizenId)
		{
			return CustomerDAL.Instance.GetCustomerID(citizenId);
		}

		public CustomerDTO GetCustomer(int id) => CustomerDAL.Instance.GetCustomer(id);

		public CustomerDTO GetCustomer(string citizenId = "", string phone = "")
			=> CustomerDAL.Instance.GetCustomer(citizenId, phone);

		public bool RemoveCustomer(int id)
		{
			return CustomerDAL.Instance.RemoveCustomer(id);
		}

		public List<CustomerDTO> GetAllCustomers() => CustomerDAL.Instance.GetAll();

		public List<CustomerReturn> CalculateUsesRoom(int year)
		{
			List<CustomerReturn> reports = new List<CustomerReturn>();

			var customers = GetAllCustomers();
			var rentings = new RentingBLL().GetAll(year);

			foreach (var customer in customers)
			{
				var customerReturn = new CustomerReturn();
				customerReturn.CustomerName = customer.FullName;
				customerReturn.Nationality = customer.Nationality;
				customerReturn.PhoneNumber = customer.PhoneNumber;
				customerReturn.NumberOfReturns = rentings.Count(element => element.CustomerId == customer.Id);

				reports.Add(customerReturn);
			}

			return reports;
		}

		public List<CustomerType> CalculateTypeCustomer()
		{
			List<CustomerType> reports = new List<CustomerType>();

			var customers = GetAllCustomers();

			customers.ForEach(element =>
			{
				if (reports.FindIndex(report => report.Nationality == element.Nationality) == -1)
				{
					reports.Add(new CustomerType() { Nationality = element.Nationality });
				}

			});

			foreach(var report in reports)
			{
				report.Quantity = customers.Count(element => element.Nationality == report.Nationality);
			}

			return reports;
		}

		#endregion
	}
}
