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
	}
}
