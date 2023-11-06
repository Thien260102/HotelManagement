using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DataAccessLayer
{
	public class EmployeeDAL
	{
        #region Fields & Properties
        private static EmployeeDAL instance;

        public static EmployeeDAL Instance
        {
            get { if (instance == null) instance = new EmployeeDAL(); return instance; }
            private set { instance = value; }
        }
        #endregion

        #region Methods
        private EmployeeDAL() { }

        public List<EmployeeDTO> GetAll()
		{
            List<EmployeeDTO> employees = new List<EmployeeDTO>();

            string query = "Select * from EMPLOYEE ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {});

            foreach(DataRow row in data.Rows)
			{
                employees.Add(new EmployeeDTO(row));
			}

            return employees;
		}

        public EmployeeDTO GetEmployee(int id)
		{
            string query = "Select * from EMPLOYEE " +
                "where ID = @id ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if(data.Rows.Count > 0)
			{
                return new EmployeeDTO(data.Rows[0]);
			}

            return null;
        }

        public Rule.STATE AddNewEmployee(EmployeeDTO employee)
        {
            string query = "Insert into EMPLOYEE " +
                "Values( @citizenID , @Fullname , @Phone , @Sex , @Birth , @Start , @Salary ) ";

            if(IsExistCitizenId(employee.CitizenId) || IsExistPhone(employee.PhoneNumber))
			{
                return Rule.STATE.EXIST;
            }

            if(DataProvider.Instance.ExecuteNonQuery(query, 
                new object[] { employee.CitizenId, employee.FullName, employee.PhoneNumber
                             , employee.Sex, employee.BirthDay, employee.StartDay, employee.Salary}) > 0)
			{
                return Rule.STATE.SUCCESS;
			}

            return Rule.STATE.FAIL;
        }

        public bool IsExistCitizenId(string citizenId)
		{
            string query = "Select Count(*) from EMPLOYEE " +
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
            string query = "Select Count(*) from EMPLOYEE " +
                "where PhoneNumber = @phone ";

            if ((int)DataProvider.Instance.ExecuteScalar(query,
                new object[] { phone }) > 0)
            {
                return true;
            }

            return false;
        }

        public int GetEmployeeID(string citizenID)
		{
            string query = "Select * from EMPLOYEE " +
                " where CitizenID = @citizenID ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { citizenID });

            if (data.Rows.Count > 0)
                return new EmployeeDTO(data.Rows[0]).Id;

            return -1; // not exist
		}

        public bool RemoveEmployee(int Id)
		{
            string query = "Delete from EMPLOYEE " +
                "where Id = @id ";

            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { Id }) > 0)
                return true;

            return false;
		}

        public Rule.STATE UpdateEmployee(EmployeeDTO employee, bool isCheck)
		{
            string query = "UPDATE EMPLOYEE " +
                "SET FullName = @name , PhoneNumber = @phone , " +
                "Sex = @sex , BirthDay = @birth , StartDay = @start , Salary = @salary " +
                "WHERE ID = @id ";

            if (isCheck && IsExistPhone(employee.PhoneNumber))
			{
                return Rule.STATE.EXIST;
			}

            if (DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { employee.FullName, employee.PhoneNumber
                             , employee.Sex, employee.BirthDay, employee.StartDay, 
                               employee.Salary, employee.Id}) > 0)
            {
                return Rule.STATE.SUCCESS;
            }

            return Rule.STATE.FAIL;
        }
        #endregion
    }
}
