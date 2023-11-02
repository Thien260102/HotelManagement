using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
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

		public int GetEmployeeId(string citizenId)
		{
			return CustomerDAL.Instance.GetCustomerID(citizenId);
		}

		public CustomerDTO GetCustomer(int id) => CustomerDAL.Instance.GetCustomer(id);

		public bool RemoveCustomer(int id)
		{
			return CustomerDAL.Instance.RemoveCustomer(id);
		}

		public List<CustomerDTO> GetAllCustomers() => CustomerDAL.Instance.GetAll();

		#endregion
	}
}
