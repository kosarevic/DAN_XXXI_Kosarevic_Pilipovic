--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblOrderMenu')
drop table tblOrderMenu

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblOrder')
drop table tblOrder

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblMenu')
drop table tblMenu

create table tblOrder
(
OrderId int primary key IDENTITY(1,1),
Price int
)

create table tblMenu
(
MenuId int primary key IDENTITY(1,1),
Meal varchar(100) not null,
Price int
)

create table tblOrderMenu 
(
OrderMenuId int primary key IDENTITY(1,1),
OrderId int foreign key references tblOrder(OrderId) not null,
MenuId int foreign key references tblMenu(MenuId) not null
)


insert into tblOrder values (300);
insert into tblOrder values (200);

insert into tblMenu values ('Supa', 100);
insert into tblMenu values ('Rostilj', 200);

insert into tblOrderMenu values (1, 1);
insert into tblOrderMenu values (1, 2);
insert into tblOrderMenu values (2, 2);