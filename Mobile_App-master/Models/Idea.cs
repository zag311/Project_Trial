using SQLite;

namespace Recraft.Models
{
    public class Idea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ItemType { get; set; }
        public string ImagePath { get; set; }
    }
}