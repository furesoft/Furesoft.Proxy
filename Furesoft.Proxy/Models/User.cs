namespace Furesoft.Proxy.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}