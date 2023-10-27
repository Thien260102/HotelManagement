using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class RoomDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public string Name { get; set; }

		public int State { get; set; }

		public int RoomTypeId { get; set; }

		//public string RoomTypeName { get; set; }

		public string Note { get; set; }
		#endregion

		#region Methods

		public RoomDTO()
		{
			Id = -1;
		}

		public RoomDTO(int id, string name, int state, int roomTypeId, string note)
		{
			Id = id;
			Name = name;
			State = state;
			RoomTypeId = roomTypeId;
			Note = note;
		}

		public RoomDTO(DataRow row)
		{
			Id = (int)row["ID"];
			Name = row["Name"].ToString();
			State = (int)row["State"];
			RoomTypeId = (int)row["TypeID"];
			Note = row["Note"].ToString();
		}
		#endregion
	}
}
