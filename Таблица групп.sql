use University --Создаеём таблицу
create table Groups(
	Id int identity(1,1) constraint PK_Groups primary key,
	GroupName nvarchar(100),
)
---------------------------------------------------------------------------

select * from Groups --Вывод содержимого таблицы

-----------------------------------------------------------------------------
alter table Groups add CheckStInGr integer --Добавить столбец в таблицу
alter table Groups drop column CheckStInGr; --Удалить столбец из талблицы

------------------------------------------------------------------------------

insert into Groups --Добавляем значения в колонку GroupName
	(GroupName)
values
	('Programmers'),
	('Economists'),
	('Biologists')

--------------------------------------------------------------------------------------
