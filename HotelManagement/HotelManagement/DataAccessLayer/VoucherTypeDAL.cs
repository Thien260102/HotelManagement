using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;

namespace HotelManagement.DataAccessLayer
{
	public class VoucherTypeDAL
	{
        #region Fields & Properties
        private static VoucherTypeDAL instance;

        public static VoucherTypeDAL Instance
        {
            get { if (instance == null) instance = new VoucherTypeDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        public List<VoucherTypeDTO> GetAll()
        {
            List<VoucherTypeDTO> voucherTypes = new List<VoucherTypeDTO>();

            string query = "Select * from VOUCHER_TYPE ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow element in data.Rows)
            {
                voucherTypes.Add(new VoucherTypeDTO(element));
            }

            return voucherTypes;
        }

        public string GetVoucherTypeName(int id)
        {
            string query = "Select * from VOUCHER_TYPE " +
                "where Id = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count == 1)
                return new VoucherTypeDTO(data.Rows[0]).Name;

            return null;
        }

        public int GetRatio(int id)
        {
            string query = "Select * from VOUCHER_TYPE " +
                "where Id = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count == 1)
                return new VoucherTypeDTO(data.Rows[0]).Ratio;

            return 0;
        }

        public int CountExistVoucher(string voucherName)
        {
            string query = "Select Count(*) from VOUCHER_TYPE " +
                "where Name = @name ";

            return (int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { voucherName });
        }

        public Rule.STATE AddNewVoucherType(VoucherTypeDTO voucher)
        {
            string query = "Insert into VOUCHER_TYPE " +
                "Values( @id , @Name , @ratio ) ";

            if (CountExistVoucher(voucher.Name) > 0)
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { voucher.Id, voucher.Name,
                               voucher.Ratio }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }


            return Rule.STATE.FAIL;
        }

        public Rule.STATE UpdateVoucherType(VoucherTypeDTO voucher, bool isCheck = false)
        {
            string query = "UPDATE VOUCHER_TYPE " +
                "SET Name = @name , Ratio = @ratio " +
                "WHERE ID = @id ";

            if (isCheck && CountExistVoucher(voucher.Name) > 0)
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { voucher.Name, voucher.Ratio, voucher.Id }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool RemoveVoucherType(int id)
        {
            string query = "Delete from VOUCHER_TYPE " +
                "where ID = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0)
                return true;

            return false;
        }

        public int GetVoucherTypeID(string voucherTypeName)
        {
            string query = "Select * from VOUCHER_TYPE " +
                "where Name = @name ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { voucherTypeName });

            if (data.Rows.Count == 1)
                return new VoucherTypeDTO(data.Rows[0]).Id;

            return 0;
        }

        public int GetLargestId()
        {
            string query = "Select MAX(ID) from VOUCHER_TYPE";

            object result = DataProvider.Instance.ExecuteScalar(query);
            if (result is null)
                return 0;

            return (int)result;
        }
        #endregion
    }
}
