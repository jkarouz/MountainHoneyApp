using DocumentFormat.OpenXml.Wordprocessing;

namespace MountainHoneyApp.Models
{
    public partial class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public User()
        {
            UserName = Name + " " + Surname;

        }
    }


}
