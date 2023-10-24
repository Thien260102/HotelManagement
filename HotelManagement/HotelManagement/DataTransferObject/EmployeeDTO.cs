using System;
using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class EmployeeDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public string CitizenId { get; set; }

		public string FullName { get; set; }

		public string PhoneNumber { get; set; }

		public bool Sex { get; set; }
		public string SSex { get; set; }

		public string BirthDay { get; set; }

		public string StartDay { get; set; }

		public decimal Salary { get; set; }
		#endregion

		#region Methods

		public EmployeeDTO()
		{
			Id = -1;
		}

		public EmployeeDTO(string citizenId, string fullname, string phone, bool sex, 
							string birthday, string start, decimal salary)
		{
			CitizenId = citizenId;
			FullName = fullname;
			PhoneNumber = phone;

			SSex = "Male";
			Sex = sex;
			if (!Sex)
				SSex = "Female";

			BirthDay = birthday;
			StartDay = start;
			Salary = salary;
		}

		public EmployeeDTO(DataRow row)
		{
			Id = (int)row["ID"];
			CitizenId = row["CitizenID"].ToString();
			FullName = row["FullName"].ToString();
			PhoneNumber = row["PhoneNumber"].ToString();

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
