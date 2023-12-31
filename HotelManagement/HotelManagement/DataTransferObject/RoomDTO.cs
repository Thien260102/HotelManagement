﻿using HotelManagement.DataAccessLayer;
using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class RoomDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public string Name { get; set; }

		public int Floor { get; set; }

		public int State { get; set; }
		public string StateName { get; set; }

		public int RoomTypeId { get; set; }
		public string RoomTypeName { get; set; }
		public int TotalPeople { get; set; }

		public string Note { get; set; }
		#endregion

		#region Methods

		public RoomDTO()
		{
			Id = -1;
		}

		public RoomDTO(int id, string name, int floor, int state, int roomTypeId, string note)
		{
			Id = id;
			Name = name;
			Floor = floor;
			State = state;
			StateName = ((Rule.ROOM_STATE)state).ToString();
			RoomTypeId = roomTypeId;
			Note = note;
		}

		public RoomDTO(DataRow row)
		{
			Id = (int)row["ID"];
			Name = row["Name"].ToString();
			Floor = (int)row["FloorNumber"];

			State = (int)row["State"];
			StateName = ((Rule.ROOM_STATE)State).ToString();

			RoomTypeId = (int)row["TypeID"];
			RoomTypeName = RoomTypeDAL.Instance.GetRoomTypeName(RoomTypeId);
			TotalPeople = RoomTypeDAL.Instance.GetTotalNumber(RoomTypeId);

			Note = row["Note"].ToString();
		}
		#endregion
	}
}
