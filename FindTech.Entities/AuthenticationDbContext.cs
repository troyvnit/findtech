using FindTech.Entities.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FindTech.Entities
{
    public class AuthenticationDbContext : IdentityDbContext<FindTechUser>
    {
        public AuthenticationDbContext()
            : base("FindTechContext", throwIfV1Schema: false)
        {
        }

        public static AuthenticationDbContext Create()
        {
            return new AuthenticationDbContext();
        }
    }
}