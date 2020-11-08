 
USE [practicamad]


/* ********** Drop Table UserProfile if already exists *********** */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CreditCard]') AND type in ('U'))
DROP TABLE [CreditCard]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Product]') AND type in ('U'))
DROP TABLE [Product]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */

CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)

CREATE TABLE CreditCard (
	cardId bigint IDENTITY(1,1) NOT NULL,
	cardType varchar(10) NOT NULL,
	cardNumber int NOT NULL,
	verificationCode int NOT NULL,
	expirationDaye date NOT NULL,
	defaultCard tinyint NOT NULL,
	userId bigint NOT NULL,

	CONSTRAINT [PK_CreditCard] PRIMARY KEY (cardId),
	CONSTRAINT [FK_UserCard] FOREIGN KEY (userId) REFERENCES UserProfile(usrId),
	CONSTRAINT [UniqueKey_Number] UNIQUE (cardNumber)
)

CREATE TABLE Category (
	categoryId bigint IDENTITY(1,1) NOT NULL,
	categoryName varchar(50) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId),
	CONSTRAINT [UniqueKey_CategoryName] UNIQUE (categoryName)
)

CREATE TABLE Product (
	productId bigint IDENTITY(1,1) NOT NULL,
	productName varchar(50) NOT NULL,
	productPrice numeric(10,2) NOT NULL,
	productDate date NOT NULL,
	productQuantity int NOT NULL,
	categoryId bigint NOT NULL,
	
	CONSTRAINT [PK_Product] PRIMARY KEY (productId),
	CONSTRAINT [FK_ProductCategory] FOREIGN KEY (categoryId) REFERENCES Category(categoryId),
	CONSTRAINT [UniqueKey_ProductName] UNIQUE (productName)
)

CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Tables UserProfile, CreditCard, Category and Product created.'
GO

GO
