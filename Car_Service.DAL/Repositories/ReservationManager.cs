using Car_Service.DAL.EF;
using Car_Service.DAL.Entities;
using Car_Service.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Car_Service.DAL.Repositories
{
    public class ReservationManager : IReservationManager
    {
        private ApplicationContext _db;
        public ReservationManager(ApplicationContext db)
        {
            _db = db;
        }
        public List<Reservation> Get()
        {
            return _db.Reservation.ToList();
        }
        public void Create(Reservation item)
        {
            _db.Reservation.Add(item);
            _db.SaveChanges();
        }

        public void Dispose()
        { 
            _db.Dispose();
        }
    }
}