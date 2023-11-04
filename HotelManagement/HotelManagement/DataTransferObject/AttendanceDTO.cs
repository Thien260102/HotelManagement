using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataTransferObject
{
	 public class AttendanceDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public int EmployeeId { get; set; }

		public string Date { get; set; }

		public int State { get; set; }

		public string Note { get; set; }
		#endregion

		#region Methods

		public AttendanceDTO()
		{
			Id = -1;
		}

		public AttendanceDTO(int employeeId, string date, int state, string note)
		{
			EmployeeId = employeeId;
			Date = date;
			State = state;
			Note = note;
		}

		public AttendanceDTO(DataRow row)
		{
			Id = (int)row["ID"];
			EmployeeId = (int)row["EmployeeID"];
			Date = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["Date"].ToString()));
			State = (int)row["State"];
			Note = row["Note"].ToString();

		}
		#endregion
	}
}
