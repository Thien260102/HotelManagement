using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Voucherinfo.xaml
    /// </summary>
    public partial class Voucherinfo : Window
    {
        #region Fields & Properties
        VoucherDTO _voucher;

        List<VoucherTypeDTO> _voucherTypes;
        int _current;

        public Action Reload;
		#endregion

		public Voucherinfo()
        {
            InitializeComponent();

            txt_Discount.PreviewTextInput += InputOnlyNumber;
            _voucher = new VoucherDTO();

            _voucherTypes = new VoucherBLL().GetAllVoucherTypes();
            foreach(var item in _voucherTypes)
			{
                combobox_VoucherType.Items.Add(item.Name);
			}
            combobox_VoucherType.SelectionChanged += SelectVoucher;
            combobox_VoucherType.SelectedIndex = 0;
            txt_Discount.IsReadOnly = true;

            txt_Expiration.SelectedDate = DateTime.Now.AddMonths(Rule.EXPIRATION_DATE_VOUCHER);
        }

		public void SetData(VoucherDTO voucher)
		{
            _voucher = voucher;

            txt_CustomerId.Text = voucher.CustomerId.ToString();
            txt_Expiration.Text = voucher.ExpirationDate;
		}

		#region Events
		private void SelectVoucher(object sender, SelectionChangedEventArgs e)
        {
            _current = combobox_VoucherType.SelectedIndex;
            txt_Discount.Text = _voucherTypes[_current].Ratio.ToString() + " %";
        }

        private void InputOnlyNumber(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
			{
                int customerID;
                if (!int.TryParse(txt_CustomerId.Text, out customerID))
				{
                    throw new Exception("Customer ID wrong");
				}

                string expirationDate = ((DateTime)txt_Expiration.SelectedDate).ToString("yyyy-MM-dd");
                if((DateTime)txt_Expiration.SelectedDate < DateTime.Now)
				{
                    throw new Exception("Expiration date must larger than today");
                }

                VoucherDTO voucher = new VoucherDTO(customerID, expirationDate, 
                    true, _voucherTypes[_current].Id);

                if (!new VoucherBLL().AddNewVoucher(voucher))
				{
                    throw new Exception("Add new voucher fail");
                }

                new MessageBoxCustom("Insert new voucher successfully.", MessageType.Success, MessageButtons.Ok).ShowDialog();

                Reload?.Invoke();
                this.Close();
            }
            catch (Exception ex)
			{
                new MessageBoxCustom(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
			}
        }
		#endregion
	}
}
