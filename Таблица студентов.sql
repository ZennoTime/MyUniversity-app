use University

create table Student(
	Id int identity(1,1) constraint PK_Student primary key,
	Name nvarchar(100),
	Age int,
	GroupId int
	foreign Key (GroupId) references Groups(Id) on update cascade,
)

select * from Student

insert into Student
	(Name, Age)
values
	('√еральт', 18),
	('Ёскель', 19),
	('Ћамберт', 20)


delete from Student

-------------------------------------------------------------------------- Ѕлок кода, где можно изменить определенную €чейку

update Student 
    set GroupId = (
        select Groups.Id
        from Groups
		where Groups.GroupName = 'PS - 12'
		)
	where Student.Name = '√еральт'
-------------------------------------------------------------------------- Ёксперименты 
update Student
set GroupId = 2
where GroupId = 1

update Student
set GroupId = 3
where Student.Id = 2
------------------------------------------------------------------------ ¬ывожу студентов по ID группы
select Name
from Student
where GroupId = 5 
