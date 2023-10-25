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

delete from Customers;
dbcc CHECKIDENT (Customers, RESEED, 0)

delete from Customers
where FirstName != 'Dong';
dbcc CHECKIDENT (Customers, RESEED, 1)

delete from Carts;
dbcc CHECKIDENT (Carts, RESEED, 0)

delete from Categories;
dbcc CHECKIDENT (Categories, RESEED, 0)

delete from Publishers;
dbcc CHECKIDENT (Publishers, RESEED, 0)

delete from Authors
dbcc CHECKIDENT (Authors, RESEED, 0)

delete from AspNetUsers;

delete from AspNetUsers
where UserName != 'admin@gmail.com';

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

-- Inserting data into Customers table
insert into Customers
    (FirstName, LastName, Debt, Street, District, City, Country)
values
    (N'Trần', N'Hoàng', 0, '123 Đường A', 'Quận B', 'Thành phố C', 'Việt Nam'),
    (N'Lê', N'Thịnh', 50000, '456 Đường X', 'Quận Y', 'Thành phố Z', 'Việt Nam');

-- Inserting data into Carts table
insert into Carts
    (CustomerId, TotalAmount)
values
    (1, 200000),
    (2, 150000);

-- Inserting data into CartItems table
insert into CartItems
    (CartId, BookId, Price, Quantity)
values
    (1, 1, 100000, 2),
    (1, 2, 80000, 1),
    (2, 3, 120000, 1);

-- Inserting data into Orders table
insert into Orders
    (CustomerId, OrderDate, ShippingDate, Name, PhoneNumber, TotalAmount, TrackingNumber, Carrier, OrderStatus, PaymentStatus, TransactionId, Street, District, City, Country)
values
    (1, '2023-10-15', '2023-10-16', N'Nguyễn Văn A', '0912345678', 200000, '123456789', 'Vietnam Post', 'Shipped', 'Paid', '1234567890', '789 Đường M', 'Quận N', 'Thành phố P', 'Việt Nam'),
    (2, '2023-10-16', '2023-10-17', null, '0987654321', 150000, '987654321', 'Viettel Post', 'Processing', 'Pending', '0987654321', '321 Đường Q', 'Quận R', 'Thành phố S', 'Việt Nam');

-- Inserting data into OrderDetails table
insert into OrderDetails
    (BookId, OrderId, Price, Quantity)
values
    (1, 1, 100000, 2),
    (2, 1, 80000, 1),
    (2, 2, 80000, 1),
    (3, 2, 120000, 1);