﻿using System;
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
        public VoucherType()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            VoucherTypeinfo vouchertypeinfo = new VoucherTypeinfo();
            vouchertypeinfo.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
