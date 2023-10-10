USE TopBookStore;

SELECT * FROM Customers;
SELECT * FROM Categories;

DELETE FROM BookCategories;
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

-- Insert data into Authors table
INSERT INTO Authors (FirstName, LastName, PhoneNumber)
VALUES
    (N'Nguyễn', N'Nhật Ánh', '0987654321'),
    (N'Nguyễn', N'Du', '0123456789');

-- Insert data into Publishers table
INSERT INTO Publishers (Name)
VALUES
    (N'Nhà xuất bản Kim Đồng'),
    (N'Nhà xuất bản Trẻ');

-- Insert data into Categories table
INSERT INTO Categories (Name)
VALUES
    (N'Thiếu nhi'),
    (N'Khoa học'),
    (N'Văn học');

-- Insert data into Customers table
INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Debt, Street, District, City, Country, CartId)
VALUES
    ('CUS001', N'Nguyễn', N'Văn A', '0912345678', 0, N'Số 123', N'Quận 1', N'Thành phố Hồ Chí Minh', N'Việt Nam', 1),
    ('CUS002', N'Trần', N'Thị B', '0909876543', 0, N'Số 456', N'Quận 2', N'Thành phố Hồ Chí Minh', N'Việt Nam', 2);

-- Insert data into Carts table
INSERT INTO Carts (CustomerId, Amount)
VALUES
    ('CUS001', 0),
    ('CUS002', 0);

-- Insert data into Orders table
INSERT INTO Orders (OrderDate, State, CustomerId)
VALUES
    ('2023-01-01', 'paid', 'CUS001'),
    ('2023-02-01', 'awaiting', 'CUS002');

-- Insert data into Receipts table
INSERT INTO Receipts (Amount, CustomerId)
VALUES
    (100000, 'CUS001'),
    (200000, 'CUS002');

-- Insert data into Books table
INSERT INTO Books (Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PublicationDate, AuthorId, PublisherId)
VALUES
    (N'Bắt trẻ đồng xanh', N'Cuốn sách kể về chuyến phiêu lưu của một nhóm bạn thời thơ ấu.', '9781234567890', 10, 50000, 0.2, 200, '2022-01-01', 1, 1),
    (N'Dế Mèn phiêu lưu ký', N'Cuốn sách kể về cuộc phiêu lưu của Dế Mèn và những người bạn.', '9789876543210', 5, 45000, 0.1, 150, '2022-02-01', 2, 2);

-- Insert data into OrderDetails table
INSERT INTO OrderDetails (Quantity, BookId, OrderId)
VALUES
    (3, 1, 1),
    (2, 2, 2);

-- Insert data into CartItems table
INSERT INTO CartItems (Quantity, CartId, BookId)
VALUES
    (2, 1, 1),
    (1, 2, 2);

-- Insert data into BookCategories table
INSERT INTO BookCategories (BookId, CategoryId)
VALUES
    (1, 1),
    (2, 3);