Create Database HotelManagement;
Use HotelManagement;
Drop Database HotelManagement;
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
	ID int not null IDENTITY(1,1),
	CustomerID int,
	UserName nvarchar(50),
	RoomID int,
	CreateDate Date,
	CheckinDate Date,
	TotalDay int,
	CheckoutDate Date,
	Total money,
	IsPaid bit,
)
ALTER TABLE RENTING
ADD CONSTRAINT PK_RENTING PRIMARY KEY (ID);

Drop Table BOOKING
Create Table BOOKING
(
	ID int not null IDENTITY(1,1),
	CustomerID int,
	UserName nvarchar(50),
	RoomTypeID int,
	CreateDate Date,
	CheckinDate Date,
	TotalDay int,
	Total money,
	IsRented bit,
)
ALTER TABLE BOOKING
ADD CONSTRAINT PK_BOOKING PRIMARY KEY (ID);

Drop table BILL
Create Table BILL
(
	ID int not null IDENTITY(1,1),
	BillDate Date,
	UserName nvarchar(50),
	CustomerID int,
	RentingID int,
	TotalDay int,
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
	ID int not null IDENTITY(1,1),
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

--- EMPLOYEEE ----
delete from EMPLOYEE where BirthDay = '2022-12-12'
Select * from EMPLOYEE 
insert into EMPLOYEE values(123321123321, 'Nguyen Van Thien', '0321222111', 0, '2022-12-12', '2022-12-12', 20000000)
insert into EMPLOYEE values(123321123322, 'Tran Anh Dung', '0321222112', 0, '2022-12-12', '2022-12-12', 20000000)
insert into EMPLOYEE values(123321123323, 'Nguyen Ngoc Duc', '0321222113', 0, '2022-12-12', '2022-12-12', 20000000)
insert into EMPLOYEE values(123321123324, 'Tran Quang Dat', '0321222114', 0, '2022-12-12', '2022-12-12', 20000000)

--- ACCOUNT ---
Delete from ACCOUNT where RoleID = 2;
Select * from ACCOUNT
Insert into ACCOUNT Values('Admin', '123', null, 1, 1, 0);
Insert into ACCOUNT Values('thien', '123456', null, 1, 2, (Select ID from EMPLOYEE Where CitizenID = 123321123321));
Insert into ACCOUNT Values('dung', '123456', null, 1, 2, (Select ID from EMPLOYEE Where CitizenID = 123321123322));
Insert into ACCOUNT Values('duc', '123456', null, 1, 2, (Select ID from EMPLOYEE Where CitizenID = 123321123323));
Insert into ACCOUNT Values('Dat', '123456', null, 1, 2, (Select ID from EMPLOYEE Where CitizenID = 123321123324));

---------------

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

--- BOOKING ---
Delete from BOOKING
Select * from BOOKING
---------------

--- RENTING ---
Delete From RENTING
Select Distinct(CustomerID) from RENTING
Select * from Renting;
---------------



--- BILL ---
Select * from BILL
---------------


--- VOUCHER_TYPE ---
Select * from VOUCHER_TYPE
Insert into VOUCHER_TYPE Values(1, 'DEFAULT', '10');
Insert into VOUCHER_TYPE Values(2, 'SORRY', '20');
Insert into VOUCHER_TYPE Values(3, 'VIP', '50');
Select MAX(ID) from VOUCHER_TYPE

---------------

--- VOUCHER ---
Select * from VOUCHER


---------------