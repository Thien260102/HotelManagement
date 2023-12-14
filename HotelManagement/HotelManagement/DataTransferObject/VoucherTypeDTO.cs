using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class VoucherTypeDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public string Name { get; set; }

		public int Ratio { get; set; }
		#endregion

		#region Methods
		public VoucherTypeDTO()
		{
			Id = -1;
		}

		public VoucherTypeDTO(int id, string name, int ratio)
		{
			Id = id;
			Name = name;
			Ratio = ratio;
		}

		public VoucherTypeDTO(DataRow row)
		{
			Id = (int)row["ID"];
			Name = (string)row["Name"];
			Ratio = (int)row["Ratio"];
		}

		#endregion
	}
}
