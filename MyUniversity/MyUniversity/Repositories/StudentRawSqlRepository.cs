using System;
using System.Collections.Generic;
using MyUniversity.Models;
using System.Data;
using System.Data.SqlClient;

namespace MyUniversity.Repositories
{
    class StudentRawSqlRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRawSqlRepository( string connectionString )
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
                        while ( reader.Read() )
                        {
                            result.Add( new Student
                            {
                                Id = Convert.ToInt32( reader[ "Id" ] ),
                                Name = Convert.ToString( reader[ "Name" ] ),
                                Age = Convert.ToInt32( reader[ "Age" ] )
                            } );
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
                            {
                                Id = Convert.ToInt32( reader[ "Id" ] ),
                                Name = Convert.ToString( reader[ "Name" ] ),
                                Age = Convert.ToInt32( reader[ "Age" ] )
                            };
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

        public void UpdateGroup( Student student )
        {
            string sqlExpression = $@"update[ Student ] set[ GroupId ] = ( select Student.groupId from Student, where[ Id ] = @id AND Student.groupId = groupId";
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                using ( SqlCommand command = new SqlCommand( sqlExpression, connection ) )
                {
                    connection.Open();
                    SqlParameter groupId = new SqlParameter();

                    command.Parameters.Add( groupId );
                    SqlDataReader reader = command.ExecuteReader();

                    command.Parameters.Add( "@groupid", SqlDbType.Int ).Value = student.GroupId;

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
                            return new Student
                            {
                                Name = Convert.ToString( reader[ "Name" ] )
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void AddStudentInGroup( Student student )
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
    }
}
