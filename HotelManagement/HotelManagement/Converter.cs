using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HotelManagement.PresentationLayer
{
    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strNum = System.Convert.ToDouble(value.ToString()).ToString("# ### ###");
            if (strNum.Trim() == "")
			{
                strNum = "0";
			}
            
            return strNum;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
