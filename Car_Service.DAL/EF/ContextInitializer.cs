using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Car_Service.DAL.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace Car_Service.DAL.EF
{
    public class ContextInitializer: CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            var roles = new List<ApplicationRole>();
            roles.AddRange(new ApplicationRole[] {
                new ApplicationRole { Name = "admin" },
                new ApplicationRole { Name = "user" }
            });
            foreach (var role in roles)
            {
                if (role != null)
                {
                    roleManager.Create(role);
                }
            }

            var admin = new ApplicationUser { Email = "ydn@mail.ru", UserName = "ydn@mail.ru" };
            var result = userManager.Create(admin, "12345Qaz");
            // добавляем роль
            userManager.AddToRole(admin.Id, "admin");
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
