using Microsoft.AspNetCore.Identity;

namespace Project_Observer_Design_Pattern.DAL
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
    }
}
