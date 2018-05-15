using Car_Service.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.DAL.Interfaces
{
    public interface IWorkerManager : IDisposable
    {
        List<Worker> Get();
        void Create(Worker item);
    }
}
