using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;

namespace HotelManagement.DataAccessLayer
{
	public class CustomerDAL
	{
        #region Fields & Properties
        private static CustomerDAL instance;

        public static CustomerDAL Instance
        {
            get { if (instance == null) instance = new CustomerDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        private CustomerDAL() { }

        public List<CustomerDTO> GetAll()
        {
            List<CustomerDTO> customers = new List<CustomerDTO>();

            string query = "Select * from CUSTOMER ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });

            foreach (DataRow row in data.Rows)
            {
                customers.Add(new CustomerDTO(row));
            }

            return customers;
        }

        public CustomerDTO GetCustomer(int id)
        {
            string query = "Select * from CUSTOMER " +
                "where ID = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count > 0)
            {
                return new CustomerDTO(data.Rows[0]);
            }

            return null;
        }

        public Rule.STATE AddNewCustomer(CustomerDTO customer)
        {
            string query = "Insert into CUSTOMER " +
                "Values( @citizenID , @Fullname , @Phone , @Sex , @Birth , @nationality ) ";

            if (IsExistCitizenId(customer.CitizenId) || IsExistPhone(customer.PhoneNumber))
            {
                return Rule.STATE.EXIST;
            }

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { customer.CitizenId, customer.FullName, customer.PhoneNumber
                             , customer.Sex, customer.BirthDay, customer.Nationality }) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public bool IsExistCitizenId(string citizenId)
        {
            string query = "Select Count(*) from CUSTOMER " +
                "where CitizenID = @id ";

            if ((int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { citizenId }) > 0)
            {
                return true;
            }

            return false;
        }

        public bool IsExistPhone(string phone)
        {
            string query = "Select Count(*) from CUSTOMER " +
                "where PhoneNumber = @phone ";

            if ((int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { phone }) > 0)
            {
                return true;
            }

            return false;
        }

        public int GetCustomerID(string citizenID)
        {
            string query = "Select * from CUSTOMER " +
                " where CitizenID = @citizenID ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { citizenID });

            if (data.Rows.Count > 0)
                return new CustomerDTO(data.Rows[0]).Id;

            return -1; // not exist
        }

        public bool RemoveCustomer(int Id)
        {
            string query = "Delete from CUSTOMER " +
                "where Id = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { Id }) > 0)
                return true;

            return false;
        }

        public Rule.STATE UpdateCustomer(CustomerDTO customer, bool isCheck)
        {
            string query = "UPDATE CUSTOMER " +
                "SET FullName = @name , PhoneNumber = @phone , " +
                "Sex = @sex , BirthDay = @birth , Nationality = @nationality " +
                "WHERE ID = @id ";

            if (isCheck && IsExistPhone(customer.PhoneNumber))
            {
                return Rule.STATE.EXIST;
			}

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { customer.FullName, customer.PhoneNumber, 
                               customer.Sex, customer.BirthDay, 
                               customer.Nationality, customer.Id}) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }

        public CustomerDTO GetCustomer(string citizenId, string phone)
		{
            string query = "Select * from CUSTOMER " +
                "where CitizenID = @citizenId Or PhoneNumber = @phone ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { citizenId, phone });

            if (data.Rows.Count > 0)
            {
                return new CustomerDTO(data.Rows[0]);
            }

            return new CustomerDTO();
        }

        #endregion
    }
}
