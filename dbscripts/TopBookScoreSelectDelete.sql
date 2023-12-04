use TopBookStore;

-- select statement for TopBookStore tables
select *
from Customers;

select *
from Categories;

select *
from Books;

select *
from BookCategory;

select *
from Authors;

select *
from Carts;

select *
from CartItems;

select *
from OrderDetails;

select *
from Orders;

-- select statement for TopBookStore Identity tables
select *
from AspNetUsers;

select *
from AspNetRoles;

select *
from AspNetUserRoles;


-- ********** Delete zone ********** --

delete from BookCategory;

delete from Books;
dbcc CHECKIDENT (Books, RESEED, 0);

delete from OrderDetails;
dbcc CHECKIDENT (OrderDetails, RESEED, 0);

delete from CartItems;
dbcc CHECKIDENT (CartItems, RESEED, 0);

delete from Orders;
dbcc CHECKIDENT (Orders, RESEED, 0);

-- delete Customers
delete from Customers
where FirstName not in ('admin1', 'admin2', 'admin3')
dbcc CHECKIDENT (Customers, RESEED, 1);

delete from Customers;
dbcc CHECKIDENT (Customers, RESEED, 0);

delete from Carts;
dbcc CHECKIDENT (Carts, RESEED, 0);

delete from Categories;
dbcc CHECKIDENT (Categories, RESEED, 0);

delete from Publishers;
dbcc CHECKIDENT (Publishers, RESEED, 0);

delete from Authors;
dbcc CHECKIDENT (Authors, RESEED, 0);

-- delete AspNetUsers
delete from AspNetUsers
where UserName not in ('admin1@gmail.com', 'admin2@gmail.com', 'amin3@gmail.com');

delete from AspNetUsers;

-- ********** Delete zone ********** --
