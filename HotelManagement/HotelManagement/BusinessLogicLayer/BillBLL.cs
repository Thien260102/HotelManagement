using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.BusinessLogicLayer
{
	public class BillBLL
	{
		public List<BillDTO> GetAll() => BillDAL.Instance.GetAll();

		public bool InsertBill(BillDTO bill)
		{
			switch (BillDAL.Instance.AddNewBill(bill))
			{
				case Rule.STATE.SUCCESS:

					return true;

				case Rule.STATE.EXIST:
					return false;

				case Rule.STATE.FAIL:
					return false;
			}

			return false;
		}

		public bool RemoveBill(int id) => BillDAL.Instance.RemoveBill(id);
	}
}
