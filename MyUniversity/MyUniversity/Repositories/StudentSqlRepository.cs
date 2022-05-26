using System;
using System.Collections.Generic;
using MyUniversity.Models;
using System.Data;
using System.Data.SqlClient;

namespace MyUniversity.Repositories
{
    class StudentSqlRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentSqlRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAll()
        {
            var result = new List<Student>();
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText = "select [Id], [Name], [Age] from [Student]";
                    using ( var reader = command.ExecuteReader() )
                    {
                        if ( reader.HasRows )
                        {
                            while ( reader.Read() )
                            {
                                result.Add( new Student
                                (
                                    Convert.ToInt32( reader[ "Id" ] ),
                                    Convert.ToInt32( reader[ "Age" ] ),
                                    Convert.ToString( reader[ "Name" ] )

                                ));
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine( "Студентов нет в Базе Данных" );
                        }
                    }
                }
            }
            return result;
        }

        public void Add( Student student )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"insert into [Student]
                            ([Name], [Age])
                        values
                            ((@name), (@age))
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = student.Name;
                    command.Parameters.Add( "@age", SqlDbType.Int ).Value = student.Age; 

                    student.Id = Convert.ToInt32( command.ExecuteScalar() );
                }
            }
        }

        public Student GetById( int id )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [Id], [Name], [Age]
                        from [Student]
                        where [Id] = @id";

                    command.Parameters.Add( "@id", SqlDbType.Int ).Value = id;
                    using ( var reader = command.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            return new Student
                            (
                                Convert.ToInt32( reader[ "Id" ] ),
                                Convert.ToInt32( reader[ "Age" ] ),
                                Convert.ToString( reader[ "Name" ] )
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void Update( Student student )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"update [Student]
                        set [Name] = @name,
                        [Age] = @age
                        where [Id] = @id";

                    command.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = student.Name;
                    command.Parameters.Add( "@id", SqlDbType.Int ).Value = student.Id;
                    command.Parameters.Add( "@age", SqlDbType.Int ).Value = student.Age;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteById( int id )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"delete [Student]
                        where [Id] = @id";

                    command.Parameters.Add( "@id", SqlDbType.Int ).Value = id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public Student GetByName( string name )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [Name]
                        from [Student]
                        where [Name] = @name";

                    command.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = name;
                    using ( var reader = command.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            return new Student( Convert.ToString( reader[ "Name" ] ) );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void AddStudentInGroup( string groupName, string name )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"update [Student]
                        set [GroupId] = ( 
                            select Groups.Id
                            from Groups
                            where Groups.GroupName = @groupname
                            )
                        where Student.Name = @name";

                    command.Parameters.Add( "@groupname", SqlDbType.NVarChar ).Value = groupName;
                    command.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = name;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetStudentByGroupId( int groupId )
        {
            var result = new List<Student>();

            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"Select Name, Id, Age
                          From Student
                          where GroupId = @groupId";

                    command.Parameters.Add( "@groupId", SqlDbType.Int ).Value = groupId;
                    using ( var reader = command.ExecuteReader() )
                    {
                        if ( reader.HasRows )
                        {
                            while ( reader.Read() )
                            {
                                result.Add( new Student
                                (
                                    Convert.ToInt32( reader[ "Id" ] ),
                                    Convert.ToInt32( reader[ "Age" ] ),
                                    Convert.ToString( reader[ "Name" ] )
                                ));

                                Console.WriteLine();
                                Console.WriteLine( $"Студенты в группе с Id: {groupId}" );
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine( $"Ни один студент не состоит в группе с Id: {groupId}" );
                        }
                    }
                    command.ExecuteNonQuery();
                }   
            }
            return result;
        }
    }
}
