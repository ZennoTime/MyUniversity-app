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

        public Group()
        {
            Id = 0;
            GroupName = "";
        }

        

        public override string ToString()
        {
            return $"ID {Id}, Group {GroupName}";
        }
    }
}
