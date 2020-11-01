USE master
GO
CREATE DATABASE TrueMart
GO
USE TrueMart
GO
CREATE TABLE [Role](
Id INT PRIMARY KEY IDENTITY,
RoleName VARCHAR(50)
)
GO
CREATE TABLE [User](
Id INT PRIMARY KEY IDENTITY,
Username VARCHAR(255),
FullName NVARCHAR(255),
Gender SMALLINT,
[Password] VARCHAR(255),
[Email] VARCHAR(255),
PhoneNumber VARCHAR(20),
[Address] NVARCHAR(MAX),
RoleId INT FOREIGN KEY REFERENCES dbo.[Role](Id),
RecordStatus INT,
[Note] NVARCHAR(MAX)
CreatedBy INT,
CreatedDate DATETIME,
UpdatedBy INT,
UpdatedDate DATETIME,
)
GO
CREATE TABLE Category(
Id INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(255),
[UrlSlag] VARCHAR(255),
RecordStatus SMALLINT,
[Note] NVARCHAR(MAX)
CreatedBy INT,
CreatedDate DATETIME,
UpdatedBy INT,
UpdatedDate DATETIME,
)
GO
CREATE TABLE Product(
Id INT PRIMARY KEY IDENTITY,
ProductName NVARCHAR(255),
[UrlSlag] VARCHAR(255),
CategoryId INT FOREIGN KEY REFERENCES dbo.Category(Id),
Remaining INT,
Price DECIMAL,
SalePrice DECIMAL,
[Description] NVARCHAR(MAX),
RecordStatus SMALLINT,
[Note] NVARCHAR(MAX)
CreatedBy INT,
CreatedDate DATETIME,
UpdatedBy INT,
UpdatedDate DATETIME,
)
GO
CREATE TABLE ProductImage(
ImageId INT PRIMARY KEY IDENTITY,
ProductId INT FOREIGN KEY REFERENCES dbo.Product(Id),
Url VARCHAR(500),
RecordStatus SMALLINT,
[Note] NVARCHAR(MAX)
CreatedBy INT,
CreatedDate DATETIME,
UpdatedBy INT,
UpdatedDate DATETIME,
)
GO
CREATE TABLE Promotion(
Id INT PRIMARY KEY IDENTITY,
PromotionName NVARCHAR(255),
StartDate DATETIME,
EndDate DATETIME,
DiscountValue FLOAT,
[Description] NVARCHAR(MAX),
RecordStatus SMALLINT,
[Note] NVARCHAR(MAX)
CreatedBy INT,
CreatedDate DATETIME,
UpdatedBy INT,
UpdatedDate DATETIME,
)
GO
CREATE TABLE Product_Promotion(
PromotionId INT FOREIGN KEY REFERENCES dbo.Promotion(Id),
ProductId INT FOREIGN KEY REFERENCES dbo.Product(Id),
)
GO
CREATE TABLE [Order](
Id INT PRIMARY KEY IDENTITY,
CustomerId INT FOREIGN KEY REFERENCES dbo.[User](Id),
OrderDate DATETIME,
TotalAmount DECIMAL,
OrderStatus SMALLINT,
RecordStatus SMALLINT,
[OrderNote] NVARCHAR(MAX),
[Note] NVARCHAR(MAX),
CreatedBy INT,
CreatedDate DATETIME,
UpdatedBy INT,
UpdatedDate DATETIME,
)
GO
CREATE TABLE OrderDetail(
OrderId INT FOREIGN KEY REFERENCES dbo.[Order](Id),
ProductId INT FOREIGN KEY REFERENCES dbo.Product(Id),
Quantity INT,
Price DECIMAL
PRIMARY KEY (OrderId,ProductId)
)