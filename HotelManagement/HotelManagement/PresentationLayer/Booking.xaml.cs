using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : UserControl
    {
        List<BookingDTO> bookings;
        int currentBooking = -1;

        public Booking()
        {
            InitializeComponent();

            LoadData();

            DataGridBooking.SelectedCellsChanged += SelectBooking;
        }

		private void SelectBooking(object sender, SelectedCellsChangedEventArgs e)
		{
            currentBooking = DataGridBooking.SelectedIndex;
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

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (currentBooking == -1)
            {
                MessageBox.Show("Please choose your booking you want to cancel");
                return;
            }

            var Result = MessageBox.Show("Do you want to cancel this booking?", "Cancel Booking", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                try
                {
                    int remainDays = (int)(DateTime.Parse(bookings[currentBooking].CheckinDate).Date
                                - DateTime.Now.Date).TotalDays;
                    double ratio = 0d;
                    if (remainDays > 15)
                    {
                        ratio = 0.7d;
                    }
                    else if (remainDays > 7 && remainDays <= 15)
                    {
                        ratio = 0.5d;
                    }
                    else if (remainDays > 3 && remainDays <= 7)
                    {
                        ratio = 0.3d;
                    }

                    decimal refund = bookings[currentBooking].Total * (decimal)ratio;

                    new BookingBLL().RemoveBooking(bookings[currentBooking].Id);

                    LoadData(); 

                    MessageBox.Show($"The refunding is {new MoneyConverter().Convert(refund, null, null, null)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
