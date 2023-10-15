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
DBCC CHECKIDENT (CartItems, RESEED, 0)
DBCC CHECKIDENT (Orders, RESEED, 0)
DBCC CHECKIDENT (Customers, RESEED, 0)
DBCC CHECKIDENT (Carts, RESEED, 0)
DBCC CHECKIDENT (Categories, RESEED, 0)
DBCC CHECKIDENT (Publishers, RESEED, 0)
DBCC CHECKIDENT (Authors, RESEED, 0)

-- Insert sample Authors
INSERT INTO Authors
    (LastName, FirstName, PhoneNumber)
VALUES
    (N'Nguyễn', N'Văn A', '0901234567'),
    (N'Trần', N'Thị B', '0912345678'),
    (N'Lê', N'Đức C', '0923456789');

-- Insert sample Publishers
INSERT INTO Publishers
    (Name)
VALUES
    (N'Nhà Xuất Bản A'),
    (N'Nhà Xuất Bản B'),
    (N'Nhà Xuất Bản C');

-- Insert sample Categories
INSERT INTO Categories
    (Name)
VALUES
    (N'Tiểu Thuyết'),
    (N'Truyện Ngắn'),
    (N'Sách Kỹ Năng');

-- Insert sample Customers
INSERT INTO Customers
    (LastName, FirstName, Email, PhoneNumber, Debt, Street, District, City, Country)
VALUES
    (N'Trần', N'Thị D', 'trand@gmail.com', '0987654321', 0, N'123 Đường A', N'Quận 1', N'TPHCM', N'Việt Nam'),
    (N'Nguyễn', N'Huỳnh E', 'nguyenhe@gmail.com', '0976543210', 0, N'456 Đường B', N'Quận 2', N'TPHCM', N'Việt Nam'),
    (N'Lê', N'Trúc F', 'letruc@gmail.com', '0965432109', 0, N'789 Đường C', N'Quận 3', N'TPHCM', N'Việt Nam');

-- Insert sample Carts
INSERT INTO Carts
    (Amount, CustomerId)
VALUES
    (100000, 1),
    (200000, 2),
    (300000, 3);

-- Insert sample Orders
INSERT INTO Orders
    (OrderDate, Amount, State, CustomerId)
VALUES
    ('2023-10-01', 150000, 'paid', 1),
    ('2023-10-02', 250000, 'sent', 2),
    ('2023-10-03', 350000, 'awaiting', 3);

-- Insert sample Receipts
INSERT INTO Receipts
    (Amount, CustomerId)
VALUES
    (50000, 1),
    (70000, 2),
    (90000, 3);

-- Insert sample Books
INSERT INTO Books
    (Title, Description, Isbn13, Inventory, Price, DiscountPercent,
    NumberOfPages, PublicationDate, AuthorId, PublisherId)
VALUES
    (N'Sách Tiểu Thuyết 1', N'Mô tả sách tiểu thuyết 1', '9781234567890', 10, 150000, 0.1,
        200, '2022-01-01', 1, 1),
    (N'Sách Tiểu Thuyết 2', N'Mô tả sách tiểu thuyết 2', '9780987654321', 5, 200000, 0.2,
        250, '2022-05-01', 2, 2),
    (N'Sách Kỹ Năng 1', N'Mô tả sách kỹ năng 1', '9789876543210', 8, 180000, 0.15,
        180, '2022-01-04', 3, 3);

-- Insert sample OrderDetails
INSERT INTO OrderDetails
    (Quantity, BookId, OrderId)
VALUES
    (2, 1, 1),
    (1, 2, 1),
    (3, 3, 2),
    (2, 1, 3);

-- Insert sample CartItems
INSERT INTO CartItems
    (Quantity, CartId, BookId)
VALUES
    (1, 1, 1),
    (2, 1, 2),
    (3, 2, 3),
    (1, 3, 1);

-- Insert sample BookCategory
INSERT INTO BookCategory
    (BookId, CategoryId)
VALUES
    (1, 1),
    (2, 1),
    (2, 2),
    (3, 3);