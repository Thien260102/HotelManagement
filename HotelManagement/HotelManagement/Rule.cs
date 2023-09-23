﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
	public class Rule
	{
		public enum SEX
		{
			FEMALE = 0,
			MALE
		}

		public enum STATE
		{
			SUCCESS = 0,
			FAIL,
			EXIST
		}

		public enum ROLE
		{
			ADMIN = 1,
			MANAGER,
			EMPLOYEE
		}

		public class SALARY
		{
			public const decimal MANAGER_SALARY = 20_000_000M;
			public const decimal EMPLOYEE_SALARY = 10_000_000M;
		}
	}
}
