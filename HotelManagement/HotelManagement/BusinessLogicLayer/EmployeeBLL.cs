﻿using HotelManagement.DataAccessLayer;
using HotelManagement.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.BusinessLogicLayer
{
	public class EmployeeBLL
	{
		#region Fields & Properties


		#endregion

		#region Methods

		public bool InsertEmployee(EmployeeDTO employee)
		{
			switch (EmployeeDAL.Instance.AddNewEmployee(employee))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Add employee infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("CitizenID or Phone Number is existed.");
					return false;
			}

			return false;
		}

		public bool UpdateEmployee(EmployeeDTO employee, bool isCheck = false)
		{
			switch (EmployeeDAL.Instance.UpdateEmployee(employee, isCheck))
			{
				case Rule.STATE.SUCCESS:
					return true;

				case Rule.STATE.FAIL:
					MessageBox.Show("Update employee infor fail");
					return false;

				case Rule.STATE.EXIST:
					MessageBox.Show("Phone number is existed.");
					return false;
			}

			return false;
		}

		public int GetEmployeeId(string citizenId)
		{
			return EmployeeDAL.Instance.GetEmployeeID(citizenId);
		}

		public EmployeeDTO GetEmployee(int id) => EmployeeDAL.Instance.GetEmployee(id);

		public string GetEmployeeName(int id)
		{
			EmployeeDTO employee = EmployeeDAL.Instance.GetEmployee(id);
			if (employee == null)
			{
				return "Null";
			}

			return employee.FullName;
		}

		public bool RemoveEmployee(int id)
		{
			return EmployeeDAL.Instance.RemoveEmployee(id);
		}

		public List<EmployeeDTO> GetAllEmployees() => EmployeeDAL.Instance.GetAll();

		public List<EmployeeDTO> GetAllEmployees(int month, int year) => EmployeeDAL.Instance.GetAll(month, year);

		public int GetRoleId(int id)
		{
			AccountDTO account = AccountDAL.Instance.GetAccount(id);

			return account.RoleID;
		}
		#endregion
	}
}
