namespace TodoWebAPI.Models
{
    public class CreateAccountResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public User Account { get; set; }
    }
}
