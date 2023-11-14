using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataAccessLayer;
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
		public string EmployeeName { get; set; }

		public string Date { get; set; }

		public int State { get; set; }
		public string SState { get; set; }

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
			EmployeeName = new EmployeeBLL().GetEmployeeName(employeeId);

			Date = date;
			State = state;
			SState = ((Rule.ATTENDANCE)state).ToString();
			Note = note;
		}

		public AttendanceDTO(DataRow row)
		{
			Id = (int)row["ID"];
			EmployeeId = (int)row["EmployeeID"];
			EmployeeName = new EmployeeBLL().GetEmployeeName(EmployeeId);
			Date = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["Date"].ToString()));
			State = (int)row["State"];
			SState = ((Rule.ATTENDANCE)State).ToString();
			Note = row["Note"].ToString();

		}
		#endregion
	}
}
