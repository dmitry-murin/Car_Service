namespace Car_Service.DAL.Migrations
{
    using Car_Service.DAL.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Car_Service.DAL.EF.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Car_Service.DAL.EF.ApplicationContext context)
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
                 roleManager.Create(role);
            }

            var admin = new ApplicationUser { Email = "ydn@mail.ru", UserName = "ydn@mail.ru" };
            var result = userManager.Create(admin, "12345Qaz");
            // добавляем роль
            userManager.AddToRole(admin.Id, "admin");
            context.SaveChanges();
        }
    }
}
