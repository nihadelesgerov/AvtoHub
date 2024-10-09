using Microsoft.AspNetCore.Identity;

namespace AvtoHubProject.Models
{
    public class AvtoHubUser : IdentityUser
    {
        public ICollection<Product> Products { get; set; }
    }
}
