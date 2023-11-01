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

delete from BookCategory;

delete from Books;
dbcc CHECKIDENT (Books, RESEED, 0)

delete from OrderDetails;
dbcc CHECKIDENT (OrderDetails, RESEED, 0)

delete from CartItems;
dbcc CHECKIDENT (CartItems, RESEED, 0)

delete from Orders;
dbcc CHECKIDENT (Orders, RESEED, 0)

-- delete Customers
delete from Customers
where FirstName != 'Dong';
dbcc CHECKIDENT (Customers, RESEED, 1)

delete from Customers;
dbcc CHECKIDENT (Customers, RESEED, 0)

delete from Carts;
dbcc CHECKIDENT (Carts, RESEED, 0)

delete from Categories;
dbcc CHECKIDENT (Categories, RESEED, 0)

delete from Publishers;
dbcc CHECKIDENT (Publishers, RESEED, 0)

delete from Authors
dbcc CHECKIDENT (Authors, RESEED, 0)

-- delete AspNetUsers
delete from AspNetUsers
where UserName != 'admin@gmail.com';

delete from AspNetUsers;

-- Inserting data into Authors table
insert into Authors
    (FirstName, LastName, PhoneNumber)
values
    (N'Nguyễn', N'Trần', '0987654321'),
    (N'Lê', N'Phạm', '0912345678'),
    (N'Trần', N'Nguyễn', null);

-- Inserting data into Categories table
insert into Categories
    (Name)
values
    (N'Khoa học'),
    (N'Tiểu thuyết'),
    (N'Lịch sử');

-- Inserting data into Publishers table
insert into Publishers
    (Name)
values
    (N'Nhà xuất bản Kim Đồng'),
    (N'Nhà xuất bản Trẻ'),
    (N'Nhà xuất bản Văn học');

-- Inserting data into Books table
insert into Books
    (AuthorId, PublisherId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate)
values
    (1, 1, N'Cuộc sống trong tương lai', N'Cuốn sách kể về cuộc sống trong tương lai đầy thú vị.', '9781234567890', 50, 100000, 0.1, 200, '2022-01-15'),
    (2, 2, N'Ngọn đồi hoa hồng', N'Một câu chuyện tình lãng mạn đầy xúc động.', '9780987654321', 30, 80000, 0.2, 150, '2021-09-20'),
    (3, 3, N'Việt Nam trong những trang sách', N'Tổng quan về lịch sử Việt Nam qua các tác phẩm văn học nổi tiếng.', '9780123456789', 20, 120000, 0.15, 250, '2023-03-10');

-- Inserting data into BookCategory table
insert into BookCategory
    (CategoryId, BookId)
values
    (1, 1),
    (2, 1),
    (2, 2),
    (3, 3);