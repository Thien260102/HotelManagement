using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HotelManagement.PresentationLayer
{
	/// <summary>
	/// Interaction logic for Renting.xaml
	/// </summary>
	public partial class Renting : UserControl
	{
		#region Fields & Properties
		List<RentingDTO> _rentings;
        Dictionary<bool, List<RentingDTO>> _filterRooms;
        int _currentRenting = -1;
        List<string> _searchTypes;
        int _currentSearchType = 0;

        #endregion

        public Renting()
		{
			InitializeComponent();

            _filterRooms = new Dictionary<bool, List<RentingDTO>>();
            _filterRooms.Add(true, new List<RentingDTO>());
            _filterRooms.Add(false, new List<RentingDTO>());
            LoadData();

            btn_IsPaid.Click += IsPaid;
            btn_Available.Click += Available;
            DataGridBooking.MouseDoubleClick += ExportBill;
		}

		private void LoadData()
        {
            _rentings = new RentingBLL().GetAll();

            _filterRooms[false].Clear();
            _filterRooms[true].Clear();
            foreach (var renting in _rentings)
			{
                _filterRooms[renting.IsPaid].Add(renting);
			}

            DataGridBooking.ItemsSource = _rentings;
            DataGridBooking.SelectedCellsChanged += SelectBooking;

            _searchTypes = new List<string>()
            {
                "Name",
                "Citizen ID",
                "Room ID"
            };
            foreach (var file in _searchTypes)
            {
                Combobox_TypeSearch.Items.Add(file);
            }
            Combobox_TypeSearch.SelectedIndex = _currentSearchType;
            //Combobox_TypeSearch.SelectionChanged += SelectSearch;
        }

        private void FilterRenting(bool state)
        {
            _currentRenting = -1;
            DataGridBooking.ItemsSource = _filterRooms[state];
        }

        #region Events
        private void ExportBill(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;

                    var renting = _rentings[dgr.GetIndex()];
                    if (renting.IsPaid)
                    {
                        new MessageBoxCustom("The renting already paid.", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                        return;
                    }
                    
                    Billinfor bill = new Billinfor();
                    bill.Show();
                    bill.SetData(renting);
                    bill.ReloadParent = LoadData;
                }
            }
        }
        public void DisableButton()
        {
            BdIsPaid.Background = Brushes.White;
            BdIsPaid.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_IsPaid.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);

            BdAvaliable.Background = Brushes.White;
            BdAvaliable.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);
            btn_Available.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(Rule.BUTTON.NORMAL);

        }
        private void Available(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdAvaliable.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FAC5C1");
            BdAvaliable.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#F04438");
            btn_Available.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#F04438");
            FilterRenting(false);
        }

        private void IsPaid(object sender, RoutedEventArgs e)
        {
            DisableButton();
            BdIsPaid.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8F1FD");
            BdIsPaid.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            btn_IsPaid.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1570EF");
            FilterRenting(true);
        }

        private void SelectBooking(object sender, SelectedCellsChangedEventArgs e)
        {
            _currentRenting = DataGridBooking.SelectedIndex;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            RentingRoom rentingRoom = new RentingRoom();
            rentingRoom.Show();
            rentingRoom.SetData();
            rentingRoom.ReloadParent += LoadData;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_currentRenting == -1)
            {
                new MessageBoxCustom("Please choose your renting you want to delete", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            var Result = new MessageBoxCustom("Do you want to delete this renting?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                try
                {
                    new RentingBLL().RemoveRenting(_rentings[_currentRenting].Id);

                    LoadData();

                    new MessageBoxCustom("Delete successfully", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                catch (Exception ex)
                {
                    new MessageBoxCustom(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
        }

		#endregion
	}
}
