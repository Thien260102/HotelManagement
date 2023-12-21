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

        CustomerDTO _customer;

        public Action Reload;
		#endregion

		public Voucherinfo()
        {
            InitializeComponent();

            _customer = new CustomerDTO();

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

            txt_CustomerPhone.LostFocus += CheckCustomer;
            txt_CustomerCitizenID.LostFocus += CheckCustomer;
        }

        public void SetData(int customerId)
		{
            _voucher = new VoucherDTO();

            _customer = new CustomerBLL().GetCustomer(customerId);
            txt_CustomerCitizenID.Text = _customer.CitizenId;
            txt_CustomerName.Text = _customer.FullName;
            txt_CustomerPhone.Text = _customer.PhoneNumber;

            txt_CustomerPhone.IsReadOnly = true;
            txt_CustomerName.IsReadOnly = true;
            txt_CustomerCitizenID.IsReadOnly = true;
        }
		public void SetData(VoucherDTO voucher)
		{
            _voucher = voucher;

            _customer = new CustomerBLL().GetCustomer(voucher.CustomerId);
            txt_CustomerCitizenID.Text = _customer.CitizenId;
            txt_CustomerName.Text = _customer.FullName;
            txt_CustomerPhone.Text = _customer.PhoneNumber;

            txt_CustomerPhone.IsReadOnly = true;
            txt_CustomerName.IsReadOnly = true;
            txt_CustomerCitizenID.IsReadOnly = true;
            
            txt_Expiration.Text = voucher.ExpirationDate;
		}

        #region Events
        private void CheckCustomer(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Name == "txt_CustomerPhone")
            {
                _customer = new CustomerBLL().GetCustomer("", textBox.Text.Trim());
            }
            else
            {
                _customer = new CustomerBLL().GetCustomer(textBox.Text.Trim(), "");
            }

            if (_customer.Id != -1)
            {
                txt_CustomerName.Text = _customer.FullName;
                txt_CustomerPhone.Text = _customer.PhoneNumber;
                txt_CustomerCitizenID.Text = _customer.CitizenId;
            }
        }


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
                string citizenId = txt_CustomerCitizenID.Text.Trim();
                string customerName = txt_CustomerName.Text.Trim();
                string customerPhone = txt_CustomerPhone.Text.Trim();
                if(string.IsNullOrEmpty(citizenId) || string.IsNullOrEmpty(customerName)
                || string.IsNullOrEmpty(customerPhone))
				{
                    throw new Exception("Please fill all information.");
				}

                if(!Utilities.Validate_CitizenId(citizenId))
				{
                    throw new Exception("Please fill correct citizen ID");
				}

                if (!Utilities.Validate_Phone(customerPhone))
				{
                    throw new Exception("Please fill correct numberphone");
				}

                string expirationDate = ((DateTime)txt_Expiration.SelectedDate).ToString("yyyy-MM-dd");
                if((DateTime)txt_Expiration.SelectedDate < DateTime.Now)
				{
                    throw new Exception("Expiration date must larger than today");
                }

                if(_customer.Id == -1)
				{
                    if (!new CustomerBLL().InsertCustomer(_customer))
					{
                        throw new Exception("Add customer information fail");
					}

				}

                VoucherDTO voucher = new VoucherDTO(new CustomerBLL().GetCustomerId(_customer.CitizenId), 
                    expirationDate, 
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
