DROP DATABASE IF EXISTS CarPractiseDB
GO

CREATE DATABASE CarPractiseDB
GO

USE CarPractiseDB
GO

CREATE TABLE CompanyCars (
    VIN VARCHAR(21) PRIMARY KEY, 
    numberPlate VARCHAR(7) NOT NULL, 
    fabricationYear INT NOT NULL,
    nextITV VARCHAR(7) NOT NULL,
    nextCarInspection VARCHAR(7) NOT NULL,
    CONSTRAINT AK_NumberPlate UNIQUE(numberPlate)   
)
GO

CREATE TABLE Users (
    userId INT PRIMARY KEY, 
    fullName NVARCHAR(100) NOT NULL, 
    givenName NVARCHAR(50) NOT NULL,
    familyName NVARCHAR(50) NOT NULL, 
    imageURL VARCHAR(200) NOT NULL, 
    email VARCHAR(50) NOT NULL, 
    phoneNumber VARCHAR(50),
    department NVARCHAR(50), 
    addressFormatted NVARCHAR(50),
    role VARCHAR(15) NOT NULL,
    creationDate DATETIME NOT NULL,
    lastLogin DATETIME NOT NULL,
    CONSTRAINT AK_Email UNIQUE(email) 
)
GO

CREATE TABLE Reservations (
    id INT IDENTITY(1,1) PRIMARY KEY, 
    userId INT NOT NULL,
    VIN VARCHAR(21),
    fromDate DATE NOT NULL, 
    toDate DATE NOT NULL,
    carUse NVARCHAR(15) NOT NULL 
)
GO

INSERT INTO CompanyCars 
VALUES 
    ("WVGZZZ1TZ8W045784", "4793RTY", 2021, "09-2024", "09-2022"),
    ("ZDCNF11A0MF015755", "3456RTU", 2021, "05-2023", "09-2023"),
    ("ZCFA81LN302718519", "0984RTI", 2021, "12-2024", "12-2022");
GO

INSERT INTO Users 
VALUES 
    (123456, "Alejandro Medina García", "Alejandro", "Medina García", 
    "testImage", "Alejandro.Medina@test.com", "+34 57685934","testDepartment", 
    "testAddress", "Administrator", '2038-01-19 03:14:07.99', '2038-01-19 03:14:07.99');
GO