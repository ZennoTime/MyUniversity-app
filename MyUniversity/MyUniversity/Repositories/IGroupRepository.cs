using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyUniversity.Models;
using Group = MyUniversity.Models.Group;

namespace MyUniversity.Repositories
{
    public interface IGroupRepository
    {
        List<Group> GetAll();
        Group GetById( int id );
        void Add( Group group );
        //void AddStudentInGroup( Group group );
        Group GetByName( string name );

        //bool Exists( int id );
    }
}
