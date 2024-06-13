-- ������������� ��������� ���� ������
USE VetClinicDB2;
-- �������� ������� "�������"
CREATE TABLE Clients (
    ID INT IDENTITY (1, 1) PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    ContactPhone VARCHAR(15),
    Address VARCHAR(100),
	Balance int default 0 
);

-- �������� ������� "��������"
CREATE TABLE Patients (
    ID INT IDENTITY (1, 1) PRIMARY KEY,
    Name VARCHAR(50) not null,
    AnimalType VARCHAR(50) not null,
    DateOfBirth DATE,
    Gender varchar(20) check (Gender IN ('�����','�����','�����������')) not null,
    ClientID INT not null,
    FOREIGN KEY (ClientID) REFERENCES Clients(ID)
);

CREATE TABLE Procedur (
    ID INT IDENTITY (1, 1) PRIMARY KEY,
    Name VARCHAR(50) not null,
    Price INT not null
);

-- �������� ������� "������"
CREATE TABLE Appointments (
    ID INT IDENTITY (1, 1) PRIMARY KEY,
    DateTime DATETIME not null,
    Description TEXT,
    PatientID INT not null,
	ClientsID INT,
	ProceduresID INT not null,
    Cost INT not  null,
	StatusPayment varchar(20) check (StatusPayment IN ('��������' , '� ��������')) not null,
    FOREIGN KEY (PatientID) REFERENCES Patients(ID),
	FOREIGN KEY (ClientsID) REFERENCES Clients(ID),
	FOREIGN KEY (ProceduresID) REFERENCES Procedur(ID)
);

-- �������� ������� "������������"
CREATE TABLE Users (
    UserID INT IDENTITY (1, 1) PRIMARY KEY,
    UserName NVARCHAR(50) not null,
    Password NVARCHAR(100) not null,
    Role nvarchar(50) check (Role IN ('������������','�������������')) not null,
);


