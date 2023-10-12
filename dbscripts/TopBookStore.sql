USE master;

IF DB_ID('TopBookStore') IS NOT NULL
    ALTER DATABASE TopBookStore SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP DATABASE TopBookStore;
GO

CREATE DATABASE TopBookStore;
GO

USE TopBookStore;

CREATE TABLE Authors
(
    AuthorId INT NOT NULL IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(60) NOT NULL,
    LastName NVARCHAR(60) NOT NULL,
    PhoneNumber VARCHAR(15)
);

CREATE TABLE Publishers
(
    PublisherId INT NOT NULL IDENTITY PRIMARY KEY,
    Name NVARCHAR(80) NOT NULL,
);

CREATE TABLE Categories
(
    CategoryId INT NOT NULL IDENTITY PRIMARY KEY,
    Name NVARCHAR(80) NOT NULL
);

CREATE TABLE Customers
(
    CustomerId INT NOT NULL IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(80) NOT NULL,
    LastName NVARCHAR(80) NOT NULL,
    Email NVARCHAR(70) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL,
    Debt MONEY NOT NULL DEFAULT 0,
    Street NVARCHAR(80),
    District NVARCHAR(50),
    City NVARCHAR(30),
    Country NVARCHAR(30)
);

CREATE TABLE Carts
(
    CartId INT NOT NULL IDENTITY PRIMARY KEY,
    Amount MONEY NOT NULL DEFAULT 0,
    CustomerId INT NOT NULL,

    -- When a customer is deleted, all associated cart records should be deleted as well.
    CONSTRAINT FK_Carts_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    ON DELETE CASCADE
);

CREATE TABLE Orders
(
    OrderId INT NOT NULL IDENTITY PRIMARY KEY,
    OrderDate DATETIME NOT NULL,
    Amount MONEY NOT NULL,
    State VARCHAR(30) DEFAULT 'awaiting'
        CHECK (state = 'awaiting' OR state = 'paid' OR state = 'sent'),
    CustomerId INT NOT NULL,

    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    ON DELETE CASCADE
);

CREATE TABLE Receipts
(
    ReceiptId INT NOT NULL IDENTITY PRIMARY KEY,
    Amount MONEY NOT NULL DEFAULT 0,
    CustomerId INT NOT NULL,

    CONSTRAINT FK_Receipts_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    ON DELETE CASCADE
);

CREATE TABLE Books
(
    BookId INT NOT NULL IDENTITY PRIMARY KEY,
    Title NVARCHAR(80) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Isbn13 VARCHAR(13) NOT NULL,
    Inventory INT NOT NULL,
    Price MONEY NOT NULL,
    DiscountPercent DEC(3, 2) NOT NULL DEFAULT 0,
    NumberOfPages INT,
    PublicationDate DATETIME NOT NULL,
    ImageUrl NVARCHAR(MAX),
    AuthorId INT NOT NULL,
    PublisherId INT NOT NULL,

    -- A Author or Publisher can be deleted only if all books related to those tables
    -- are deleted
    CONSTRAINT FK_Books_Authors FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
    ON DELETE NO ACTION,
    CONSTRAINT FK_Books_Publishers FOREIGN KEY (PublisherId) REFERENCES Publishers(PublisherId)
    ON DELETE NO ACTION
);

CREATE TABLE OrderDetails
(
    OrderDetailId INT NOT NULL IDENTITY PRIMARY KEY,
    Quantity INT NOT NULL DEFAULT 0,
    BookId INT NOT NULL,
    OrderId INT NOT NULL,

    CONSTRAINT FK_OrderDetails_Books FOREIGN KEY (BookId) REFERENCES Books(BookId)
    ON DELETE CASCADE,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
    ON DELETE CASCADE
);

CREATE TABLE CartItems
(
    CartItemId INT NOT NULL IDENTITY PRIMARY KEY,
    Quantity INT NOT NULL,
    CartId INT NOT NULL,
    BookId INT NOT NULL,

    CONSTRAINT FK_CartItems_Carts FOREIGN KEY (CartId) REFERENCES Carts(CartId)
    ON DELETE CASCADE,
    CONSTRAINT FK_CartItems_Books FOREIGN KEY (BookId) REFERENCES Books(BookId)
    ON DELETE CASCADE
);

CREATE TABLE BookCategory
(
    BookId INT NOT NULL,
    CategoryId INT NOT NULL,
    PRIMARY KEY (BookId, CategoryId),

    CONSTRAINT FK_BookCategory_Books FOREIGN KEY (BookId) REFERENCES Books(BookId)
    ON DELETE CASCADE,
    CONSTRAINT FK_BookCategory_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
    ON DELETE CASCADE
);