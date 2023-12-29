using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.BusinessLogicLayer
{
	public class AttendanceBLL
	{

		public List<AttendanceDTO> GetAll() => AttendanceDAL.Instance.GetAll();

		public List<AttendanceDTO> GetAll(int month, int year) => AttendanceDAL.Instance.GetAll(month, year);

		public List<AttendanceDTO> GetAttendancesOf(int employeeId) 
			=> AttendanceDAL.Instance.GetAll(employeeId);

		public void Checkin(int employeeId)
		{
			switch (AttendanceDAL.Instance.AddNewAttendace(
				new AttendanceDTO(employeeId, DateTime.Now.ToString("yyyy-MM-dd"),
								  (int)Rule.ATTENDANCE.WORKED, "Worked")))
			{
				case Rule.STATE.SUCCESS:
					MessageBox.Show("Checking in successfully.");
					break;

				case Rule.STATE.EXIST:
					MessageBox.Show("You have already check in.");
					break;

				case Rule.STATE.FAIL:
					MessageBox.Show("Check in fail");
					break;
			}
		}

		public bool AddNewLeaveRegister(AttendanceDTO attendance)
		{
			switch (AttendanceDAL.Instance.AddNewAttendace(attendance))
			{
				case Rule.STATE.SUCCESS:
					MessageBox.Show("Add new leave register successfully.");
					return true;

				case Rule.STATE.EXIST:
					MessageBox.Show("Date already existed.");
					break;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add leave register fail");
					break;
			}

			return false;
		}

		public bool UpdateAttendance(AttendanceDTO attendance, bool isCheck = false)
		{
			switch(AttendanceDAL.Instance.UpdateAttendance(attendance, isCheck))
			{
				case Rule.STATE.SUCCESS:
					MessageBox.Show("Update attendance successfully");
					return true;

				case Rule.STATE.EXIST:
					MessageBox.Show("Date already existed.");
					break;

				case Rule.STATE.FAIL:
					MessageBox.Show("Update attendance fail");
					break;
			}
			return false;
		}
	}
}
