using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class RoomTypeDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public string Name { get; set; }

		public int HighestNumberPeople { get; set; }

		public decimal Price { get; set; }
		#endregion

		#region Methods

		public RoomTypeDTO()
		{
			Id = -1;
		}

		public RoomTypeDTO(int id, string name, int totalNumber, decimal price)
		{
			Id = id;
			Name = name;
			HighestNumberPeople = totalNumber;
			Price = price;
		}

		public RoomTypeDTO(DataRow row)
		{
			Id = (int)row["ID"];
			Name = row["Name"].ToString();
			HighestNumberPeople = (int)row["HighestNumberPeople"];
			Price = (decimal)row["Price"];
		}
		#endregion
	}
}
