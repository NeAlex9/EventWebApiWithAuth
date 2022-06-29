namespace Auth.Services.Entities
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
