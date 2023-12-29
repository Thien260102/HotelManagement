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
    /// Interaction logic for DBoardVoucher.xaml
    /// </summary>
    public partial class DBoardVoucher : UserControl
    {
        public DBoardVoucher()
        {
            InitializeComponent();
        }

        public void SetData(VoucherTypeDTO voucher)
		{
            VoucherType.Content = voucher.Name;
            Discount.Content = voucher.Ratio + "%";
		}
    }
}
