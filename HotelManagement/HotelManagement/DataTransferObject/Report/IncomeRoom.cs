using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject.Report
{
	public class IncomeRoom
	{
		#region Fields & Properties
		public string RoomName { get; set; }

		public int Floor { get; set; }

		public string TypeName { get; set; }

		public decimal Income { get; set; }
		#endregion

		#region Methods
		public IncomeRoom()
		{
			Income = 0;
		}

		public IncomeRoom(string fullname, int floor, string typeName, decimal income)
		{
			RoomName = fullname;
			Floor = floor;
			TypeName = typeName;
			Income = income;
		}

		#endregion
	}
}
