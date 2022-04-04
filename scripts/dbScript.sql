CREATE DATABASE ApiDB
GO
USE ApiDB
GO

CREATE TABLE Cars (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    Username NVARCHAR(50) NOT NULL, 
    PlateNumber NVARCHAR(7) NOT NULL,
    FabricationYear INT NOT NULL, 
    NextITV NVARCHAR(7) NOT NULL,
    CONSTRAINT AK_PlateNumber UNIQUE(PlateNumber)   
)
GO

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    Username NVARCHAR(50) NOT NULL, 
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(15) NOT NULL 
)
GO

INSERT INTO Cars 
VALUES  ('example_admin',	      '7485DTR', 2010, '10-2022'),
        ('example_general_user1', '5625ERT', 2009, '09-2022'),
        ('example_general_user2', '8956ASD', 2017, '05-2022'),
        ('example_general_user1', '4759KLU', 2020, '03-2024')
GO

INSERT INTO Users 
VALUES  ('example_general_user1', 'MyPass_w0rd',  'GeneralUser'),
        ('example_general_user2', 'MyPass_w0rd2', 'GeneralUser'),
        ('example_admin',		  'MyPass_w0rd',  'Administrator')
GO