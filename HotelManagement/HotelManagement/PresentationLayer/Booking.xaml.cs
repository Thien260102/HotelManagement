using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : UserControl
    {
        List<BookingDTO> bookings;

        public Booking()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
		{
            bookings = new BookingBLL().GetAll();

            DataGridBooking.ItemsSource = bookings;
		}

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            ReceiveBooking receiveBooking = new ReceiveBooking();
            receiveBooking.Show();
            receiveBooking.ReloadBooking += LoadData;
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
