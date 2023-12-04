using HotelManagement.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject
{
	public class RentingDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public int CustomerId { get; set; }
		public string CustomerName { get; set; }

		public string Username { get; set; }
		public string EmployeeName { get; set; }

		public int RoomId { get; set; }
		public string RoomTypeName { get; set; }

		public string CreateDate { get; set; }

		public string CheckinDate { get; set; }

		public int TotalDay { get; set; }

		public string CheckoutDate { get; set; }

		public decimal Total { get; set; }

		public bool IsPaid { get; set; }
		#endregion

		#region Methods

		public RentingDTO()
		{
			Id = -1;
			IsPaid = false;
		}

		public RentingDTO(int customerId, string userName, int roomId, string createDate,
							string checkinDate, int totalDay, string checkoutDate, decimal total)
		{
			CustomerId = customerId;
			Username = userName;
			RoomId = roomId;
			CreateDate = createDate;
			CheckinDate = checkinDate;
			TotalDay = totalDay;
			CheckoutDate = checkoutDate;
			Total = total;
			IsPaid = false;
		}

		public RentingDTO(DataRow row)
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

			RoomId = (int)row["RoomID"];
			RoomTypeName = new RoomTypeBLL().GetRoomTypeName(RoomId);

			CreateDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["CreateDate"].ToString()));
			CheckinDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["CheckinDate"].ToString()));
			CheckoutDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["CheckinDate"].ToString()));
			TotalDay = (int)row["TotalDay"];
			Total = (decimal)row["Total"];
		}
		#endregion
	}
}
