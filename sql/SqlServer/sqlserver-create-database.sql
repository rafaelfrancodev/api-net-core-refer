
CREATE SCHEMA common;
GO

CREATE TABLE common.Person (
	Id INT IDENTITY PRIMARY KEY NOT NULL,
	Name VARCHAR(150) NOT NULL,
	DateBirthday DATE NOT NULL,
	Active BIT DEFAULT(1),
	IdUser INT NULL,
	CreatedAt DATETIME NULL,
	UpdatedAt DATETIME NULL,
	IdUserChange INT NULL,
	Deleted BIT DEFAULT(0)
)