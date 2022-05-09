using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUniversity.Models
{
    public class StudentGroup
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }

        public StudentGroup()
        {
            StudentId = 0;
            GroupId = 0;
        }

        public override string ToString()
        {
            return $"Student width ID: {StudentId} is in group width ID: {GroupId}";
        }
    }
}
