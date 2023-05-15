
create database WeBank;
use WeBank


create table User_Register(
ID int identity(1,1) primary key,
Username Varchar(100),
Email Varchar(100) NOT NULL,
Passcode Varchar(20) NOT NULL);

select * from User_Register;


create table ACCOUNT_HOLDER_DETAILS(
ID int identity(1,1) primary key,
account_number varchar(255) NOT NULL,
Password Varchar(20),
Fullname varchar(100) NOT NULL,
Fathername varchar(100) NOT NULL,
Adharno bigint NOT NULL,
Email varchar(255) NOT NULL,
Address varchar(500) NOT NULL,
DOB date NOT NULL,
Age int NOT NULL,
accounttype varchar(20) Not null,
Branch varchar(255) Not null,
Registration_Date date NOT NULL,
Phoneno varchar(50) NOT NULL,
CusID int Foreign key References  User_Register(ID));

select * from ACCOUNT_HOLDER_DETAILS

create table Transactions(
ID int identity(1,1) primary key, 
account_number varchar(255) NOT NULL ,
amount float NOT NULL,
Transaction_Date date NOT NULL,
Transaction_Type varchar(255) Not null,
AccID int Foreign key References ACCOUNT_HOLDER_DETAILS(ID));



insert into Transactions(account_number,amount,Transaction_Date,Transaction_Type) values('WEBAC00001',1000,'2000-12-12','Deposit');

select * from Transactions;

create table Fund_Transfer(
ID int identity(1,1) primary key, 
Transaction_id varchar(255) Not null  ,
account_number varchar(255) NOT NUll,
To_account_number varchar(255) Not null,
amount float not null,
TCCID int Foreign key References Transactions(ID));
select * from Fund_Transfer;


 drop TABLE Transactions;
  drop TABLE ACCOUNT_HOLDER_DETAILS; 
  drop TABLE Fund_Transfer;
   drop TABLE User_Register;




create table Admin(AdminId int primary key Identity(1,1) NOT NULL, Email Varchar(100)  Not null,
Passcode Varchar(20) Not NULL);

INSERT INTO Admin VALUES('Admin@gmail.com','Admin@123');

SELECT * FROM Admin;

 drop TABLE Admin;
