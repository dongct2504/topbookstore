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
    AuthorId INT PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(15)
);

CREATE TABLE Categories
(
    CategoryId VARCHAR(30) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Publishers
(
    PublisherId INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(15)
);

CREATE TABLE Books
(
    BookId INT PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    BookDescription NVARCHAR(200) NOT NULL DEFAULT '',
    Isbn13 VARCHAR(13) NOT NULL,
    Price MONEY NOT NULL,
    NumberOfPages INT,
    PulicationDate DATETIME,
    CategoryId VARCHAR(30) NOT NULL,
    PublisherId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (PublisherId) REFERENCES Publishers(PublisherId)
);

-- Junction Table
CREATE TABLE BookAuthors
(
    BookId INT NOT NULL,
    AuthorId INT NOT NULL,
    PRIMARY KEY (BookId, AuthorId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);

CREATE TABLE Addresses
(
    AddressId INT PRIMARY KEY,
    Street NVARCHAR(100),
    District NVARCHAR(50),
    City NVARCHAR(50),
    ZipCode VARCHAR(10),
    Country NVARCHAR(50)
);

CREATE TABLE Customers
(
    CustomerId INT PRIMARY KEY,
    FirstName NVARCHAR(80) NOT NULL,
    LastName NVARCHAR(80) NOT NULL,
    Email VARCHAR(80) NOT NULL,
    AddressId INT NOT NULL,
    FOREIGN KEY (AddressId) REFERENCES Addresses(AddressId)
);

CREATE TABLE Invoices
(
    InvoiceId INT NOT NULL PRIMARY KEY,
    InvoiceDate DATETIME NOT NULL,
    TotalAmount MONEY NOT NULL DEFAULT 0,
    CustomerId INT NOT NULL,
    AddressId INT NOT NULL,

    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (AddressId) REFERENCES Addresses(AddressId)
);

CREATE TABLE LineItem
(
    LineItemId INT NOT NULL PRIMARY KEY,
    Quantity INT NOT NULL DEFAULT 0,
    InvoiceId INT NOT NULL,
    BookId INT NOT NULL,
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

CREATE TABLE Carts
(
    CartId INT PRIMARY KEY,
    Quantity INT NOT NULL DEFAULT 0,
    CustomerId INT NOT NULL,
    BookId INT NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);