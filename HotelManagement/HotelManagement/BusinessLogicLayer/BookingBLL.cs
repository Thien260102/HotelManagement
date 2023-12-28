using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using HotelManagement.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.BusinessLogicLayer
{
	public class BookingBLL
	{
		public List<BookingDTO> GetAll() => BookingDAL.Instance.GetAll();

		public bool InsertBooking(BookingDTO booking)
		{
			switch (BookingDAL.Instance.AddNewBooking(booking))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					return false;

				case Rule.STATE.EXIST:
					return false;
			}

			return false;
		}

		public bool UpdateBooking(BookingDTO booking)
		{
			switch (BookingDAL.Instance.UpdateBooking(booking))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add booking infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("The previous booking still remain.");
					return false;
			}

			return false;
		}

		public bool RemoveBooking(int id)
		{
			return BookingDAL.Instance.RemoveBooking(id);
		}

		public int CountBooking(bool isRented = false) => BookingDAL.Instance.CountBooking(isRented);
	}
}
