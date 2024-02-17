CREATE DATABASE Marks;

USE Marks;

CREATE TABLE StudentsGrades (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    GroupName NVARCHAR(50) NOT NULL,
    AverageGrade FLOAT NOT NULL,
    MinSubject NVARCHAR(50) NOT NULL,
    MaxSubject NVARCHAR(50) NOT NULL
);

INSERT INTO StudentsGrades (FullName, GroupName, AverageGrade, MinSubject, MaxSubject)
VALUES ('Grigoriev Dmytro Igorovich', 'Group 34', 4.5, 'Math', 'History');

INSERT INTO StudentsGrades (FullName, GroupName, AverageGrade, MinSubject, MaxSubject)
VALUES ('Petrenko Oleg Oleksandrovich', 'Group 24', 4.0, 'Physich', 'Math');