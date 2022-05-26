namespace MyUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Student( int id, int age, string name)
        {
            Id = id;
            Age = age;
            Name = name;
        }

        public Student (string name )
        {
            Name = name;
        }

        public Student ( int age, string name )
        {
            Age = age;
            Name = name;
        }
    }
}
