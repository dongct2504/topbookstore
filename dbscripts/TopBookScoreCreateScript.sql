use master;

if db_id('TopBookStore') is not null
   alter database TopBookStore set SINGLE_USER with rollback IMMEDIATE
   drop database TopBookStore;
go

create database TopBookStore;
go

use TopBookStore;


if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('BookCategory') and o.name = 'FK_BOOKCATE_BOOKCATEG_CATEGORI')
alter table BookCategory
   drop constraint FK_BOOKCATE_BOOKCATEG_CATEGORI
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('BookCategory') and o.name = 'FK_BOOKCATE_BOOKCATEG_BOOKS')
alter table BookCategory
   drop constraint FK_BOOKCATE_BOOKCATEG_BOOKS
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('Books') and o.name = 'FK_BOOKS_BOOKAUTHO_AUTHORS')
alter table Books
   drop constraint FK_BOOKS_BOOKAUTHO_AUTHORS
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('Books') and o.name = 'FK_BOOKS_BOOKPUBLI_PUBLISHE')
alter table Books
   drop constraint FK_BOOKS_BOOKPUBLI_PUBLISHE
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('CartItems') and o.name = 'FK_CARTITEM_CARTITEMB_BOOKS')
alter table CartItems
   drop constraint FK_CARTITEM_CARTITEMB_BOOKS
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('CartItems') and o.name = 'FK_CARTITEM_CARTITEMC_CARTS')
alter table CartItems
   drop constraint FK_CARTITEM_CARTITEMC_CARTS
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('Carts') and o.name = 'FK_CARTS_CARTCUSTO_CUSTOMER')
alter table Carts
   drop constraint FK_CARTS_CARTCUSTO_CUSTOMER
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('OrderDetails') and o.name = 'FK_ORDERDET_ORDERDETA_BOOKS')
alter table OrderDetails
   drop constraint FK_ORDERDET_ORDERDETA_BOOKS
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('OrderDetails') and o.name = 'FK_ORDERDET_ORDERDETA_ORDERS')
alter table OrderDetails
   drop constraint FK_ORDERDET_ORDERDETA_ORDERS
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('Orders') and o.name = 'FK_ORDERS_ORDERCUST_CUSTOMER')
alter table Orders
   drop constraint FK_ORDERS_ORDERCUST_CUSTOMER
go

if exists (select 1
from sysobjects
where  id = object_id('Authors')
   and type = 'U')
   drop table Authors
go

if exists (select 1
from sysindexes
where  id    = object_id('BookCategory')
   and name  = 'BOOKCATEGORY2_FK'
   and indid > 0
   and indid < 255)
   drop index BookCategory.BOOKCATEGORY2_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('BookCategory')
   and name  = 'BOOKCATEGORY_FK'
   and indid > 0
   and indid < 255)
   drop index BookCategory.BOOKCATEGORY_FK
go

if exists (select 1
from sysobjects
where  id = object_id('BookCategory')
   and type = 'U')
   drop table BookCategory
go

if exists (select 1
from sysindexes
where  id    = object_id('Books')
   and name  = 'BOOKPUBLISHERS_FK'
   and indid > 0
   and indid < 255)
   drop index Books.BOOKPUBLISHERS_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('Books')
   and name  = 'BOOKAUTHORS_FK'
   and indid > 0
   and indid < 255)
   drop index Books.BOOKAUTHORS_FK
go

if exists (select 1
from sysobjects
where  id = object_id('Books')
   and type = 'U')
   drop table Books
go

if exists (select 1
from sysindexes
where  id    = object_id('CartItems')
   and name  = 'CARTITEMBOOK_FK'
   and indid > 0
   and indid < 255)
   drop index CartItems.CARTITEMBOOK_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('CartItems')
   and name  = 'CARTITEMCARTS_FK'
   and indid > 0
   and indid < 255)
   drop index CartItems.CARTITEMCARTS_FK
go

if exists (select 1
from sysobjects
where  id = object_id('CartItems')
   and type = 'U')
   drop table CartItems
go

if exists (select 1
from sysindexes
where  id    = object_id('Carts')
   and name  = 'CARTCUSTOMERS_FK'
   and indid > 0
   and indid < 255)
   drop index Carts.CARTCUSTOMERS_FK
go

if exists (select 1
from sysobjects
where  id = object_id('Carts')
   and type = 'U')
   drop table Carts
go

if exists (select 1
from sysobjects
where  id = object_id('Categories')
   and type = 'U')
   drop table Categories
go

if exists (select 1
from sysobjects
where  id = object_id('Customers')
   and type = 'U')
   drop table Customers
go

if exists (select 1
from sysindexes
where  id    = object_id('OrderDetails')
   and name  = 'ORDERDETAILBOOK_FK'
   and indid > 0
   and indid < 255)
   drop index OrderDetails.ORDERDETAILBOOK_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('OrderDetails')
   and name  = 'ORDERDETAILORDERS_FK'
   and indid > 0
   and indid < 255)
   drop index OrderDetails.ORDERDETAILORDERS_FK
go

if exists (select 1
from sysobjects
where  id = object_id('OrderDetails')
   and type = 'U')
   drop table OrderDetails
go

if exists (select 1
from sysindexes
where  id    = object_id('Orders')
   and name  = 'ORDERCUSTOMERS_FK'
   and indid > 0
   and indid < 255)
   drop index Orders.ORDERCUSTOMERS_FK
go

if exists (select 1
from sysobjects
where  id = object_id('Orders')
   and type = 'U')
   drop table Orders
go

if exists (select 1
from sysobjects
where  id = object_id('Publishers')
   and type = 'U')
   drop table Publishers
go

/*==============================================================*/
/* Table: Authors                                               */
/*==============================================================*/
create table Authors
(
   AuthorId int identity,
   FirstName nvarchar(50) not null,
   LastName nvarchar(50) not null,
   PhoneNumber char(15) null,
   constraint PK_AUTHORS primary key (AuthorId)
)
go

/*==============================================================*/
/* Table: Categories                                            */
/*==============================================================*/
create table Categories
(
   CategoryId int identity,
   Name nvarchar(128) not null,
   constraint PK_CATEGORIES primary key (CategoryId)
)
go

/*==============================================================*/
/* Table: Publishers                                            */
/*==============================================================*/
create table Publishers
(
   PublisherId int identity,
   Name nvarchar(128) not null,
   constraint PK_PUBLISHERS primary key (PublisherId)
)
go

/*==============================================================*/
/* Table: Books                                                 */
/*==============================================================*/
create table Books
(
   BookId int identity,
   AuthorId int not null,
   PublisherId int not null,
   Title nvarchar(80) not null,
   Description nvarchar(max) not null,
   Isbn13 char(13) not null,
   Inventory int not null,
   Price money not null,
   DiscountPercent decimal(3,2) not null,
   NumberOfPages int not null,
   PublicationDate datetime not null,
   ImageUrl nvarchar(max) null,
   constraint PK_BOOKS primary key (BookId),
   constraint FK_BOOKS_BOOKAUTHO_AUTHORS foreign key (AuthorId)
      references Authors (AuthorId)
         on delete cascade,
   constraint FK_BOOKS_BOOKPUBLI_PUBLISHE foreign key (PublisherId)
      references Publishers (PublisherId)
         on delete cascade
)
go

/*==============================================================*/
/* Table: BookCategory                                          */
/*==============================================================*/
create table BookCategory
(
   CategoryId int not null,
   BookId int not null,
   constraint PK_BOOKCATEGORY primary key (CategoryId, BookId),
   constraint FK_BOOKCATE_BOOKCATEG_CATEGORI foreign key (CategoryId)
      references Categories (CategoryId)
         on delete cascade,
   constraint FK_BOOKCATE_BOOKCATEG_BOOKS foreign key (BookId)
      references Books (BookId)
         on delete cascade
)
go

/*==============================================================*/
/* Index: BOOKCATEGORY_FK                                       */
/*==============================================================*/




create nonclustered index BOOKCATEGORY_FK on BookCategory (CategoryId asc)
go

/*==============================================================*/
/* Index: BOOKCATEGORY2_FK                                      */
/*==============================================================*/




create nonclustered index BOOKCATEGORY2_FK on BookCategory (BookId asc)
go

/*==============================================================*/
/* Index: BOOKAUTHORS_FK                                        */
/*==============================================================*/




create nonclustered index BOOKAUTHORS_FK on Books (AuthorId asc)
go

/*==============================================================*/
/* Index: BOOKPUBLISHERS_FK                                     */
/*==============================================================*/




create nonclustered index BOOKPUBLISHERS_FK on Books (PublisherId asc)
go

/*==============================================================*/
/* Table: Customers                                             */
/*==============================================================*/
create table Customers
(
   CustomerId int identity,
   FirstName nvarchar(50) not null,
   LastName nvarchar(50) not null,
   Debt money not null,
   Address nvarchar(128) null,
   Ward nvarchar(50) null,
   District nvarchar(30) null,
   City nvarchar(30) null,
   constraint PK_CUSTOMERS primary key (CustomerId)
)
go

/*==============================================================*/
/* Table: Carts                                                 */
/*==============================================================*/
create table Carts
(
   CartId int identity,
   CustomerId int not null,
   TotalAmount money not null,
   constraint PK_CARTS primary key (CartId),
   constraint FK_CARTS_CARTCUSTO_CUSTOMER foreign key (CustomerId)
      references Customers (CustomerId)
         on delete cascade
)
go

/*==============================================================*/
/* Table: CartItems                                             */
/*==============================================================*/
create table CartItems
(
   CartItemId int identity,
   CartId int not null,
   BookId int not null,
   Price money not null,
   Quantity int not null,
   constraint PK_CARTITEMS primary key (CartItemId),
   constraint FK_CARTITEM_CARTITEMC_CARTS foreign key (CartId)
      references Carts (CartId)
         on delete cascade,
   constraint FK_CARTITEM_CARTITEMB_BOOKS foreign key (BookId)
      references Books (BookId)
         on delete cascade
)
go

/*==============================================================*/
/* Index: CARTITEMCARTS_FK                                      */
/*==============================================================*/




create nonclustered index CARTITEMCARTS_FK on CartItems (CartId asc)
go

/*==============================================================*/
/* Index: CARTITEMBOOK_FK                                       */
/*==============================================================*/




create nonclustered index CARTITEMBOOK_FK on CartItems (BookId asc)
go

/*==============================================================*/
/* Index: CARTCUSTOMERS_FK                                      */
/*==============================================================*/




create nonclustered index CARTCUSTOMERS_FK on Carts (CustomerId asc)
go

/*==============================================================*/
/* Table: Orders                                                */
/*==============================================================*/
create table Orders
(
   OrderId int identity,
   CustomerId int not null,
   OrderDate datetime not null,
   ShippingDate datetime not null,
   Name nvarchar(128) not null,
   PhoneNumber char(15) not null,
   TotalAmount money not null,
   TrackingNumber varchar(128) null,
   Carrier varchar(128) null,
   OrderStatus varchar(20) null,
   PaymentStatus varchar(20) null,
   TransactionId varchar(256) null,
   Address nvarchar(128) null,
   Ward nvarchar(50) null,
   District nvarchar(30) null,
   City nvarchar(30) null,
   constraint PK_ORDERS primary key (OrderId),
   constraint FK_ORDERS_ORDERCUST_CUSTOMER foreign key (CustomerId)
      references Customers (CustomerId)
         on delete cascade
)
go

/*==============================================================*/
/* Table: OrderDetails                                          */
/*==============================================================*/
create table OrderDetails
(
   OrderDetailId int identity,
   BookId int not null,
   OrderId int not null,
   Price money not null,
   Quantity int not null,
   constraint PK_ORDERDETAILS primary key (OrderDetailId),
   constraint FK_ORDERDET_ORDERDETA_ORDERS foreign key (OrderId)
      references Orders (OrderId)
         on delete cascade,
   constraint FK_ORDERDET_ORDERDETA_BOOKS foreign key (BookId)
      references Books (BookId)
         on delete cascade
)
go

/*==============================================================*/
/* Index: ORDERDETAILORDERS_FK                                  */
/*==============================================================*/




create nonclustered index ORDERDETAILORDERS_FK on OrderDetails (OrderId asc)
go

/*==============================================================*/
/* Index: ORDERDETAILBOOK_FK                                    */
/*==============================================================*/




create nonclustered index ORDERDETAILBOOK_FK on OrderDetails (BookId asc)
go

/*==============================================================*/
/* Index: ORDERCUSTOMERS_FK                                     */
/*==============================================================*/




create nonclustered index ORDERCUSTOMERS_FK on Orders (CustomerId asc)
go

