using Microsoft.AspNetCore.Identity;

namespace MVC_Authentication.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string? profileImage { get; set; }

    }
}
