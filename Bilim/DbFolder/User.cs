using Microsoft.AspNetCore.Identity;
namespace Bilim.DbFolder
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
