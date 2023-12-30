using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject.Report
{
	public class MostUsedRoom
	{
		#region Fields & Properties
		public string RoomName { get; set; }

		public int Floor { get; set; }

		public string TypeName { get; set; }

		public int NumberOfUses { get; set; }
		#endregion

		#region Methods
		public MostUsedRoom()
		{
			NumberOfUses = 0;
		}

		#endregion
	}
}
