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
	public class RoomTypeBLL
	{
		#region Fields & Properties


		#endregion

		#region Methods
		public List<RoomTypeDTO> GetAllRoomTypes() => RoomTypeDAL.Instance.GetAll();

		public string GetRoomTypeName(int id) => RoomTypeDAL.Instance.GetRoomTypeName(id);

		public bool AddNewRoomType(RoomTypeDTO roomType)
		{
			switch (RoomTypeDAL.Instance.AddNewRoomType(roomType))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add new room type fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("Room Type name is existed.");
					return false;
			}

			return false;
		}

		public bool UpdateRoomType(RoomTypeDTO roomType, bool isCheck = false)
		{
			switch (RoomTypeDAL.Instance.UpdateRoomType(roomType, isCheck))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Update room type infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("Room type name is existed.");
					return false;
			}

			return false;
		}

		public bool RemoveRoomType(int id)
		{
			if (id > 0 && id < 5)
			{
				MessageBox.Show("Cannot remove default room type");
				return false;
			}

			if (RoomDAL.Instance.CountRoomWith(id) > 0)
			{
				MessageBox.Show("Please remove room existed");
				return false;
			}

			return RoomTypeDAL.Instance.RemoveRoomType(id);
		}

		public int CountAll() => RoomTypeDAL.Instance.CountAll();
		#endregion
	}
}
