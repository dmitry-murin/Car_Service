using Car_Service.DAL.EF;
using Car_Service.DAL.Entities;
using Car_Service.DAL.Interfaces;
using System.Data.Entity;
using System;
using System.Linq;

namespace Car_Service.DAL.Repositories
{
    public class ConfirmReservationManager : IConfirmReservation
    {
        private ApplicationContext _db;
        public ConfirmReservationManager(ApplicationContext db)
        {
            _db = db;
        }
        public ConfirmReservation Get(Guid guid)
        {
           return _db.ConfirmReservation.FirstOrDefault(s=>s.Guid==guid);
        }
        public void Create(ConfirmReservation item)
        {
            _db.ConfirmReservation.Add(item);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Update(ConfirmReservation item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        
    }
}
