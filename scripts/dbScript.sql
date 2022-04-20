DROP DATABASE IF EXISTS CarPractiseDB
GO

CREATE DATABASE CarPractiseDB
GO

USE CarPractiseDB
GO

CREATE TABLE CompanyCars (
    VIN VARCHAR(21) PRIMARY KEY, 
    NumberPlate VARCHAR(7) NOT NULL, 
    FabricationYear INT NOT NULL,
    NextITV VARCHAR(7) NOT NULL,
    NextCarInspection VARCHAR(7) NOT NULL
    CONSTRAINT AK_NumberPlate UNIQUE(NumberPlate)   
)
GO

CREATE TABLE Users (
    UserId VARCHAR(10) PRIMARY KEY, 
    FullName NVARCHAR(100) NOT NULL, 
    GivenName NVARCHAR(50) NOT NULL,
    FamilyName NVARCHAR(50) NOT NULL, 
    ImageURL VARCHAR(200) NOT NULL, 
    Email VARCHAR(50) NOT NULL, 
    PhoneNumber VARCHAR(50),
    Department NVARCHAR(50), 
    AddressFormatted NVARCHAR(50),
    Role VARCHAR(15) NOT NULL,
    CreationDate DATETIME NOT NULL,
    LastLogin DATETIME NOT NULL,
    CONSTRAINT AK_Email UNIQUE(Email),
    CONSTRAINT AK_Phone UNIQUE(PhoneNumber) 
)
GO

CREATE TABLE Reservations (
    ReservationId INT IDENTITY(1,1) PRIMARY KEY, 
    UserId VARCHAR(10) FOREIGN KEY REFERENCES Users(UserId),
    VIN VARCHAR(21) FOREIGN KEY REFERENCES CompanyCars(VIN),
    FromDate VARCHAR(10) NOT NULL, 
    ToDate VARCHAR(10) NOT NULL,
    CarUse VARCHAR(13) NOT NULL
)
GO

INSERT INTO CompanyCars (VIN, NumberPlate, FabricationYear, NextITV, NextCarInspection)
VALUES 
    ("WVGZZZ1TZ8W045784", "4793RTY", 2021, "09-2024", "09-2022"),
    ("ZDCNF11A0MF015755", "3456RTU", 2021, "05-2023", "09-2023"),
    ("ZCFA81LN302718519", "0984RTI", 2021, "12-2024", "12-2022");
GO

INSERT INTO Users 
VALUES 
    (123456, "Alejandro Medina García", "Alejandro", "Medina García", 
    "testImage", "Alejandro.Medina@test.com", "+34 57685934","testDepartment", 
    "testAddress", "Administrator", '2038-01-19 03:14:07.99', '2038-01-19 03:14:07.99'),
    (144666, "Tesssssssst", "Test", "Tesssssssst Test", 
    "testImage", "Test.Test@test.com", "+34 573654934","testDepartment", 
    "testAddress", "Employee", '2038-01-19 03:14:07.99', '2038-01-19 03:14:07.99');
GO

INSERT INTO Reservations 
VALUES 
    (123456, "WVGZZZ1TZ8W045784", "02-09-2024", "24-09-2024", "shared");
GO