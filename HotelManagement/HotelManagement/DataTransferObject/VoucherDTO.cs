using System;
using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class VoucherDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public int CustomerId { get; set; }

		public string ExpirationDate { get; set; }

		public bool IsAvailable { get; set; }

		public int VoucherTypeId { get; set; }
		#endregion

		#region Methods
		public VoucherDTO()
		{
			Id = -1;
		}

		public VoucherDTO(int customerId, string expirationDate, bool isAvailable, int typeId)
		{
			CustomerId = customerId;
			ExpirationDate = expirationDate;
			IsAvailable = isAvailable;
			VoucherTypeId = typeId;
		}

		public VoucherDTO(DataRow row)
		{
			Id = (int)row["ID"];
			CustomerId = (int)row["CustomerId"];
			ExpirationDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["ExpirationDate"].ToString()));
			IsAvailable = (bool)row["IsAvailable"];
			VoucherTypeId = (int)row["TypeID"];
		}

		#endregion
	}
}
