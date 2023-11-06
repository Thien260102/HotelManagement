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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Guest.xaml
    /// </summary>
    public partial class Guest : UserControl
    {
        List<CustomerDTO> customers;
        int currentCustomer = -1;

        public Guest()
        {
            InitializeComponent();

            LoadData();
            DataGrid_Guest.SelectedCellsChanged += SelectGuest;
        }

		private void LoadData()
		{
            customers = new CustomerBLL().GetAllCustomers();

            DataGrid_Guest.ItemsSource = customers;
		}

        private void SelectGuest(object sender, SelectedCellsChangedEventArgs e)
        {
            currentCustomer = DataGrid_Guest.SelectedIndex;
        }

        #region Button
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            GuestInfor guestInfor = new GuestInfor();
            guestInfor.ReloadGuest += LoadData;
            guestInfor.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (currentCustomer == -1)
            {
                MessageBox.Show("Please choose customer you want to change.");
                return;
            }

            GuestInfor guestInfor = new GuestInfor();
            guestInfor.SetData(customers[currentCustomer]);
            guestInfor.ReloadGuest += LoadData;
            guestInfor.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (currentCustomer == -1)
            {
                MessageBox.Show("Please choose customer you want to remove.");
                return;
            }

            new CustomerBLL().RemoveCustomer(customers[currentCustomer].Id);
            LoadData();
        }
		#endregion
	}
}
