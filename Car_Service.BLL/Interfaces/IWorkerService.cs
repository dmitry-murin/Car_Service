using Car_Service.BLL.DTO;
using Car_Service.BLL.Infrastructure;
using Car_Service.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Service.BLL.Interfaces
{
    public interface IWorkerService : IDisposable
    {
        List<WorkerDTO> GetWorker();

        OperationDetails AddWorker(WorkerDTO model);
        OperationDetails AddWorkTime(WorkTimeDTO model);
        TimesDTO WorkerTimes(int workerId);
        TimesDTO ReservationTimes(int workerId);
    }
}
