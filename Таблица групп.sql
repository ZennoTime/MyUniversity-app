use University --������� �������
create table Groups(
	Id int identity(1,1) constraint PK_Groups primary key,
	GroupName nvarchar(100),
)
---------------------------------------------------------------------------

select * from Groups --����� ����������� �������

-----------------------------------------------------------------------------
alter table Groups add CheckStInGr integer --�������� ������� � �������
alter table Groups drop column CheckStInGr; --������� ������� �� ��������

------------------------------------------------------------------------------

insert into Groups --��������� �������� � ������� GroupName
	(GroupName)
values
	('Programmers'),
	('Economists'),
	('Biologists')

--------------------------------------------------------------------------------------
