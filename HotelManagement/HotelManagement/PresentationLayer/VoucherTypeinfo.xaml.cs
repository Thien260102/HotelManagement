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
    /// Interaction logic for VoucherTypeinfo.xaml
    /// </summary>
    public partial class VoucherTypeinfo : Window
    {
        #region Fields & Properties
        VoucherTypeDTO _voucherType;
        string _originName;

        public Action Reload;
		#endregion

		public VoucherTypeinfo()
        {
            InitializeComponent();

            txt_Discount.PreviewTextInput += InputOnlyNumber;
            _voucherType = new VoucherTypeDTO();
        }

        public void SetData(VoucherTypeDTO voucherType)
        {
            this._voucherType = voucherType;
            txt_Discount.Text = voucherType.Ratio.ToString();
            txt_Name.Text = voucherType.Name;

            _originName = voucherType.Name;
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
                string name = txt_Name.Text.Trim();
                name = name.ToUpper();
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception("Please fill the name");
                }

                int discount;
                if (!int.TryParse(txt_Discount.Text.Trim(), out discount))
                {
                    throw new Exception("Discount must be number > 0 and <= 100");
                }
                else if (discount <= 0 || discount > 100)
				{
                    throw new Exception("Discount must be number > 0 and <= 100");
                }

                bool isCheck = true;
                if (_originName == name)
                {
                    isCheck = false;
                }

                _voucherType.Name = name;
                _voucherType.Ratio = discount;

                VoucherBLL voucherBLL = new VoucherBLL();
                if (_voucherType.Id == -1)  // Add new voucher type
                {
                    _voucherType.Id = voucherBLL.CountAllVoucherType() + 1;
                    if (voucherBLL.AddNewVoucherType(_voucherType))
                    {
                        new MessageBoxCustom("Add new voucher type successfully", MessageType.Success, MessageButtons.Ok).ShowDialog();
                        Reload?.Invoke();
                        this.Close();
                    }
                    else
					{
                        _voucherType.Id = -1;
                        return;
					}
                }
                else                // Update room
                {

                    if (voucherBLL.UpdateVoucherType(_voucherType, isCheck))
                    {
                        new MessageBoxCustom("Update voucher type successfully", MessageType.Success, MessageButtons.Ok).ShowDialog();
                        Reload?.Invoke();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                new MessageBoxCustom(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
