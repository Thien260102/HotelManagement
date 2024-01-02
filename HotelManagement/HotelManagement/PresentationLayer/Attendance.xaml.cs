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
        List<string> _searchTypes;
        int _currentSearchType = 0;
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

            _searchTypes = new List<string>()
            {
                "Name",
                "Date"
            };
            foreach (var file in _searchTypes)
            {
                Combobox_TypeSearch.Items.Add(file);
            }
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;
            //Combobox_TypeSearch.SelectionChanged += SelectSearch;
        }

        private void SelectAttendance(object sender, SelectedCellsChangedEventArgs e)
		{
			currentIndex = DataGridAttendance.SelectedIndex;
		}

		private void btn_Checkin_Click(object sender, RoutedEventArgs e)
		{
			if (Utilities.GetRole() == Rule.ROLE.ADMIN)
			{
                new MessageBoxCustom("Admin does not need check in", MessageType.Info, MessageButtons.Ok).ShowDialog();
				return;
			}

			new AttendanceBLL().Checkin(AccountBLL.Account.EmployeeID);
			LoadData();
		}

		private void btn_LeaveRegister_Click(object sender, RoutedEventArgs e)
		{
			if (Utilities.GetRole() == Rule.ROLE.ADMIN)
			{
                new MessageBoxCustom("Admin does not need leave register", MessageType.Info, MessageButtons.Ok).ShowDialog();
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
                new MessageBoxCustom("Please choose attendance you want to change", MessageType.Info, MessageButtons.Ok).ShowDialog();
				return;
			}

			if (attendances[currentIndex].State == (int)Rule.ATTENDANCE.WORKED)
			{
                new MessageBoxCustom("Cannot change work day", MessageType.Info, MessageButtons.Ok).ShowDialog();
				return;
			}

			AttendanceInfor attendanceInfor = new AttendanceInfor();
			attendanceInfor.SetData(attendances[currentIndex]);
			attendanceInfor.Show();
			attendanceInfor.ReloadAttendance += LoadData;
		}
	}
}
