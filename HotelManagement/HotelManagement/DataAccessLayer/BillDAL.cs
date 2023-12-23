using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class BillDAL
	{
        #region Fields & Properties
        private static BillDAL instance;

        public static BillDAL Instance
        {
            get { if (instance == null) instance = new BillDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        public List<BillDTO> GetAll()
		{
            string query = "Select * from BILL ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            var bills = new List<BillDTO>();

            foreach(DataRow row in data.Rows)
			{
                bills.Add(new BillDTO(row));
			}

            return bills;
        }

        public BillDTO GetBill(int id)
        {
            string query = "Select * from BILL " +
                "where ID = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count == 1)
               return new BillDTO(data.Rows[0]);

            return new BillDTO();
        }


        public Rule.STATE AddNewBill(BillDTO bill)
        {
            string query = "Insert into BILL " +
                "Values( @billDate , @userName , @customerId , @rentingId , @totalDay , @total ) ";

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { bill.BillDate, bill.Username, bill.CustomerId,
                               bill.RentingId, bill.TotalDay, bill.Total }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }


            return Rule.STATE.FAIL;
        }

        public bool RemoveBill(int id)
        {
            string query = "Delete from BILL " +
                "where ID = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0)
                return true;

            return false;
        }
        #endregion
    }
}
