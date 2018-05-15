using Car_Service.BLL.Interfaces;
using Car_Service.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car_Service.BLL.Infrastructure;
using Car_Service.DAL.Entities;
using System.Linq;
using Car_Service.BLL.DTO;
using System.Collections;
using System;
using System.Globalization;

namespace Car_Service.BLL.Services
{
    public class WorkerService: IWorkerService
    {
        IUnitOfWork Database { get; set; }
        IClock Clock { get; set; }

        public WorkerService(IUnitOfWork uow, IClock clock)
        {
            Database = uow;
            Clock = clock;
        }
        public List<WorkerDTO> GetWorker()
        {
            return Database.WorkerManager.Get().Select(s=>new WorkerDTO { Id=s.Id, Name=s.FirstName, SurName=s.SurName, Telephone=s.Telephone, Email=s.Email}).ToList();
        }
        public OperationDetails AddWorker(WorkerDTO model)
        { 
            var worker =  Database.WorkerManager.Get().Find(s=>s.Email==model.Email);
            if(worker==null)
            {
                worker = new Worker
                {
                    FirstName = model.Name,
                    SurName = model.SurName,
                    Telephone = model.Telephone,
                    Email = model.Email
                };
                Database.WorkerManager.Create(worker);
                return new OperationDetails(true, "Рабочий успешно добавлен", "");
            }
            return new OperationDetails(false, "Рабочий с таким e-mail уже существует", "Email");
        }
        public OperationDetails AddWorkTime(WorkTimeDTO model)
        {
            DateTime curentDate = Clock.CurentUtcDateTime();
            Worker worker = Database.WorkerManager.Get().Find(s => s.Id == model.UserId);
            if (worker == null)
                return new OperationDetails(false, "Рабочий не найден", "");
            else if(model.StartTime < curentDate || model.EndTime < model.StartTime)
                return new OperationDetails(false, "Ошибка даты", "");
            var workerWorkTime = Database.WorkTimeManager.Get().Where(s => (s.Worker == worker));
            foreach (var x in workerWorkTime)
            {
                var dateStart = DateTime.SpecifyKind(x.DateStart, DateTimeKind.Utc);
                var dateEnd = DateTime.SpecifyKind(x.DateEnd, DateTimeKind.Utc);
                if ((
                    (model.StartTime >= dateStart) && (model.StartTime < dateEnd))
                    || ((model.EndTime > dateStart) && (model.EndTime <= dateEnd))
                    || ((dateStart >= model.StartTime) && (dateEnd <= model.EndTime)))
                    return new OperationDetails(false, "Уже работает в эту дату", "");
            }
            WorkTime workTime = new WorkTime{
                DateStart = model.StartTime,
                DateEnd = model.EndTime,
                Worker=worker
            };
            Database.WorkTimeManager.Create(workTime);
            return new OperationDetails(true, "Время работы успешно добавлено", "");
        }
        public TimesDTO WorkerTimes(int workerId)
        {
            var worker = Database.WorkerManager.Get().Find(s => s.Id == workerId);
            if (worker != null) 
            {
                var workTime = Database.WorkTimeManager.Get().Where(s => s.Worker.Id == workerId
                && (s.DateEnd.Date>=Clock.CurentUtcDateTime().Date))
                .Select(s=> { return new TimeDTO {
                    StartTime = DateTime.SpecifyKind(s.DateStart, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc)};
                }).ToList<TimeDTO>();
                return new TimesDTO
                {
                    WorkerId = workerId,
                    Times = workTime
                };
            }
            else
                return null;
        }
        public TimesDTO ReservationTimes(int workerId)
        {
            var worker = Database.WorkerManager.Get().Find(s => s.Id == workerId);
            if (worker != null)
            {
                var workTime = Database.ReservationManager.Get()
                    .Where(s => s.Worker.Id == workerId 
                    && (s.DateEnd.Date >= Clock.CurentUtcDateTime().Date)
                    && (s.ConfirmReservation.IsConfirm 
                        || s.ConfirmReservation.ExpireDate >= Clock.CurentUtcDateTime()))
                    .Select(s => { return new TimeDTO {
                        StartTime = DateTime.SpecifyKind(s.DateStart, DateTimeKind.Utc),
                        EndTime = DateTime.SpecifyKind(s.DateEnd, DateTimeKind.Utc) }; })
                    .ToList<TimeDTO>();
                return new TimesDTO
                {
                    WorkerId = workerId,
                    Times = workTime
                };
            }
            else
                return null;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        
    }
}
