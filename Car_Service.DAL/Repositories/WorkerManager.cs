using Car_Service.DAL.EF;
using Car_Service.DAL.Entities;
using Car_Service.DAL.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Car_Service.DAL.Repositories
{
    public class WorkerManager : IWorkerManager
    {
        private ApplicationContext _db;
        public WorkerManager(ApplicationContext db)
        {
            _db = db;
        }
        public List<Worker> Get()
        {
            return _db.Worker.ToList();
        }
        public void Create(Worker item)
        {
            _db.Worker.Add(item);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}