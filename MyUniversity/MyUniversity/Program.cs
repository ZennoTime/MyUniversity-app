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
            IStudentRepository studentRepository = new StudentSqlRepository( _connectionString );
            IGroupRepository groupRepository = new GroupSqlRepository( _connectionString );

            var helpcommands = new List<string>()
            {   "Доступные команды:","","Get запросы: ",
                    "  get-students - показать список студентов",
                    "  get-groups - показать список групп",
                    "  get students by group Id - вывести список студентов по Id их группы ", "",
                "Add - запросы: ",
                    "  add-student - добавить студента",
                    "  add-group - добавить группу",
                    "  add student in group - добавить студента в группу", "",
                "Запросы состояния элементов: ",
                    "  update-student - изменить информацию о студенте",
                    "  delete-student - удалить студента", "",
                "help - вывести список команд",
                "exit - выйти из приложения"
            };

            foreach ( var helpcommand in helpcommands )
            {
                Console.WriteLine( helpcommand );
            }

            while ( true )
            {
                string command = Console.ReadLine();
                if (command != "exit")
                {
                    switch ( command )
                    {
                        case "help":
                            {
                                foreach ( var helpcommand in helpcommands )
                                {
                                    Console.WriteLine( helpcommand );
                                }
                                break;
                            }
                        case "get-students":
                            {
                                List<Student> students = studentRepository.GetAll();
                                foreach ( Student student in students )
                                {
                                    Console.WriteLine( $"Id: {student.Id}, [Name, Age] = [{student.Name},  {student.Age} лет] " );
                                }
                                break;
                            }
                        case "add-student":
                            {
                                Console.WriteLine( "Введите имя студента" );
                                string name = Console.ReadLine();

                                Console.WriteLine( "Введите возраст студента" );
                                int age = Convert.ToInt32( Console.ReadLine() );

                                studentRepository.Add( new Student( age, name ) );
                                Console.WriteLine( "Успешно добавлено" );

                                break;
                            }
                        case "update-student":
                            {
                                Console.WriteLine( "Введите Id студента" );
                                int studentId = int.Parse( Console.ReadLine() );

                                Student student = studentRepository.GetById( studentId );
                                if ( student == null )
                                {
                                    Console.WriteLine( "Студент не найден" );
                                    continue;
                                }

                                bool FlagUpdate = true;
                                bool FlagAge = true;

                                while ( FlagUpdate )
                                {
                                    Console.WriteLine( "Введите новое имя студента" );
                                    student.Name = Console.ReadLine();

                                    if ( student.Name == String.Empty )
                                    {
                                        Console.WriteLine( "" );
                                        Console.WriteLine( "Неверно введено имя" );
                                        Console.WriteLine( "         ↓         " );
                                    }
                                    else
                                    {
                                        Console.WriteLine( "Введите возраст студента" );
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

                                break;
                            }
                        case "delete-student":
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

                                break;
                            }
                        case "get-groups":
                            {
                                List<Group> groups = groupRepository.GetAll();
                                foreach ( Group group in groups )
                                {
                                    Console.WriteLine( $"Id: {group.Id}, Group Name is {group.GroupName}" );
                                }
                                break;
                            }
                        case "add-group":
                            {
                                Console.WriteLine( "Введите название группы" );
                                string name = Console.ReadLine();

                                groupRepository.Add( new Group( name ) );
                                Console.WriteLine( "Успешно добавлено" );

                                break;
                            }
                        case "add student in group":
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

                                break;
                            }
                        case "get students by group Id":
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

                                foreach ( Student student in students )
                                {
                                    Console.WriteLine( $"Id: {student.Id}, [Name, Age] = [{student.Name},  {student.Age} лет] " );
                                }

                                break;
                            }
                        default:
                            {
                                Console.WriteLine();
                                Console.WriteLine( "Команда не найденна" );

                                break;
                            }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}