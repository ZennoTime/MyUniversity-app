using System.Collections.Generic;
using MyUniversity.Models;

namespace MyUniversity.Repositories
{
    interface IStudentRepository
    {
        void Add( Student student );
        void DeleteById( int id );
        Student GetById( int id );
        List<Student> GetAll();
        void Update( Student student );
        Student GetByName( string name );
        void AddStudentInGroup( string groupName, string name );
        List<Student> GetStudentByGroupId( int groupId );
    }
}
