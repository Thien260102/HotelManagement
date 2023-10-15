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
        public Main()
        {
            InitializeComponent();
        }
        public void DisableButton()
        {
            Dashboards.Background = Brushes.Transparent;
            btn_Dashboard.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Rooms.Background = Brushes.Transparent;
            btn_Room.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Guests.Background = Brushes.Transparent;
            btn_Guest.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Bookings.Background = Brushes.Transparent;
            btn_Booking.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Employees.Background = Brushes.Transparent;
            btn_Employee.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Vouchers.Background = Brushes.Transparent;
            btn_Voucher.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            Reports.Background = Brushes.Transparent;
            btn_Report.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
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
        private void btn_Room_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Rooms.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Room room = new Room();
            OpenUserControl(room);
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
            Button_Choose(sender);
            Employees.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Employee employee = new Employee();
            OpenUserControl(employee);
        }
        private void btn_Voucher_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            Vouchers.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.BORDER);
            Voucher voucher = new Voucher();    
            OpenUserControl(voucher);
        }
       

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
            Login login = new Login();
            login.Show();
            this.Close();
        }

       
    }
}
