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
    /// Interaction logic for Voucher.xaml
    /// </summary>
    public partial class Voucher : UserControl
    {
        #region Fields & Properties
        List<VoucherDTO> _vouchers;
        int _current = -1;

		#endregion

		public Voucher()
        {
            InitializeComponent();

            DataGridVoucher.SelectedCellsChanged += SelectVoucher;
            DataGridVoucher.IsReadOnly = true;

            LoadData();
        }

        private void LoadData()
		{
            _vouchers = new VoucherBLL().GetAll();
            DataGridVoucher.ItemsSource = _vouchers;
		}

		#region Events
		private void SelectVoucher(object sender, SelectedCellsChangedEventArgs e)
		{
            _current = DataGridVoucher.SelectedIndex;
		}

		private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Voucherinfo voucherinfo = new Voucherinfo();
            voucherinfo.Reload = LoadData;
            voucherinfo.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_current == -1)
            {
                new MessageBoxCustom("Please choose voucher.", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            new VoucherBLL().DeleteVoucherType(_vouchers[_current].Id);
            new MessageBoxCustom("Remove voucher successfully.", MessageType.Info, MessageButtons.Ok).ShowDialog();
            LoadData();
        }
		#endregion
	}
}
