using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using HotelManagement.DataTransferObject.Report;
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

		public List<RoomDTO> GetRooms(Rule.ROOM_STATE state)
		{
			List<RoomDTO> rooms = RoomDAL.Instance.GetAll(state);

			List<RoomTypeDTO> roomTypes = new RoomTypeBLL().GetAllRoomTypes();
			foreach (var room in rooms)
			{
				room.RoomTypeName = roomTypes.Find(element => element.Id == room.RoomTypeId).Name;
			}

			return rooms;
		}

		public RoomDTO GetRoom(int id) => RoomDAL.Instance.GetRoomInfor(id);

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

		public int CountAvailableRoom() => RoomDAL.Instance.CountRoomState(Rule.ROOM_STATE.AVAILABLE);

		public bool UpdateRoomState(int id, int state) => RoomDAL.Instance.UpdateRoomState(id, state);

		public List<IncomeRoom> CalculateIncomeRoom(int month, int year)
		{
			List<IncomeRoom> reports = new List<IncomeRoom>();

			var rooms = GetAllRooms();
			var rentings = new RentingBLL().GetAll(month, year);

			foreach (var room in rooms)
			{
				var incomeRoom = new IncomeRoom();
				incomeRoom.RoomName = room.Name;
				incomeRoom.Floor = room.Floor;
				incomeRoom.TypeName = room.RoomTypeName;

				var incomes = rentings.FindAll(element => element.RoomId == room.Id).Select(element => element.Total);
				foreach(var income in incomes)
				{
					incomeRoom.Income += income;
				}

				reports.Add(incomeRoom);
			}

			return reports;
		}
		#endregion
	}
}
