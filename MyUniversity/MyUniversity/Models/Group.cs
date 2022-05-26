namespace MyUniversity.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public Group( string groupName, int id)
        {
            Id = id;
            GroupName = groupName;
        }

        public Group (string groupName )
        {
            GroupName = groupName;
        }
    }
}
