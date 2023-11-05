using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagement.PresentationLayer
{
	/// <summary>
	/// Interaction logic for AttendanceInfor.xaml
	/// </summary>
	public partial class AttendanceInfor : Window
	{
		AttendanceDTO attendace;
		string originDate = "";

		public Action ReloadAttendance;

		public AttendanceInfor()
		{
			InitializeComponent();

			attendace = new AttendanceDTO();
		}

		public void SetData(AttendanceDTO attendace)
		{
			this.attendace = attendace;
			txt_Date.Text = attendace.Date;
			txt_Note.Text = attendace.Note;
			originDate = attendace.Date;
		}

		private void btn_Save_Click(object sender, RoutedEventArgs e)
		{
            try
            {
                string date = txt_Date.Text.Trim();
                string note = txt_Note.Text.Trim();
                Rule.ATTENDANCE state = Rule.ATTENDANCE.EXCUSED;

                if (string.IsNullOrEmpty(note))
                {
                    throw new Exception("Please fill all information");
                }

                if (!Utilities.Validate_DateTime(date)
                 || DateTime.Parse(date) <= DateTime.Now)
                {
                    throw new Exception("Please fill date correctly");
                }

				bool isCheck = false;
				if (originDate != date)
				{
					isCheck = true;
				}

				attendace.EmployeeId = AccountBLL.Account.EmployeeID;
				attendace.Date = date;
				attendace.Note = note;
				attendace.State = (int)state;

				AttendanceBLL attendanceBLL = new AttendanceBLL();
                if (attendace.Id == -1) //Add new
                {
					if (attendanceBLL.AddNewLeaveRegister(attendace))
					{
						ReloadAttendance?.Invoke();
						this.Close();
					}

                }
                else    //Update
				{
					if (attendanceBLL.UpdateAttendance(attendace, isCheck))
					{
						ReloadAttendance?.Invoke();
						this.Close();
					}
				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
