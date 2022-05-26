using System.Collections.Generic;
using Group = MyUniversity.Models.Group;

namespace MyUniversity.Repositories
{
    public interface IGroupRepository
    {
        List<Group> GetAll();
        void Add( Group group );
        Group GetByName( string name );
    }
}
