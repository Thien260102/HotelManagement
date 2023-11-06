Create Database HotelManagement;
Use HotelManagement;

Drop table ACCOUNT;
Create Table ACCOUNT
(
	UserName nvarchar(50) not null,
	PassWord nvarchar(50),
	Email nvarchar(max),
	IsAvailable bit, --- 1: available, 0: unavailable
	RoleID int,
	EmployeeID int,
);
ALTER TABLE ACCOUNT
ADD CONSTRAINT PK_ACCOUNT PRIMARY KEY (UserName);

Drop table ROLE;
Create Table ROLE
(
	ID int not null,
	RoleName nvarchar(50),
);
ALTER TABLE ROLE
ADD CONSTRAINT PK_ROLE PRIMARY KEY (ID);

Drop table EMPLOYEE;
Create Table EMPLOYEE
(
	ID int not null IDENTITY(1,1),
	CitizenID nvarchar(50) not null,
	FullName nvarchar(100),
	PhoneNumber nvarchar(30),
	Sex bit, --- 1: male, 0: female
	BirthDay Date, --- YYYY-MM-DD
	StartDay Date,
	Salary decimal,
);
ALTER TABLE EMPLOYEE
ADD CONSTRAINT PK_EMPLOYEE PRIMARY KEY (ID, CitizenID);

Drop table ATTENDANCE;
Create Table ATTENDANCE
(
	ID int not null IDENTITY(1,1),
	EmployeeID int not null,
	Date Date,
	State int,
	Note nvarchar(max)
);
ALTER TABLE ATTENDANCE
ADD CONSTRAINT PK_ATTENDANCE PRIMARY KEY (ID);

Drop table CUSTOMER;
Create Table CUSTOMER
(
	ID int not null IDENTITY(1,1),
	CitizenID nvarchar(50) not null, --- or Passport ID
	FullName nvarchar(100),
	PhoneNumber nvarchar(30),
	Sex bit, --- 1: male, 0: female
	BirthDay Date, --- YYYY-MM-DD
	Nationality nvarchar(100),
);
ALTER TABLE CUSTOMER
ADD CONSTRAINT PK_CUSTOMER PRIMARY KEY (ID, CitizenID);

Drop table ROOM_TYPE;
Create Table ROOM_TYPE
(
	ID int not null,
	Name nvarchar(50),
	HighestNumberPeople int,
	Price money,
)
ALTER TABLE ROOM_TYPE
ADD CONSTRAINT PK_ROOM_TYPE PRIMARY KEY (ID);

Drop table ROOM;
Create Table ROOM
(
	ID int not null IDENTITY(1,1),
	Name nvarchar(50),
	FloorNumber int,
	State int,
	TypeID int,
	Note nvarchar(max)
)
ALTER TABLE ROOM
ADD CONSTRAINT PK_ROOM PRIMARY KEY (ID);

Drop table RENTING;
Create Table RENTING
(
	ID int not null,
	CustomerID int,
	UserName nvarchar(50),
	RoomID int,
	StartDate Date,
	EndDate Date,
	TotalDayStay int,
)
ALTER TABLE RENTING
ADD CONSTRAINT PK_RENTING PRIMARY KEY (ID);

Drop Table BOOKING
Create Table BOOKING
(
	ID int not null,
	CustomerID int,
	UserName nvarchar(50),
	RoomTypeID int,
	CreateDate Date,
	CheckinDate Date,
	TotalDay int,
	Total money,
)
ALTER TABLE BOOKING
ADD CONSTRAINT PK_BOOKING PRIMARY KEY (ID);

Drop table BILL
Create Table BILL
(
	ID int not null,
	BillDate Date,
	UserName nvarchar(50),
	Total money
)
ALTER TABLE BILL
ADD CONSTRAINT PK_BILL PRIMARY KEY (ID);


Drop table VOUCHER_TYPE
Create Table VOUCHER_TYPE
(
	ID int not null,
	Name nvarchar(50),
	Ratio int,
)
ALTER TABLE VOUCHER_TYPE
ADD CONSTRAINT PK_VOUCHER_TYPE PRIMARY KEY (ID);

Drop table VOUCHER
Create Table VOUCHER
(
	ID int not null,
	CustomerID int,
	ExpirationDate Date,
	IsAvailable bit,
	TypeID int
)
ALTER TABLE VOUCHER
ADD CONSTRAINT PK_VOUCHER PRIMARY KEY (ID);


--- Data to test ---

--- ROLE ---
Delete from ROLE;
Insert into ROLE Values(1, 'Admin');
Insert into ROLE Values(2, 'Manager');
Insert into ROLE Values(3, 'Employee');
Select * from ROLE

---------------

--- ACCOUNT ---
Delete from ACCOUNT where UserName != 'admin';
Select * from ACCOUNT
Insert into ACCOUNT Values('admin', '123', null, 1, 1, 0);
---------------

--- EMPLOYEEE ----
delete from EMPLOYEE 
Select * from EMPLOYEE where CitizenID = '123'
insert into EMPLOYEE values(123, 'thien', 123, 0, '2022-12-12', '2022-12-12', 0)

UPDATE EMPLOYEE
SET Sex = 1, FullName = 'Tien'
WHERE CitizenID = 12345;

------------------

--- ATTENDANCE ----
delete from ATTENDANCE where ID = 11

Select * from ATTENDANCE where Date = '2002-10-10';
------------------

--- ROOM_TYPE ---
Delete from ROOM_TYPE
Insert into ROOM_TYPE Values(1, 'SMALL', 2, 200000);
Insert into ROOM_TYPE Values(2, 'MEDIUM', 5, 400000);
Insert into ROOM_TYPE Values(3, 'LARGE', 7, 500000);
Insert into ROOM_TYPE Values(4, 'VIP', 2, 1000000);
Select * from ROOM_TYPE

-----------------

--- ROOM ---
Delete from ROOM 
Insert into ROOM Values('1', 1, 0, 1, 'TV');	

Select * from ROOM
------------------

--- CUSTOMER ---
Delete from CUSTOMER

Insert into CUSTOMER VALUES('123456789011', 'Nguyen Van Thien', '0326933046', 1, '2002-01-26', 'Vietnamese');

Select * from CUSTOMER

---------------


--- RENTING ---
Select * from RENTING
---------------

--- BOOKING ---
Select * from BOOKING
---------------


--- BILL ---
Select * from BILL
---------------


--- VOUCHER_TYPE ---
Select * from VOUCHER_TYPE
---------------

--- VOUCHER ---
Select * from VOUCHER
---------------