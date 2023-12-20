using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;

namespace HotelManagement.DataAccessLayer
{
	class VoucherDAL
	{
        #region Fields & Properties
        private static VoucherDAL instance;

        public static VoucherDAL Instance
        {
            get { if (instance == null) instance = new VoucherDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        public List<VoucherDTO> GetAll()
        {
            List<VoucherDTO> vouchers = new List<VoucherDTO>();

            string query = "Select * from VOUCHER ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow element in data.Rows)
            {
                vouchers.Add(new VoucherDTO(element));
            }

            return vouchers;
        }

        public VoucherDTO GetVoucher(int id)
        {
            string query = "Select * from VOUCHER " +
                "where Id = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count == 1)
                return new VoucherDTO(data.Rows[0]);

            return new VoucherDTO();
        }

        public int CountExistVoucher(int customerId)
        {
            string query = "Select Count(*) from VOUCHER " +
                "where CustomerID = @name ";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { customerId });
        }

        public int CountExistVoucherWith(int type)
        {
            string query = "Select Count(*) from VOUCHER " +
                "where VoucherTypeId = @type ";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { type });
        }

        public Rule.STATE AddNewVoucher(VoucherDTO voucher)
        {
            string query = "Insert into VOUCHER " +
                "Values( @cusId , @expiration , @isAvailable , @typeId ) ";

            //if (CountExistVoucher(voucher.CustomerId) > 0)
            //{
            //    return Rule.STATE.EXIST;
            //}

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { voucher.CustomerId, voucher.ExpirationDate,
                               voucher.IsAvailable, voucher.VoucherTypeId }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }


            return Rule.STATE.FAIL;
        }

        public Rule.STATE UpdateVoucher(VoucherDTO voucher, bool isCheck = false)
        {
            string query = "UPDATE VOUCHER " +
                "SET CustomerId = @customerId , ExpirationDate = @date , IsAvailable = @isAvailable , " +
                "TypeID = @typeId " +
                "WHERE ID = @id ";

            if (isCheck && CountExistVoucher(voucher.CustomerId) > 0)
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { voucher.CustomerId, voucher.ExpirationDate,
                               voucher.IsAvailable, voucher.VoucherTypeId, voucher.Id }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool RemoveVoucher(int id)
        {
            string query = "Delete from VOUCHER " +
                "where ID = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0)
                return true;

            return false;
        }

        public int CountAll()
        {
            string query = "Select Count(*) from VOUCHER";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { });
        }
        #endregion
    }
}
