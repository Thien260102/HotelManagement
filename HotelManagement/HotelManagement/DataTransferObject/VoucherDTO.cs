using HotelManagement.BusinessLogicLayer;
using System;
using System.Data;

namespace HotelManagement.DataTransferObject
{
	public class VoucherDTO
	{
		#region Fields & Properties
		public int Id { get; set; }

		public int CustomerId { get; set; }

		public string CustomerName { get; set; }
		public string CustomerPhone { get; set; }

		public string ExpirationDate { get; set; }

		public bool IsAvailable { get; set; }

		public int VoucherTypeId { get; set; }
		public string VoucherTypeName { get; set; }
		public int Ratio { get; set; }
		#endregion

		#region Methods
		public VoucherDTO()
		{
			Id = -1;
		}

		public VoucherDTO(int customerId, string expirationDate, bool isAvailable, int typeId)
		{
			CustomerId = customerId;
			var customer = new CustomerBLL().GetCustomer(CustomerId);
			CustomerName = customer.FullName;
			CustomerPhone = customer.PhoneNumber;

			ExpirationDate = expirationDate;
			IsAvailable = isAvailable;
			VoucherTypeId = typeId;
		}

		public VoucherDTO(DataRow row)
		{
			Id = (int)row["ID"];
			CustomerId = (int)row["CustomerId"];
			var customer = new CustomerBLL().GetCustomer(CustomerId);
			CustomerName = customer.FullName;
			CustomerPhone = customer.PhoneNumber;

			ExpirationDate = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(row["ExpirationDate"].ToString()));
			IsAvailable = (bool)row["IsAvailable"];
			VoucherTypeId = (int)row["TypeID"];

			var voucherBLL = new VoucherBLL();
			VoucherTypeName = voucherBLL.GetVoucherTypeName(VoucherTypeId);
			Ratio = voucherBLL.GetVoucherRatio(VoucherTypeId);
		}

		#endregion
	}
}
