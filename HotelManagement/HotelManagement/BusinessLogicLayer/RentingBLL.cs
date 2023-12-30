using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.BusinessLogicLayer
{
	public class RentingBLL
	{
		public List<RentingDTO> GetAll() => RentingDAL.Instance.GetAll();

		public List<RentingDTO> GetAll(int month, int year) => RentingDAL.Instance.GetAll(month, year);

		public List<RentingDTO> GetAll(int year) => RentingDAL.Instance.GetAll(year);

		public bool InsertRenting(RentingDTO renting)
		{
			switch (RentingDAL.Instance.AddNewRenting(renting))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add renting infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("The previous renting still remain.");
					return false;
			}

			return false;
		}

		public bool UpdateRenting(RentingDTO renting)
		{
			switch (RentingDAL.Instance.Update(renting))
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

		public bool RemoveRenting(int id)
		{
			return RentingDAL.Instance.RemoveRenting(id);
		}

		public int CountRentingToday() => RentingDAL.Instance.CountRenting(DateTime.Now.ToString("yyyy-MM-dd"));

		public int CountCustomers() => RentingDAL.Instance.CountCustomers();
	
	}
}
