namespace Choice_Ym.Models
{
    public class User : EntityModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public string Password { get; set; }
    }
}