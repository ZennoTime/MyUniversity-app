use University --������� �������
create table Groups(
	Id int identity(1,1) constraint PK_Groups primary key,
	GroupName nvarchar(100),
)
---------------------------------------------------------------------------

select * from Groups --����� ����������� �������

-----------------------------------------------------------------------------
ALTER TABLE Groups ADD CheckStInGr integer --�������� ������� � �������
ALTER TABLE Groups DROP COLUMN CheckStInGr; --������� ������� �� ��������

------------------------------------------------------------------------------

insert into Groups --��������� �������� � ������� GroupName
	(GroupName)
values
	('Programmers'),
	('Economists'),
	('Biologists')

--------------------------------------------------------------------------------------
