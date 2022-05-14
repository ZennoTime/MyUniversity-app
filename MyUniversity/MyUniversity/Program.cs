using System;
using System.Collections.Generic;
using MyUniversity.Models;
using MyUniversity.Repositories;

namespace MyUniversity
{
    class Program
    {
        private static string _connectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=University;Pooling=true;Integrated Security=SSPI";

        static void Main( string[] args )
        {
            IStudentRepository studentRepository = new StudentRawSqlRepository( _connectionString );
            IGroupRepository groupRepository = new GroupRawSqlRepository( _connectionString );

            Console.WriteLine( "Доступные команды:" );
            Console.WriteLine();
            Console.WriteLine( "Get запросы: " );
            Console.WriteLine( "  get-students - показать список студентов" );
            Console.WriteLine( "  get-groups - показать список групп" );
            Console.WriteLine( "  get students by group Id - вывести список студентов по Id их группы " );
            Console.WriteLine();
            Console.WriteLine( "Add - запросы: " );
            Console.WriteLine( "  add-student - добавить студента" );
            Console.WriteLine( "  add-group - добавить группу" );
            Console.WriteLine( "  add student in group - добавить студента в группу" );
            Console.WriteLine();
            Console.WriteLine("Запросы состояния элементов: ");
            Console.WriteLine( "  update-student - изменить информацию о студенте" );
            Console.WriteLine( "  delete-student - удалить студента" );
            Console.WriteLine();
            Console.WriteLine( "exit - выйти из приложения" );

            while ( true )
            {
                string command = Console.ReadLine();

                if ( command == "get-students" )
                {
                    List<Student> students = studentRepository.GetAll();
                    foreach ( Student student in students )
                    {
                        Console.WriteLine( $"Id: {student.Id}, [Name, Age] = [{student.Name},  {student.Age} лет] " );
                    }
                }
                else if ( command == "add-student" )
                {
                    Console.WriteLine( "Введите имя автора" );// Здесь так же нужно сделать проверку на имя и возраст, подумай над тем, чтобы вынести данную проверку в другой модуль!
                    string name = Console.ReadLine();
                    Console.WriteLine( "Введите возраст автора" );
                    int age = Convert.ToInt32( Console.ReadLine() ); // Добавил, в целом в коде доделай функции для студента, дальше иди к функициям для группы, дальше всё объедини 

                    studentRepository.Add( new Student
                    {
                        Name = name,
                        Age = age
                    } );
                    Console.WriteLine( "Успешно добавлено" );
                }
                else if ( command == "update-student" )
                {
                    Console.WriteLine( "Введите id автора" );
                    int studentId = int.Parse( Console.ReadLine() );
                    Student student = studentRepository.GetById( studentId );

                    if ( student == null )
                    {
                        Console.WriteLine( "Автор не найден" );
                        continue;
                    }
                    bool FlagUpdate = true;
                    bool FlagAge = true;
                    while ( FlagUpdate )
                    {
                        Console.WriteLine( "Введите новое имя автора" );
                        student.Name = Console.ReadLine();

                        if ( student.Name == String.Empty )
                        {
                            Console.WriteLine( "" );
                            Console.WriteLine( "Неверно введено имя" );
                            Console.WriteLine( "         ↓         " );
                        }
                        else
                        {
                            Console.WriteLine( "Введите возраст автора" );
                            while ( FlagAge )
                            {
                                student.Age = Convert.ToInt32( Console.ReadLine() );
                                if ( student.Age < 15 )
                                {
                                    Console.WriteLine( "---► Неверно введён возраст" );
                                    Console.WriteLine( "----► Возраст не может быть меньше 15, университет не для школьного возраста..." );
                                    Console.WriteLine( "                ↓           " );
                                    Console.WriteLine( "-----► Введите новый возраст" );
                                    continue;
                                }
                                else
                                {
                                    FlagAge = false;
                                }
                            }
                        }
                        if ( student.Name != String.Empty & student.Age >= 15 )
                        {
                            FlagUpdate = false;
                        }
                    }

                    studentRepository.Update( student );
                    Console.WriteLine( "Успешно обновлено" );
                }
                else if ( command == "delete-student" )
                {
                    Console.WriteLine( "Введите id студента" );
                    int studentId = int.Parse( Console.ReadLine() );
                    var student = studentRepository.GetById( studentId );
                    if ( student == null )
                    {
                        Console.WriteLine( "Студент не найден" );
                        continue;
                    }

                    studentRepository.DeleteById( student.Id );
                    Console.WriteLine( "Успешно удалено" );
                }
                else if ( command == "get-groups" )
                {
                    List<Group> groups = groupRepository.GetAll();
                    foreach ( Group group in groups )
                    {
                        Console.WriteLine( $"Id: {group.Id}, Group Name is {group.GroupName}" );
                    }
                }
                else if ( command == "add-group" )
                {
                    Console.WriteLine( "Введите название группы" );
                    string name = Console.ReadLine();
                    groupRepository.Add( new Group
                    {
                        GroupName = name
                    } );
                    Console.WriteLine( "Успешно добавлено" );
                }
                else if ( command == "add student in group")
                {
                    Console.WriteLine( "Введите имя студента с большой буквы" );
                    string name = Console.ReadLine();
                    Student student = studentRepository.GetByName( name );

                    if ( student == null )
                    {
                        Console.WriteLine( "Студент не найден" );
                        continue;
                    }
                    Console.WriteLine();

                    Console.WriteLine( "Введите название группы" );
                    string groupName = Console.ReadLine();
                    Group group = groupRepository.GetByName( groupName );

                    if ( group == null )
                    {
                        Console.WriteLine( "Группа не найдена" );
                        continue;
                    }
                    Console.WriteLine();
                    if ( student != null & group != null )
                    {
                        studentRepository.AddStudentInGroup( groupName, name );
                        Console.WriteLine( "Студент успешно добавлен в группу" );//Подумай как GroupId присвоить Id группы, которая есть в таблице Groups.
                    }
                    else
                    {
                        Console.WriteLine( "Ошибка добавления, проверьте введённые данные" );
                    }
                }
                else if ( command == "get students by group Id" )
                {
                    Console.WriteLine();
                    Console.WriteLine( "Список групп: " );
                    Console.WriteLine();
                    List<Group> groups = groupRepository.GetAll();
                    foreach ( Group group in groups )
                    {
                        Console.WriteLine( $"Id: {group.Id}, Group Name is {group.GroupName}" );
                    }
                    Console.WriteLine();
                    Console.WriteLine( "Введите Id группы" );
                    int groupId = int.Parse( Console.ReadLine() );
                    List<Student> students = studentRepository.GetStudentByGroupId( groupId );
                    Console.WriteLine( $" Студенты в группе " );//Сделай SQL запрос для того, чтобы по Id группы определить имя группы
                    foreach ( Student student in students )
                    {
                        Console.WriteLine( $"Id: {student.Id}, [Name, Age] = [{student.Name},  {student.Age} лет] " );
                    }

                }

                else if ( command == "exit" )
                {
                    break;
                }
            }
        }
    }
}
