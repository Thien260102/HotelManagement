using HotelManagement.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
	public class Utilities
	{
		public static Rule.ROLE GetRole()
		{
			return (Rule.ROLE)AccountBLL.Account.RoleID;
		}

		public static bool Validate_Password(string pass)
		{
			if (pass.Length < 6)
				return false;

			return true;
		}

		public static bool Validate_CitizenId(string citizenId)
		{
			if (citizenId.Length != 12)
				return false;

			foreach (var c in citizenId)
				if (c < '0' || c > '9')
					return false;

			return true;
		}

		public static bool Validate_Phone(string phone)
		{
			if (phone.Length < 10 || phone.Length > 11)
				return false;

			foreach (var c in phone)
				if (c < '0' || c > '9')
					return false;

			return true;
		}

		public static bool Validate_DateTime(string datetime)
		{
			DateTime birthDay;
			return DateTime.TryParse(datetime, out birthDay);
		}

		public static DateTime GetDefaultCheckinTime(DateTime checkin)
			=> new DateTime(checkin.Year, checkin.Month, checkin.Day, 14, 0, 0);

		public static DateTime GetDefaultCheckoutTime(DateTime checkout)
			=> new DateTime(checkout.Year, checkout.Month, checkout.Day, 12, 0, 0);

		public static int GetDayInMonth(int month, int year = 2023)
			=> DateTime.DaysInMonth(year, month);
			

	}
}
