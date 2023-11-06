using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagement.PresentationLayer
{
	/// <summary>
	/// Interaction logic for Attendance.xaml
	/// </summary>
	public partial class Attendance : UserControl
	{
		Rule.ROLE role;

		int currentIndex = -1;
		List<AttendanceDTO> attendances;

		public Attendance(Rule.ROLE role = Rule.ROLE.EMPLOYEE)
		{
			InitializeComponent();

			this.role = role;
			LoadData();
			DataGridAttendance.SelectedCellsChanged += SelectAttendance;
		}

		private void LoadData()
		{
			if (role == Rule.ROLE.ADMIN)
			{
				attendances = new AttendanceBLL().GetAll();
			}
			else
			{
				attendances = new AttendanceBLL().GetAttendancesOf(AccountBLL.Account.EmployeeID);
			}

			DataGridAttendance.ItemsSource = attendances;
		}

		private void SelectAttendance(object sender, SelectedCellsChangedEventArgs e)
		{
			currentIndex = DataGridAttendance.SelectedIndex;
		}

		private void btn_Checkin_Click(object sender, RoutedEventArgs e)
		{
			if (Utilities.GetRole() == Rule.ROLE.ADMIN)
			{
				MessageBox.Show("Admin does not need check in.");
				return;
			}

			new AttendanceBLL().Checkin(AccountBLL.Account.EmployeeID);
			LoadData();
		}

		private void btn_LeaveRegister_Click(object sender, RoutedEventArgs e)
		{
			if (Utilities.GetRole() == Rule.ROLE.ADMIN)
			{
				MessageBox.Show("Admin does not need leave register.");
				return;
			}

			AttendanceInfor attendanceInfor = new AttendanceInfor();
			attendanceInfor.Show();
			attendanceInfor.ReloadAttendance += LoadData;
		}

		private void btn_Update_Click(object sender, RoutedEventArgs e)
		{
			if (currentIndex == -1)
			{
				MessageBox.Show("Please choose attendance you want to change.");
				return;
			}

			if (attendances[currentIndex].State == (int)Rule.ATTENDANCE.WORKED)
			{
				MessageBox.Show("Cannot change work day.");
				return;
			}

			AttendanceInfor attendanceInfor = new AttendanceInfor();
			attendanceInfor.SetData(attendances[currentIndex]);
			attendanceInfor.Show();
			attendanceInfor.ReloadAttendance += LoadData;
		}
	}
}
