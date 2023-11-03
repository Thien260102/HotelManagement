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

        public Guest()
        {
            InitializeComponent();

            LoadData();
        }


        private void LoadData()
		{
            customers = new CustomerBLL().GetAllCustomers();

            DataGrid_Guest.ItemsSource = customers;
		}

		#region Button
        private void btn_Add_Click(object sender, RoutedEventArgs e)

        {

        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
		#endregion
	}
}
