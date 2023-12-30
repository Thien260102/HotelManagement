using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject.Report
{
	public class CustomerType
	{
		#region Fields & Properties
		public string Nationality { get; set; }

		public int Quantity { get; set; }
		#endregion

		#region Methods
		public CustomerType()
		{
			Quantity = 0;
		}
		#endregion
	}
}
