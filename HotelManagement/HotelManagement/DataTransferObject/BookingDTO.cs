using HotelManagement.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject
{
	public class BookingDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public int CustomerId { get; set; }
		public string CustomerName { get; set; }

		public string Username { get; set; }
		//public string EmployeeName { get; set; }

		public int RoomTypeId { get; set; }
		public string RoomTypeName { get; set; }

		public string CreateDate { get; set; }

		public string CheckinDate { get; set; }

		public int TotalDay { get; set; }

		public decimal Total { get; set; }
		#endregion

		#region Methods

		public BookingDTO()
		{
			Id = -1;
		}

		public BookingDTO(int customerId, string userName, int roomTypeId, string createDate,
							string checkinDate, int totalDay, decimal total)
		{
			CustomerId = customerId;
			Username = userName;
			RoomTypeId = roomTypeId;
			CreateDate = createDate;
			CheckinDate = checkinDate;
			TotalDay = totalDay;
			Total = total;
		}

		public BookingDTO(DataRow row)
		{
			Id = (int)row["ID"];

			CustomerId = (int)row["CustomerID"];
			CustomerName = new CustomerBLL().GetCustomer(CustomerId).FullName;

			Username = row["UserName"].ToString();
			//if(Username != "Admin")
			//{
			//	EmployeeName = new EmployeeBLL().GetEmployee(new AccountBLL().GetAccount(Username).EmployeeID).FullName;
			//}
			//else
			//{
			//	EmployeeName = "Admin";
			//}

			RoomTypeId = (int)row["RoomTypeID"];
			RoomTypeName = new RoomTypeBLL().GetRoomTypeName(RoomTypeId);

			CreateDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["CreateDate"].ToString()));
			CheckinDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["CheckinDate"].ToString()));
			TotalDay = (int)row["TotalDay"];
			Total = (decimal)row["Total"];
		}
		#endregion
	}
}
