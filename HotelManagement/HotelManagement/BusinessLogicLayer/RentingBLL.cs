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

		public bool RemoveRenting(int id)
		{
			return RentingDAL.Instance.RemoveRenting(id);
		}
	}
}
