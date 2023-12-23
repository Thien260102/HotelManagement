using HotelManagement.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject
{
	public class BillDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public string BillDate { get; set; }

		public int CustomerId { get; set; }
		public string CustomerName { get; set; }

		public string Username { get; set; }
		public string EmployeeName { get; set; }

		public int RentingId { get; set; }
		
		public int TotalDay { get; set; }

		public decimal Total { get; set; }
		#endregion

		#region Methods

		public BillDTO()
		{
			Id = -1;
		}

		public BillDTO(string billDate, string userName, int customerId, int rentingId, int totalDay,
					   decimal total)
		{
			BillDate = billDate;
			CustomerId = customerId;
			Username = userName;
			RentingId = rentingId;
			TotalDay = totalDay;
			Total = total;
		}

		public BillDTO(DataRow row)
		{
			Id = (int)row["ID"];

			CustomerId = (int)row["CustomerID"];
			CustomerName = new CustomerBLL().GetCustomer(CustomerId).FullName;

			Username = row["UserName"].ToString();
			if (Username != "Admin")
			{
				AccountDTO account = new AccountBLL().GetAccount(Username);
				if (account.UserName == "")
				{
					EmployeeName = "Null";
				}
				else
				{
					EmployeeName = new EmployeeBLL().GetEmployee(account.EmployeeID).FullName;
				}
			}
			else
			{
				EmployeeName = "Admin";
			}

			RentingId = (int)row["RentingID"];
			BillDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["BillDate"].ToString()));
			TotalDay = (int)row["TotalDay"];
			Total = (decimal)row["Total"];
		}
		#endregion
	}
}
