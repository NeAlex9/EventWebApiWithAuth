namespace Events.Services.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public IEnumerable<Role>? Roles { get; set; }
    }
}
