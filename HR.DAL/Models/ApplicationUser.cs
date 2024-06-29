using Microsoft.AspNetCore.Identity;
namespace HR.DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsAgree { get; set; }
    }
}
