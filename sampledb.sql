CREATE DATABASE sampledb

USE sampledb

CREATE TABLE Users(id INT PRIMARY KEY, email VARCHAR(255) NOT NULL, username VARCHAR(255) NOT NULL, password VARCHAR(255) NOT NULL);

--DROP TABLE users

INSERT INTO Users VALUES(1, 'ankit@gmail.com', 'ankit', '12345');
INSERT INTO Users VALUES(2, 'akash@yahoo.com', 'akash', '12345');
INSERT INTO Users VALUES(3, 'anish@zoho.com', 'anish', '12345');
INSERT INTO Users VALUES(4, 'aman@gmail.com', 'aman', '12345');


CREATE TABLE Userrole(id INT PRIMARY KEY IDENTITY, userid int, userrole VARCHAR(255),
CONSTRAINT FK_users_userrole FOREIGN KEY (userid)
REFERENCES users(id)
)

--DROP TABLE userrole

INSERT INTO Userrole VALUES(1, 'admin');
INSERT INTO Userrole VALUES(2, 'user');
INSERT INTO Userrole VALUES(3, 'user');
INSERT INTO Userrole VALUES(4, 'user');

CREATE TABLE Employee(id INT PRIMARY KEY IDENTITY, name VARCHAR(255) NOT NULL, salary MONEY, designation VARCHAR(255))

--DROP TABLE EMPLOYEE

INSERT INTO Employee VALUES('ankit', 234234.43, 'admin');
INSERT INTO Employee VALUES('anish', 14234.43, 'trainee');
INSERT INTO Employee VALUES('ankit', 44234.43, 'software engineer');
INSERT INTO Employee VALUES('ankit', 54234.43, 'manager');

Select * from Employee;