using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagement.PresentationLayer
{
	/// <summary>
	/// Interaction logic for Attendance.xaml
	/// </summary>
	public partial class Attendance : UserControl
	{
		#region Fields And Properties
		Rule.ROLE _role;

		int currentIndex = -1;
		List<AttendanceDTO> _attendances;
        List<string> _searchTypes;
        int _currentSearchType = 0;
		#endregion

		public Attendance(Rule.ROLE role = Rule.ROLE.EMPLOYEE)
		{
			InitializeComponent();

			this._role = role;
			LoadData();
			DataGridAttendance.SelectedCellsChanged += SelectAttendance;

			Combobox_TypeSearch.SelectionChanged += SelectTypeSearch;
			Combobox_TypeSearch.SelectedIndex = _currentSearchType;

			txt_Search.KeyDown += Finding;
		}

		private void LoadData()
		{
			if (_role == Rule.ROLE.ADMIN)
			{
				_attendances = new AttendanceBLL().GetAll();
			}
			else
			{
				_attendances = new AttendanceBLL().GetAttendancesOf(AccountBLL.Account.EmployeeID);
			}

			DataGridAttendance.ItemsSource = _attendances;

            _searchTypes = new List<string>()
            {
                "Name",
                "Date"
            };
            foreach (var file in _searchTypes)
            {
                Combobox_TypeSearch.Items.Add(file);
            }
        }

		#region Events
		private void SelectTypeSearch(object sender, SelectionChangedEventArgs e)
		{
			_currentSearchType = Combobox_TypeSearch.SelectedIndex;
		}

		private void Finding(object sender, KeyEventArgs e)
		{
			TextBox text = sender as TextBox;
			if (e.Key == Key.Return)
			{
				var filter = new List<AttendanceDTO>();

				switch (_currentSearchType)
				{
					case 0: // Name
						filter = (from attendance in _attendances
								  where attendance.EmployeeName.ToLower().Contains(text.Text.ToLower())
								  select attendance).ToList();
						break;

					case 1: // date
						filter = (from attendance in _attendances
								  where attendance.Date.ToLower().Contains(text.Text)
								  select attendance).ToList();
						break;
				}
				DataGridAttendance.ItemsSource = filter;
			}
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

			if (_attendances[currentIndex].State == (int)Rule.ATTENDANCE.WORKED)
			{
                new MessageBoxCustom("Cannot change work day", MessageType.Info, MessageButtons.Ok).ShowDialog();
				return;
			}

			AttendanceInfor attendanceInfor = new AttendanceInfor();
			attendanceInfor.SetData(_attendances[currentIndex]);
			attendanceInfor.Show();
			attendanceInfor.ReloadAttendance += LoadData;
		}
		#endregion
	}
}
