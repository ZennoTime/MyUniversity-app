using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUniversity.Models;

namespace MyUniversity.Repositories
{
    class StudentGroupRawSqlRepository : IStudentInGroupRepository
    {
        private readonly string _connectionString;
        public StudentGroupRawSqlRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public List<StudentGroup> GetAll()
        {
            var result = new List<StudentGroup>();

            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText = "select [StudentId], [GroupId] from [StudentGroup]";

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            result.Add( new StudentGroup
                            {
                                StudentId = Convert.ToInt32( reader[ "Id" ] ),
                                GroupId = Convert.ToInt32( reader[ "Age" ] )
                            } );
                        }
                    }
                }
            }

            return result;
        }
    }
}
