USE TopBookStore;

SELECT * FROM Customers;
SELECT * FROM Categories;
SELECT * FROM Books;
SELECT * FROM BookCategory;
SELECT * FROM Authors;
SELECT * FROM Addresses;

SELECT * FROM AspNetUsers;
SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUserRoles;

DELETE FROM BookCategory;
DELETE FROM Books;
DELETE FROM OrderDetails;
DELETE FROM Addresses;
DELETE FROM CartItems;
DELETE FROM Orders;
DELETE FROM Customers;
DELETE FROM Carts;
DELETE FROM Categories;
DELETE FROM Publishers;
DELETE FROM Authors

DELETE FROM AspNetUsers;

DBCC CHECKIDENT (Books, RESEED, 0)
DBCC CHECKIDENT (OrderDetails, RESEED, 0)
DBCC CHECKIDENT (Addresses, RESEED, 0)
DBCC CHECKIDENT (CartItems, RESEED, 0)
DBCC CHECKIDENT (Orders, RESEED, 0)
DBCC CHECKIDENT (Customers, RESEED, 0)
DBCC CHECKIDENT (Carts, RESEED, 0)
DBCC CHECKIDENT (Categories, RESEED, 0)
DBCC CHECKIDENT (Publishers, RESEED, 0)
DBCC CHECKIDENT (Authors, RESEED, 0)

-- Insert sample authors
INSERT INTO Authors (FirstName, LastName, PhoneNumber)
VALUES
    (N'Nguyễn', N'Thị Hương', '0901234567'),
    (N'Trần', N'Văn Nam', '0912345678'),
    (N'Lê', N'Thị Thu', '0987654321');

-- Insert sample publishers
INSERT INTO Publishers (Name)
VALUES
    (N'Nhà xuất bản Kim Đồng'),
    (N'Nhà xuất bản Trẻ'),
    (N'Nhà xuất bản Văn Học');

-- Insert sample categories
INSERT INTO Categories (Name)
VALUES
    (N'Tiểu thuyết'),
    (N'Sách thiếu nhi'),
    (N'Sách kỹ năng sống');

-- Insert sample customers
INSERT INTO Customers (FirstName, LastName, Debt)
VALUES
    (N'Nguyễn', N'Thị Hương', 150000),
    (N'Trần', N'Văn Nam', 0),
    (N'Lê', N'Thị Thu', 50000);

-- Insert sample carts
INSERT INTO Carts (TotalAmount, CustomerId)
VALUES
    (250000, 1),
    (100000, 2),
    (50000, 3);

-- Insert sample orders
INSERT INTO Orders (OrderDate, TotalAmount, State, CustomerId)
VALUES
    ('2023-10-15 09:30:00', 250000, 'paid', 1),
    ('2023-10-16 14:15:00', 100000, 'awaiting', 2),
    ('2023-10-17 11:45:00', 50000, 'sent', 3);

-- Insert sample addresses
INSERT INTO Addresses (Street, District, City, Country, CustomerId)
VALUES
    (N'123 Đường A', N'Quận 1', N'Thành phố Hồ Chí Minh', N'Việt Nam', 1),
    (N'456 Đường B', N'Quận 2', N'Thành phố Hồ Chí Minh', N'Việt Nam', 2),
    (N'789 Đường C', N'Quận 3', N'Thành phố Hà Nội', N'Việt Nam', 3);

-- Insert sample books
INSERT INTO Books (Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages,
    PublicationDate, AuthorId, PublisherId)
VALUES
    (N'Sóng', N'Một cuốn tiểu thuyết tình cảm', '9781234567890', 100, 120000, 0.1, 250,
        '2023-01-01', 1, 1),
    (N'Con rồng cháu tiên', N'Truyện dành cho trẻ em', '9780987654321', 50, 80000, 0, 80,
        '2022-05-01', 2, 2),
    (N'Thành công từ những nguyên tắc đơn giản', N'Sách hướng dẫn kỹ năng sống', '9789876543210', 80, 150000, 0.2, 200,
        '2023-04-15', 3, 3);

-- Insert sample order details
INSERT INTO OrderDetails (Price, Quantity, BookId, OrderId)
VALUES
    (120000, 1, 1, 1),
    (80000, 2, 2, 2),
    (150000, 1, 3, 3);

-- Insert sample cart items
INSERT INTO CartItems (Price, Quantity, CartId, BookId)
VALUES
    (120000, 1, 1, 1),
    (80000, 2, 2, 2),
    (150000, 1, 3, 3);

-- Insert sample book categories
INSERT INTO BookCategory (BookId, CategoryId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3);