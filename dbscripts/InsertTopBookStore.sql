USE TopBookStore;

SELECT * FROM Customers;
SELECT * FROM Categories;
SELECT * FROM Books;
SELECT * FROM BookCategory;
SELECT * FROM Authors;

SELECT * FROM AspNetUsers;
SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUserRoles;

DELETE FROM BookCategory;
DELETE FROM Books;
DELETE FROM Receipts;
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
DBCC CHECKIDENT (Receipts, RESEED, 0)
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
INSERT INTO Authors
    (LastName, FirstName, PhoneNumber)
VALUES
    (N'Nguyễn', N'Văn A', '0123456789'),
    (N'Trần', N'Thị B', '0987654321'),
    (N'Lê', N'Quang C', '0912345678');

-- Insert sample publishers
INSERT INTO Publishers
    (Name)
VALUES
    (N'Nhà Xuất Bản Kim Đồng'),
    (N'Nhà Xuất Bản Trẻ'),
    (N'Nhà Xuất Bản Văn Học');

-- Insert sample categories
INSERT INTO Categories
    (Name)
VALUES
    (N'Tiểu Thuyết'),
    (N'Kinh Tế'),
    (N'Khoa Học');

-- Insert sample customers
INSERT INTO Customers
    (LastName, FirstName, Debt)
VALUES
    (N'Nguyễn', N'Thị D', 0),
    (N'Trần', N'Văn E', 1000000),
    (N'Lê', N'Thị F', 500000);

-- Insert sample carts
INSERT INTO Carts
    (Amount, CustomerId)
VALUES
    (250000, 1),
    (100000, 2),
    (500000, 3);

-- Insert sample orders
INSERT INTO Orders
    (OrderDate, Amount, State, CustomerId)
VALUES
    (GETDATE(), 200000, N'awaiting', 1),
    (GETDATE(), 150000, N'paid', 2),
    (GETDATE(), 300000, N'sent', 3);

-- Insert sample receipts
INSERT INTO Receipts
    (Amount, CustomerId)
VALUES
    (500000, 1),
    (200000, 2),
    (100000, 3);

-- Insert sample addresses
INSERT INTO Addresses
    (Street, District, City, Country, CustomerId)
VALUES
    (N'123 Đường Nguyễn Văn A', N'Quận 1', N'Thành phố Hồ Chí Minh', N'Việt Nam', 1),
    (N'456 Đường Trần Thị B', N'Quận 2', N'Thành phố Hồ Chí Minh', N'Việt Nam', 2),
    (N'789 Đường Lê Quang C', N'Quận 3', N'Thành phố Hồ Chí Minh', N'Việt Nam', 3);

-- Insert sample books
INSERT INTO Books
    (Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate, ImageUrl, AuthorId, PublisherId)
VALUES
    (N'Tiếng Gọi Công Việc', N'Một cuốn sách về sự nghiệp và thành công', '9781234567890', 100, 50000, 0.1, 200, GETDATE(), 'book1.jpg', 1, 1),
    (N'Kinh Tế Học Đại Cương', N'Giới thiệu về kinh tế học', '9780987654321', 50, 75000, 0.05, 300, GETDATE(), 'book2.jpg', 2, 2),
    (N'Cuốn Sách Khoa Học', N'Một cuốn sách về khoa học', '9789876543210', 80, 60000, 0.2, 150, GETDATE(), 'book3.jpg', 3, 3);

-- Insert sample order details
INSERT INTO OrderDetails
    (Quantity, BookId, OrderId)
VALUES
    (2, 1, 1),
    (1, 2, 2),
    (3, 3, 3);

-- Insert sample cart items
INSERT INTO CartItems
    (Quantity, CartId, BookId)
VALUES
    (1, 1, 1),
    (2, 2, 2),
    (3, 3, 3);

-- Insert sample book categories
INSERT INTO BookCategory
    (BookId, CategoryId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3);