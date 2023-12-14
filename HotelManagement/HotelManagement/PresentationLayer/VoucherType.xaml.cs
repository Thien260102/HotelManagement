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
    /// Interaction logic for VoucherType.xaml
    /// </summary>
    public partial class VoucherType : UserControl
    {
        #region Fields & Properties
        List<VoucherTypeDTO> _vouchers;

        int _current = -1;
		#endregion


		public VoucherType()
        {
            InitializeComponent();

            LoadData();
            DataGridVoucherType.SelectedCellsChanged += SelectVoucherType;
            DataGridVoucherType.IsReadOnly = true;
        }

		private void LoadData()
		{
            _vouchers = new VoucherBLL().GetAllVoucherTypes();
            DataGridVoucherType.ItemsSource = _vouchers;
		}

		#region Events
		private void SelectVoucherType(object sender, SelectedCellsChangedEventArgs e)
        {
            _current = DataGridVoucherType.SelectedIndex;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            VoucherTypeinfo vouchertypeinfo = new VoucherTypeinfo();
            vouchertypeinfo.Reload = LoadData;
            vouchertypeinfo.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (_current == -1)
            {
                new MessageBoxCustom("Please choose voucher type.", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            VoucherTypeinfo vouchertypeinfo = new VoucherTypeinfo();
            vouchertypeinfo.Reload = LoadData;
            vouchertypeinfo.SetData(_vouchers[_current]);
            vouchertypeinfo.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_current == -1)
			{
                new MessageBoxCustom("Please choose voucher type.", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
			}

            new VoucherBLL().DeleteVoucherType(_vouchers[_current].Id);
            new MessageBoxCustom("Remove voucher type successfully.", MessageType.Info, MessageButtons.Ok).ShowDialog();
            LoadData();
        }
		#endregion
	}
}
