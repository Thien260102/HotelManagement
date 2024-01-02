using HotelManagement.BusinessLogicLayer;
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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private UserControl currentUserControl;
        private Button currentButton;
        public int expand = -1;
        public int expandvch = -1;
        public Main()
        {
            InitializeComponent();
            btn_Profile.Content = Utilities.GetRole();
            btn_Dashboard_Click(null, null);
        }

		#region click Usercontrol

		public void DisableButton()
        {
            Dashboards.Background = Brushes.Transparent;
            btn_Dashboard.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            RoomTypes.Background = Brushes.Transparent;
            btn_RoomType.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Rooms.Background = Brushes.Transparent;
            btn_Room.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Guests.Background = Brushes.Transparent;
            btn_Guest.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Bookings.Background = Brushes.Transparent;
            btn_Booking.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Employees.Background = Brushes.Transparent;
            btn_Employee.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            VoucherTypes.Background = Brushes.Transparent;
            btn_VoucherType.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Vouchers.Background = Brushes.Transparent;
            btn_Voucher.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Reports.Background = Brushes.Transparent;
            btn_Report.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Bills.Background = Brushes.Transparent;
            btn_Bill.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            BAttendance.Background = Brushes.Transparent;
            btn_Attendance.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            BRenting.Background = Brushes.Transparent;
            btn_Renting.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
        }
        void Button_Choose(object senderButton)
        {
            if (senderButton != null)
            {
                DisableButton();
                currentButton = (Button)senderButton;
                currentButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.HIGHLIGHT);
                
            }
        }
        private void OpenUserControl(UserControl userControl)
        {
            if (currentUserControl != null)
            {
                panelMain.Children.Clear();
            }

            currentUserControl = userControl;
            panelMain.Children.Add(currentUserControl);
        }
        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Dashboards.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Dashboard dashboard = new Dashboard();
            OpenUserControl(dashboard);
        }
        private void btn_RoomSub_Click(object sender, RoutedEventArgs e)
        {
            
            if (expand == -1)
            {
                ChooseRoom.Height = 135;
                expand *= -1;
                image_sub.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/down-arrow.png"));
            }
            else
            {
                ChooseRoom.Height = 45;
                expand *= -1;
                image_sub.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/next.png"));
            }
        }
        private void btn_Room_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Rooms.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Room room = new Room();
            OpenUserControl(room);

        }
        private void btn_RoomType_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            RoomTypes.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            RoomType roomType = new RoomType();
            OpenUserControl(roomType);
        }
        private void btn_Guest_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Guests.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Guest guest = new Guest();
            OpenUserControl(guest);
        }
        private void btn_Booking_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Bookings.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER); 
            Booking booking = new Booking();
            OpenUserControl(booking);
        }
        private void btn_Employee_Click(object sender, RoutedEventArgs e)
        {
            if(Utilities.GetRole() != Rule.ROLE.ADMIN)
			{
                new MessageBoxCustom("You do not have an authority", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
			}

            Button_Choose(sender);
            Employees.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Employee employee = new Employee();
            OpenUserControl(employee);
        }
        private void btn_VoucherSub_Click(object sender, RoutedEventArgs e)
        {
            if (expandvch == -1)
            {
                ChooseVoucher.Height = 135;
                expandvch *= -1;
                image_subvoucher.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/down-arrow.png"));
            }
            else
            {
                ChooseVoucher.Height = 45;
                expandvch *= -1;
                image_subvoucher.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/next.png"));
            }
        }

        private void btn_VoucherType_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            VoucherTypes.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            VoucherType vouchertype = new VoucherType();
            OpenUserControl(vouchertype);
        }
        private void btn_Voucher_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Vouchers.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Voucher voucher = new Voucher();    
            OpenUserControl(voucher);
        }
        private void btn_Report_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Reports.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Report report = new Report();  
            OpenUserControl (report);
        }
       
        private void btn_Attendance_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            BAttendance.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Attendance attendance = new Attendance(Utilities.GetRole());
            OpenUserControl(attendance);
        }

        private void btn_Renting_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            BRenting.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Renting renting = new Renting();
            OpenUserControl(renting);
        }

        #endregion

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            bool? result = new MessageBoxCustom("Are you sure, You want to log out ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (result.Value)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.GetRole() == Rule.ROLE.ADMIN)
            {
                new MessageBoxCustom("Admin does not have personal infor", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            Profile profile = new Profile();
            profile.Show();
            profile.IsChangePassword += ReLogin;
        }

        private void ReLogin()
		{
            new Login().Show();
            this.Close();
		}

        private void btn_Bill_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Bills.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Bill bill = new Bill();
            OpenUserControl (bill);

        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.HOVER);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = Brushes.Transparent;
        }
    }
}
