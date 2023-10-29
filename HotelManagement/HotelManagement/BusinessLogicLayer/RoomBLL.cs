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
	public class RoomBLL
	{
		#region Fields & Properties


		#endregion

		#region Methods
		public List<RoomDTO> GetAllRooms()
		{
			List<RoomDTO> rooms = RoomDAL.Instance.GetAll();

			List<RoomTypeDTO> roomTypes = new RoomTypeBLL().GetAllRoomTypes();
			foreach (var room in rooms)
			{
				room.RoomTypeName = roomTypes.Find(element => element.Id == room.RoomTypeId).Name;
			}

			return rooms;
		}

		public bool AddNewRoom(RoomDTO room)
		{
			switch (RoomDAL.Instance.AddNewRoom(room))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add new room fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("Room name is existed.");
					return false;
			}

			return false;
		}

		public bool UpdateRoom(RoomDTO room, bool isCheck = false)
		{
			switch (RoomDAL.Instance.UpdateRoom(room, isCheck))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Update room infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("Room name is existed.");
					return false;
			}

			return false;
		}

		public bool RemoveRoom(int id) => RoomDAL.Instance.RemoveRoom(id);
		#endregion
	}
}
