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

		public string CustomerId { get; set; }

		public string Username { get; set; }

		public int RoomTypeId { get; set; }

		public bool Sex { get; set; }
		public string SSex { get; set; }

		public string BirthDay { get; set; }

		public string StartDay { get; set; }

		public decimal Salary { get; set; }
		#endregion

		#region Methods

		public BookingDTO()
		{
			Id = -1;
		}

		public BookingDTO(string citizenId, string fullname, int roomTypeId, bool sex,
							string birthday, string start, decimal salary)
		{
			CustomerId = citizenId;
			Username = fullname;
			RoomTypeId = roomTypeId;

			SSex = "Male";
			Sex = sex;
			if (!Sex)
				SSex = "Female";

			BirthDay = birthday;
			StartDay = start;
			Salary = salary;
		}

		public BookingDTO(DataRow row)
		{
			Id = (int)row["ID"];
			CustomerId = row["CitizenID"].ToString();
			Username = row["FullName"].ToString();
			RoomTypeId = (int)row["RoomTypeID"];

			SSex = "Male";
			Sex = (bool)row["Sex"];
			if (!Sex)
				SSex = "Female";

			BirthDay = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["BirthDay"].ToString()));
			StartDay = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["StartDay"].ToString()));
			Salary = (decimal)row["Salary"];

		}
		#endregion
	}
}
