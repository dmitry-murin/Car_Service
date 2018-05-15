using Car_Service.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace Car_Service.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {

        }
    }
}
