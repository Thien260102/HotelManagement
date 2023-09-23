using System;
using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class EmployeeDTO
	{
		#region Fields & Properties
		public int Id;

		public string CitizenId;

		public string FullName;

		public string PhoneNumber;

		public bool Sex;

		public string BirthDay;

		public string StartDay;

		public decimal Salary;
		#endregion

		#region Methods

		public EmployeeDTO()
		{

		}

		public EmployeeDTO(string citizenId, string fullname, string phone, bool sex, 
							string birthday, string start, decimal salary)
		{
			CitizenId = citizenId;
			FullName = fullname;
			PhoneNumber = phone;
			Sex = sex;
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
			Sex = (bool)row["Sex"];
			BirthDay = row["BirthDay"].ToString();
			StartDay = row["StartDay"].ToString();
			Salary = (decimal)row["Salary"];
		}
		#endregion
	}
}
