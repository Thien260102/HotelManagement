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


--- Data to test ---


Insert into ROLE Values(1, 'Admin');
Insert into ROLE Values(2, 'Manager');
Insert into ROLE Values(3, 'Employee');


Select * from ACCOUNT
Insert into ACCOUNT Values('admin', '123', null, 1, 1, 0);
delete from ACCOUNT

Select * from EMPLOYEE where CitizenID = '123'
delete from EMPLOYEE 
insert into EMPLOYEE values(123, 'thien', 123, 0, '2022-12-12', '2022-12-12', 0)

Select * from ROLE

