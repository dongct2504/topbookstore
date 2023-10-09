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
  AuthorId INT NOT NULL PRIMARY KEY,
  FirstName NVARCHAR(60) NOT NULL,
  LastName NVARCHAR(60) NOT NULL,
  PhoneNumber VARCHAR(15)
);

CREATE TABLE Publishers
(
  PublisherId INT NOT NULL PRIMARY KEY,
  Name NVARCHAR(80) NOT NULL,
);

CREATE TABLE Categories
(
  CategoryId INT NOT NULL PRIMARY KEY,
  Name NVARCHAR(80) NOT NULL
);

CREATE TABLE Customers
(
  CustomerId NVARCHAR(450) NOT NULL PRIMARY KEY,
  FirstName NVARCHAR(80) NOT NULL,
  LastName NVARCHAR(80) NOT NULL,
  PhoneNumber VARCHAR(15),
  Debt MONEY NOT NULL DEFAULT 0,
  Street NVARCHAR(80),
  District NVARCHAR(50),
  City NVARCHAR(30),
  Country NVARCHAR(30),
  CartId INT NOT NULL,
);

CREATE TABLE Carts
(
  CartId INT NOT NULL PRIMARY KEY,
  CustomerId NVARCHAR(450) NOT NULL,
  Amount MONEY NOT NULL DEFAULT 0,
  
  -- When a customer is deleted, all associated cart records should be deleted as well.
  CONSTRAINT FK_Carts_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    ON DELETE CASCADE
);

CREATE TABLE Orders
(
  OrderId INT NOT NULL PRIMARY KEY,
  OrderDate DATETIME NOT NULL,
  State VARCHAR(30) DEFAULT 'awaiting'
    CHECK (state = 'awaiting' OR state = 'paid' OR state = 'sent'),
  CustomerId NVARCHAR(450) NOT NULL,

  CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    ON DELETE CASCADE
);

CREATE TABLE Receipts
(
  ReceiptId INT NOT NULL PRIMARY KEY,
  Amount MONEY NOT NULL,
  CustomerId NVARCHAR(450) NOT NULL,

  CONSTRAINT FK_Receipts_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    ON DELETE CASCADE
);

CREATE TABLE Books
(
  BookId INT NOT NULL PRIMARY KEY,
  Title NVARCHAR(80) NOT NULL,
  Description NVARCHAR(MAX) NOT NULL,
  Isbn13 VARCHAR(13) NOT NULL,
  Inventory INT NOT NULL,
  Price MONEY NOT NULL,
  DiscountPercent DEC(3, 2) NOT NULL,
  NumberOfPages INT,
  PulicationDate DATETIME NOT NULL,
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
  OrderDetailId INT NOT NULL PRIMARY KEY,
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
  CartItemId INT NOT NULL PRIMARY KEY,
  Quantity INT NOT NULL,
  CartId INT NOT NULL,
  BookId INT NOT NULL,

  CONSTRAINT FK_CartItems_Carts FOREIGN KEY (CartId) REFERENCES Carts(CartId)
    ON DELETE CASCADE,
  CONSTRAINT FK_CartItems_Books FOREIGN KEY (BookId) REFERENCES Books(BookId)
    ON DELETE CASCADE
);

CREATE TABLE BookCategories
(
  BookId INT NOT NULL,
  CategoryId INT NOT NULL,
  PRIMARY KEY (BookId, CategoryId),

  CONSTRAINT FK_BookCategories_Books FOREIGN KEY (BookId) REFERENCES Books(BookId)
    ON DELETE CASCADE,
  CONSTRAINT FK_BookCategories_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
    ON DELETE CASCADE
);