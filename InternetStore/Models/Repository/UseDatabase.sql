USE InternetStore
SELECT * FROM Products

USE InternetStore
SELECT * FROM Responses
WHERE WillAttend = 1

USE InternetStore
SELECT Id, Name, Email FROM Responses
WHERE WillAttend = 'true'

USE InternetStore
SELECT Id, Name, Email FROM Responses
WHERE WillAttend = 'true'
ORDER BY Email

USE InternetStore
INSERT INTO Responses(Name, Email, Phone, WillAttend)
VALUES ('Joe Dobbs', 'joe@example.com', '555-888-1234', 1)

USE InternetStore
UPDATE Responses
SET Phone='404-204-1234'
WHERE WillAttend = 1

USE InternetStore
SELECT * FROM Responses

USE InternetStore
DELETE FROM Responses
WHERE WillAttend = 0

USE InternetStore
DROP TABLE IF EXISTS Preferences
CREATE TABLE Preferences (
Id bigint IDENTITY,
Email nvarchar(max),
NutAllergy bit,
Teetotal bit,
ResponseId bigint,
)

USE InternetStore
INSERT INTO Responses(Name, Email, Phone, WillAttend)
VALUES ('Dave Habbs', 'dave@example.com', '555-777-1234', 1)
INSERT INTO Preferences (Email, NutAllergy, Teetotal)
VALUES ('dave@example.com', 0, 1)

USE InternetStore
SELECT * FROM Products

USE InternetStore
ALTER TABLE Products
ADD CategoryID bigint

USE InternetStore
CREATE TABLE Categories (
Id bigint IDENTITY,
Name nvarchar(max),
)

USE InternetStore
INSERT INTO Categories(Name) VALUES ('Circular duct fans')
INSERT INTO Categories(Name) VALUES ('Axial fans')
INSERT INTO Categories(Name) VALUES ('Explosion proof fans')
INSERT INTO Categories(Name) VALUES ('Smoke extract fans')

USE InternetStore
SELECT * FROM Categories

USE InternetStore
ALTER TABLE Products
DROP COLUMN Category;
