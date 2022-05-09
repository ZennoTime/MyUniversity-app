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
        public string Name { get; set; }

        public Group()
        {
            Id = 0;
            Name = "";
        }

        public override string ToString()
        {
            return $"ID {Id}, Group {Name}";
        }
    }
}
