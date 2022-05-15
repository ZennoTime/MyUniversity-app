use University

create table Student(
	Id int identity(1,1) constraint PK_Student primary key,
	Name nvarchar(100),
	Age int,
	GroupId int
	Foreign Key (GroupId) references Groups(Id) on update cascade,
)

select * from Student

insert into Student
	(Name, Age)
values
	('�������', 18),
	('������', 19),
	('�������', 20)

ALTER TABLE Student ADD Age int;

delete from Student

-------------------------------------------------------------------------- ���� ����, ��� ����� �������� ������������ ������

UPDATE Student 
    SET GroupId = (
        SELECT Groups.Id
        FROM Groups
		WHERE Groups.GroupName = 'PS - 12'
		)
	WHERE Student.Name = '�������'
-------------------------------------------------------------------------- ������������ 
UPDATE Student
SET GroupId = 2
WHERE GroupId = 1

UPDATE Student
SET GroupId = 3
WHERE Student.Id = 2
------------------------------------------------------------------------ ������ ��������� �� ID ������
SELECT Name
FROM Student
WHERE GroupId = 5 

