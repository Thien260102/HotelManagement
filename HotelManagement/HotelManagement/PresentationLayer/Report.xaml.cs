using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using HotelManagement.DataTransferObject.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagement.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
		#region Fields & Properties
		List<string> _reportTypes;
        int _currentReport = 0;

        List<string> _fileTypes;
        int _currentFileType = 0;

        string _fileName = "";

        #region Reports
        List<EmployeeSalary> _employeeSalaries;

        List<IncomeRoom> _incomeRooms;

        List<MostUsedRoom> _mostUsedRooms;

        List<CustomerReturn> _customerReturns;

        List<CustomerType> _customerTypes;
		#endregion
		#endregion

		public Report()
        {
            InitializeComponent();

            LoadData();

            btn_Process.Click += ProcessingData;

            txt_Month.PreviewTextInput += InputOnlyNumber;
            txt_Year.PreviewTextInput += InputOnlyNumber;
            txt_Month.Text = DateTime.Now.Month.ToString();
            txt_Year.Text = DateTime.Now.Year.ToString();
        }

		private void LoadData()
		{
            _reportTypes = new List<string>()
            {
                "Income Room Report",
                "Most Uses Room Report",
                "Customer Report",
                "Type Customer",
                "Employee Report",
            };
            foreach (var report in _reportTypes)
            {
                Combobox_TypeReport.Items.Add(report);
            }

            Combobox_TypeReport.SelectedIndex = _currentReport;
            Combobox_TypeReport.SelectionChanged += SelectReport;

            _fileTypes = new List<string>()
            {
                ".csv",
                ".pdf",
                ".doc"
            };
            foreach (var file in _fileTypes)
            {
                Combobox_TypeExport.Items.Add(file);
            }

            Combobox_TypeExport.SelectedIndex = _currentFileType;
            Combobox_TypeExport.SelectionChanged += SelectExport;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        #region Events
        private void InputOnlyNumber(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void SelectExport(object sender, SelectionChangedEventArgs e)
        {
            _currentFileType = Combobox_TypeExport.SelectedIndex;
        }

        private void SelectReport(object sender, SelectionChangedEventArgs e)
        {
            _currentReport = Combobox_TypeReport.SelectedIndex;
        }


        private void ProcessingData(object sender, RoutedEventArgs e)
        {
            int month = 0;
            if (_reportTypes[_currentReport] != "Customer Report"
                && _reportTypes[_currentReport] != "Type Customer"
                && !int.TryParse(txt_Month.Text.Trim(), out month))
			{
                new MessageBoxCustom("Input month truely", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
			}
            int year = 2023;
            if (_reportTypes[_currentReport] != "Type Customer"
                && !int.TryParse(txt_Year.Text.Trim(), out year))
            {
                new MessageBoxCustom("Input year truely", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            _fileName = _reportTypes[_currentReport] + DateTime.Now.ToString("HHmmss dd-MM-yyyy");
            switch (_currentReport)
            {
                case 0: // Income Room
                    _incomeRooms = new RoomBLL().CalculateIncomeRoom(month, year);
                    DataGridReport.ItemsSource = _incomeRooms;
                    break;

                case 1: // Most used Room
                    _mostUsedRooms = new RoomBLL().CalculateUsesRoom(month, year);
                    DataGridReport.ItemsSource = _mostUsedRooms;
                    break;

                case 2: // Customer
                    _customerReturns = new CustomerBLL().CalculateUsesRoom(year);
                    DataGridReport.ItemsSource = _customerReturns;
                    break;

                case 3: // Type customer
                    _customerTypes = new CustomerBLL().CalculateTypeCustomer();
                    DataGridReport.ItemsSource = _customerTypes;
                    break;

                case 4: // Employee
                    _employeeSalaries = new EmployeeSalaryReportBLL().CalculateEmployeeSalaryMonth(month, year);
                    DataGridReport.ItemsSource = _employeeSalaries;
                    break;
            }

            DataGridReport.Tag = _currentReport;
        }

		private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridReport.Items.Count == 0)
			{
                new MessageBoxCustom("Please processing data before export.", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
			}

            switch(_currentFileType)
			{
                case 0: // csv
                    switch((int)DataGridReport.Tag)
					{
                        case 0: // Income Room
                            ReportUtilities.ExportToExcel(ToDataTable(_incomeRooms), _fileName);
                            break;

                        case 1: // Most used Room
                            ReportUtilities.ExportToExcel(ToDataTable(_mostUsedRooms), _fileName);
                            break;

                        case 2: // Customer returns
                            ReportUtilities.ExportToExcel(ToDataTable(_customerReturns), _fileName);
                            break;

                        case 3: // Customer Type
                            ReportUtilities.ExportToExcel(ToDataTable(_customerTypes), _fileName);
                            break;

                        case 4: // Employee
                            ReportUtilities.ExportToExcel(ToDataTable(_employeeSalaries), _fileName);
                            break;
                    }

                    break;

                case 1: // pdf
                    switch ((int)DataGridReport.Tag)
                    {
                        case 0: // Income Room
                            ReportUtilities.ExportToPDF(ToDataTable(_incomeRooms), _fileName);
                            break;

                        case 1: // Most used Room
                            ReportUtilities.ExportToPDF(ToDataTable(_mostUsedRooms), _fileName);
                            break;

                        case 2: // Customer returns
                            ReportUtilities.ExportToPDF(ToDataTable(_customerReturns), _fileName);
                            break;

                        case 3: // Customer type
                            ReportUtilities.ExportToPDF(ToDataTable(_customerTypes), _fileName);
                            break;

                        case 4: // Employee
                            ReportUtilities.ExportToPDF(ToDataTable(_employeeSalaries), _fileName);
                            break;
                    }
                    break;

                case 2: // word
                    switch ((int)DataGridReport.Tag)
                    {
                        case 0: // Income Room
                            ReportUtilities.ExportToWord(ToDataTable(_incomeRooms), _fileName);
                            break;

                        case 1: // Most used Room
                            ReportUtilities.ExportToWord(ToDataTable(_mostUsedRooms), _fileName);
                            break;

                        case 2: // Customer returns
                            ReportUtilities.ExportToWord(ToDataTable(_customerReturns), _fileName);
                            break;

                        case 3: // Customer types
                            ReportUtilities.ExportToWord(ToDataTable(_customerTypes), _fileName);
                            break;

                        case 4: // Employee
                            ReportUtilities.ExportToWord(ToDataTable(_employeeSalaries), _fileName);
                            break;
                    }
                    break;
			}
        }

		#endregion
	}
}
