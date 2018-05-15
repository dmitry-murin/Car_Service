using Car_Service.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Car_Service.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Image> Image { get; set; }
        public DbSet<WorkTime> WorkTime { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<ConfirmReservation> ConfirmReservation { get; set; }

        public ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new ContextInitializer());
        }
        public ApplicationContext(string conectionString) : base(conectionString) {
            
        }
    }
}