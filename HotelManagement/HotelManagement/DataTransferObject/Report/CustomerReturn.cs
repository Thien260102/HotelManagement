using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject.Report
{
	public class CustomerReturn
	{
		#region Fields & Properties
		public string CustomerName { get; set; }

		public string PhoneNumber { get; set; }

		public string Nationality { get; set; }

		public int NumberOfReturns { get; set; }
		#endregion

		#region Methods
		public CustomerReturn()
		{
			NumberOfReturns = 0;
		}

		#endregion
	}
}
