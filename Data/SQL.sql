CREATE DATABASE WebXeDap;
GO

USE WebXeDap;
GO
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50),
    Password NVARCHAR(100),
    Email NVARCHAR(100),
    Phone NVARCHAR(20)
);

CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100)
);

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Price FLOAT,
    Description NVARCHAR(255),
    Image NVARCHAR(255),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);
INSERT INTO Categories (Name)
VALUES 
(N'Xe địa hình'),
(N'Xe thể thao');

INSERT INTO Products (Name, Price, Description, Image, CategoryId)
VALUES 
(N'Xe đạp MTB', 5000000, N'Xe địa hình xịn', '/images/mtb.jpg', 1),
(N'Xe đạp đua', 7000000, N'Xe tốc độ cao', '/images/road.jpg', 2);

INSERT INTO Users (Username, Password, Email, Phone)
VALUES 
('user1', '123', 'user1@gmail.com', '0123456789');