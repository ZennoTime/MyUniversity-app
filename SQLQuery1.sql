use University --Создаеём таблицу
create table Groups(
	Id int identity(1,1) constraint PK_Groups primary key,
	GroupName nvarchar(100),
)
---------------------------------------------------------------------------

select * from Groups --Вывод содержимого таблицы

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
INSERT INTO Groups (StudentId) --Добавляем значения из столбца Id в таблице Student в столбец StudentId в таблице Groups
SELECT Id FROM Student
-----------------------------------------------------------------------------
ALTER TABLE Groups ADD CheckStInGr integer --Добавить столбец в таблицу
ALTER TABLE Groups DROP COLUMN CheckStInGr; --Удалить столбец из талблицы


UPDATE Groups
	SET StudentInGroup 


select * from Student

insert into Groups --Добавляем значения в колонку GroupName
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
('Заголовок 1', 'Тело новости', 3),
('Заголовок 2', 'Тело новости 2', 3),
('Заголовок 3', 'Тело новости 3', 1)

select p.Title, p.Body, a.Name as AuthorName from Post as p
join Author a on a.Id = p.AuthorId

---------------------------------------------------------------