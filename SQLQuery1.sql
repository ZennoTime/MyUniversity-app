use University --������� �������
create table Groups(
	Id int identity(1,1) constraint PK_Groups primary key,
	GroupName nvarchar(100),
)
---------------------------------------------------------------------------

select * from Groups --����� ����������� �������

UPDATE Groups 
    SET StudentId = (
        SELECT Student.Id
        FROM Student
		WHERE Student.Id != 0

    );
UPDATE Groups 
    SET StudentInGroup = (
		INSERT INTO Groups (StudentInGroup)
		values
			(1)
		WHERE Groups.StudentId = 6
    );


UPDATE Groups SET  StudentInGroup = 9  WHERE Id = 9

-----------------------------------------------------------------------------
INSERT INTO Groups (StudentId) --��������� �������� �� ������� Id � ������� Student � ������� StudentId � ������� Groups
SELECT Id FROM Student
-----------------------------------------------------------------------------
ALTER TABLE Groups ADD CheckStInGr integer --�������� ������� � �������
ALTER TABLE Groups DROP COLUMN CheckStInGr; --������� ������� �� ��������


UPDATE Groups
	SET StudentInGroup 


select * from Student

insert into Groups --��������� �������� � ������� GroupName
	(GroupName)
values
	('Programmers'),
	('Economists'),
	('Biologists')

--------------------------------------------------------------------------------------
create table Post(
	Id int identity(1,1) constraint PK_Post primary key,
	Title nvarchar(100),
	Body nvarchar(max),
	AuthorId int constraint FK_Post_Author references Author(Id)
)

select * from Post
select * from Author

insert into Post
values
('��������� 1', '���� �������', 3),
('��������� 2', '���� ������� 2', 3),
('��������� 3', '���� ������� 3', 1)

select p.Title, p.Body, a.Name as AuthorName from Post as p
join Author a on a.Id = p.AuthorId

---------------------------------------------------------------