using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int GroupId { get; set; }

        public Student()
        {
            Id = 0;
            Name = "";
            Age = 0;
            GroupId = 0;

        }

        public override string ToString()
        {
            return $"Student: (ID: {Id}, name: {Name}, age: {Age}, GroupId : {GroupId})";
        }
    }
}
