using HotelManagement.DataTransferObject;
using HotelManagement.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.PresentationLayer;

namespace HotelManagement.BusinessLogicLayer
{
	public class VoucherBLL
	{
		#region Methods

		#region Voucher
		public List<VoucherDTO> GetAll() => VoucherDAL.Instance.GetAll();

		public bool AddNewVoucher(VoucherDTO voucher)
		{
			switch (VoucherDAL.Instance.AddNewVoucher(voucher))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					new MessageBoxCustom("Add voucher fail", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;

				case Rule.STATE.EXIST:
					new MessageBoxCustom("Voucher name is existed.", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;
			}

			return false;
		}

		public bool UpdateVoucher(VoucherDTO voucher, bool isCheck = false)
		{
			switch (VoucherDAL.Instance.UpdateVoucher(voucher, isCheck))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					new MessageBoxCustom("Update voucher fail", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;

				case Rule.STATE.EXIST:
					new MessageBoxCustom("Voucher is existed.", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;
			}

			return false;
		}

		public bool DeleteVoucher(int id) => VoucherDAL.Instance.RemoveVoucher(id);

		public bool IsVoucherTypeUsing(int voucherTypeId) => VoucherDAL.Instance.CountExistVoucherWith(voucherTypeId) > 0;
		#endregion

		#region Voucher Type
		public List<VoucherTypeDTO> GetAllVoucherTypes() => VoucherTypeDAL.Instance.GetAll();

		public bool AddNewVoucherType(VoucherTypeDTO voucherType)
		{
			switch (VoucherTypeDAL.Instance.AddNewVoucherType(voucherType))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					new MessageBoxCustom("Add voucher type fail", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;

				case Rule.STATE.EXIST:
					new MessageBoxCustom("Voucher type name is existed.", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;
			}

			return false;
		}

		public bool UpdateVoucherType(VoucherTypeDTO voucherType, bool isCheck = false)
		{
			switch (VoucherTypeDAL.Instance.UpdateVoucherType(voucherType, isCheck))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					new MessageBoxCustom("Update voucher type fail", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;

				case Rule.STATE.EXIST:
					new MessageBoxCustom("Voucher type name is existed.", MessageType.Error, MessageButtons.Ok).ShowDialog();
					return false;
			}

			return false;
		}

		public bool DeleteVoucherType(int id) => VoucherTypeDAL.Instance.RemoveVoucherType(id);

		public int CountAllVoucherType() => VoucherTypeDAL.Instance.GetLargestId();
		#endregion

		#endregion
	}
}
