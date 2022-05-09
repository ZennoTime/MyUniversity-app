using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUniversity.Models;

namespace MyUniversity.Repositories
{
    interface IStudentInGroupRepository
{
        List<StudentGroup> GetAll();
        //List<Student> GetStudentsByGroupId( int groupId );
        Group GetStudentGroup( int studentId );
        bool HaveGroup( int studentId );
        public void AddStudentInGroup( int studentId, int groupId );
        public void UpdateStudentGroup( int studentId, int groupId );
    }
}
