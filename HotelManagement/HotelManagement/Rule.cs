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
			EMPLOYEE,
		}

		public class SALARY
		{
			public const decimal ADMIN_SALARY = 0M;
			public const decimal MANAGER_SALARY = 20_000_000M;
			public const decimal EMPLOYEE_SALARY = 10_000_000M;
		}

		public enum AGE
		{
			CUSTOMER = 15,
			EMPLOYEE = 18
		}

		public class BUTTON
		{
			public const string NORMAL = "#7F000000";

			public const string HIGHLIGHT = "#C379E2";

            public const string BORDER = "#F3E5FF";
        }

		public enum ROOM_STATE
		{
			AVAILABLE = 0,
			RENTING,
			FIXING
		}
	}
}
