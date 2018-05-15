using System;
using System.Collections.Generic;
using Car_Service.DAL.EF;
using Car_Service.DAL.Entities;
using Car_Service.DAL.Interfaces;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Car_Service.DAL.Repositories
{
    public class WorkTimeManager : IWorkTimeManager
    {
        private ApplicationContext _db;
        public WorkTimeManager(ApplicationContext db)
        {
            _db = db;
        }
        public List<WorkTime> Get()
        {
            return _db.WorkTime.ToList();
        }
        public void Create(WorkTime item)
        {
            _db.WorkTime.Add(item);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        
    }
}
