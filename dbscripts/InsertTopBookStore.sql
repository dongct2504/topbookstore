USE TopBookStore;

SELECT * FROM Customers;

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
INSERT INTO Authors (AuthorId, FirstName, LastName, PhoneNumber)
VALUES
    (1, N'Nguyễn', N'Nhật Ánh', '0987654321'),
    (2, N'Nguyễn', N'Du', '0123456789');

-- Insert data into Publishers table
INSERT INTO Publishers (PublisherId, Name)
VALUES
    (1, N'Nhà xuất bản Kim Đồng'),
    (2, N'Nhà xuất bản Trẻ');

-- Insert data into Categories table
INSERT INTO Categories (CategoryId, Name)
VALUES
    ('CT001', N'Thiếu nhi'),
    ('CT002', N'Khoa học'),
    ('CT003', N'Văn học');

-- Insert data into Customers table
INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Debt, Street, District, City, Country, CartId)
VALUES
    ('CUS001', N'Nguyễn', N'Văn A', '0912345678', 0, N'Số 123', N'Quận 1', N'Thành phố Hồ Chí Minh', N'Việt Nam', 1),
    ('CUS002', N'Trần', N'Thị B', '0909876543', 0, N'Số 456', N'Quận 2', N'Thành phố Hồ Chí Minh', N'Việt Nam', 2);

-- Insert data into Carts table
INSERT INTO Carts (CartId, CustomerId, Amount)
VALUES
    (1, 'CUS001', 0),
    (2, 'CUS002', 0);

-- Insert data into Orders table
INSERT INTO Orders (OrderId, OrderDate, State, CustomerId)
VALUES
    (1, '2023-01-01', 'paid', 'CUS001'),
    (2, '2023-02-01', 'awaiting', 'CUS002');

-- Insert data into Receipts table
INSERT INTO Receipts (ReceiptId, Amount, CustomerId)
VALUES
    (1, 100000, 'CUS001'),
    (2, 200000, 'CUS002');

-- Insert data into Books table
INSERT INTO Books (BookId, Title, Description, Isbn13, Inventory, Price, DiscountPercent, NumberOfPages, PulicationDate, AuthorId, PublisherId, OrderId, CartId)
VALUES
    (1, N'Bắt trẻ đồng xanh', N'Cuốn sách kể về chuyến phiêu lưu của một nhóm bạn thời thơ ấu.', '9781234567890', 10, 50000, 0.2, 200, '2022-01-01', 1, 1, 1, 1),
    (2, N'Dế Mèn phiêu lưu ký', N'Cuốn sách kể về cuộc phiêu lưu của Dế Mèn và những người bạn.', '9789876543210', 5, 45000, 0.1, 150, '2022-02-01', 2, 2, 2, 2);

-- Insert data into OrderDetails table
INSERT INTO OrderDetails (OrderDetailId, Quantity, BookId, OrderId)
VALUES
    (1, 3, 1, 1),
    (2, 2, 2, 2);

-- Insert data into CartItems table
INSERT INTO CartItems (CartItemId, Quantity, CartId, BookId)
VALUES
    (1, 2, 1, 1),
    (2, 1, 2, 2);

-- Insert data into BookCategories table
INSERT INTO BookCategories (BookId, CategoryId)
VALUES
    (1, 'CT001'),
    (2, 'CT003');