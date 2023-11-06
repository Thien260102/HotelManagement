using System;
using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class CustomerDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public string CitizenId { get; set; }

		public string FullName { get; set; }

		public string PhoneNumber { get; set; }

		public bool Sex { get; set; }
		public string SSex { get; set; }

		public string BirthDay { get; set; }

		public string Nationality { get; set; }
		#endregion

		#region Methods

		public CustomerDTO()
		{
			Id = -1;
		}

		public CustomerDTO(string citizenId, string fullname, string phone, bool sex,
							string birthday, string nationality)
		{
			CitizenId = citizenId;
			FullName = fullname;
			PhoneNumber = phone;

			SSex = "Male";
			Sex = sex;
			if (!Sex)
				SSex = "Female";

			BirthDay = birthday;
			Nationality = nationality;
		}

		public CustomerDTO(DataRow row)
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
			Nationality = row["Nationality"].ToString();

		}
		#endregion
	}
}
