using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyUniversity.Models;

namespace MyUniversity.Repositories
{
    class GroupRawSqlRepository : IGroupRepository
    {
        private readonly string _connectionString;
        public GroupRawSqlRepository( string connectionString )
        {
            _connectionString = connectionString;
        }
        public List<Group> GetAll()
        {
            var result = new List<Group>();

            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText = "select [Id], [GroupName] from [Groups]";

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            result.Add( new Group
                            {
                                Id = Convert.ToInt32( reader[ "Id" ] ),
                                GroupName = Convert.ToString( reader[ "GroupName" ] )
                            } );
                        }
                    }
                }
            }
            return result;
        }
        public void Add( Group group )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"insert into [Groups]
                            ([GroupName])
                        values
                            (@groupname)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add( "@groupname", SqlDbType.NVarChar ).Value = group.GroupName;

                    group.Id = Convert.ToInt32( command.ExecuteScalar() );
                }
            }
        }
        public Group GetById( int id )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [Id], [GroupName]
                        from [Groups]
                        where [Id] = @id";

                    command.Parameters.Add( "@id", SqlDbType.Int ).Value = id;
                    using ( var reader = command.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            return new Group
                            {
                                Id = Convert.ToInt32( reader[ "Id" ] ),
                                GroupName = Convert.ToString( reader[ "GroupName" ] )
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
        /*        public void AddStudentInGroup( Group group )
                {
                    using ( var connection = new SqlConnection( _connectionString ) )
                    {
                        connection.Open();
                        using ( SqlCommand command = connection.CreateCommand() )
                        {
                            command.CommandText =
                                @"insert into [Groups] (StudentId)
                                Select Id From Student
                                values
                                    (@id)
                                select SCOPE_IDENTITY()";

                            group.Id = Convert.ToInt32( command.ExecuteScalar() );// РАЗБЕРИСЬ С ЭТИМ БЛОКОМ
                        }
                    }
                }*/

        //ЗАПАСНОЙ ПЛАН
        public Group GetByName( string name )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [GroupName]
                        from [Groups]
                        where [GroupName] = @groupname";


                    command.Parameters.Add( "@groupname", SqlDbType.NVarChar ).Value = name;
                    using ( var reader = command.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            return new Group
                            {
                                GroupName = Convert.ToString( reader[ "GroupName" ] )

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
    }
}
