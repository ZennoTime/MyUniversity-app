using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUniversity.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int StudentId { get; set; }
        public int StudentInGroup { get; set; }

        public Group()
        {
            Id = 0;
            GroupName = "";
            StudentId = 0;
            StudentInGroup = 0;
        }

        

        public override string ToString()
        {
            return $"ID {Id}, Group {GroupName}";
        }
    }
}
