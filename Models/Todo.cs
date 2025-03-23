namespace TodoWebAPI.Models
{
    public class Todo
    {
        public int id { get; set; }
        public string? description { get; set; }
        public bool complete { get; set; }
        public string userId { get; set; }
  
    }
}
