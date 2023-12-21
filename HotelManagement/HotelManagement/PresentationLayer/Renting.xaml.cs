using HotelManagement.BusinessLogicLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagement.PresentationLayer
{
	/// <summary>
	/// Interaction logic for Renting.xaml
	/// </summary>
	public partial class Renting : UserControl
	{
        List<RentingDTO> rentings;
        int currentRenting = -1;

        public Renting()
		{
			InitializeComponent();

            LoadData();
		}

        private void LoadData()
        {
            rentings = new RentingBLL().GetAll();

            DataGridBooking.ItemsSource = rentings;
            DataGridBooking.SelectedCellsChanged += SelectBooking;
        }

        private void SelectBooking(object sender, SelectedCellsChangedEventArgs e)
        {
            currentRenting = DataGridBooking.SelectedIndex;
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
            if (currentRenting == -1)
            {
                new MessageBoxCustom("Please choose your renting you want to delete", MessageType.Info, MessageButtons.Ok).ShowDialog();
                return;
            }

            var Result = new MessageBoxCustom("Do you want to delete this renting?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                try
                {
                    new RentingBLL().RemoveRenting(rentings[currentRenting].Id);

                    LoadData();

                    new MessageBoxCustom("Delete successfully", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                catch (Exception ex)
                {
                    new MessageBoxCustom(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
        }

    }
}
