using Car_Service.BLL.Interfaces;
using Car_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_Service.BLL.DTO;
using Car_Service.BLL.Infrastructure;
using static Car_Service.BLL.DTO.ReservationDTO;
using System.IO;
using Car_Service.DAL.Entities;
using Car_Service.Helpers;

namespace Car_Service.BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly string _imagePath = "static";
        IUnitOfWork _database;
        IClock _clock;

        public ReservationService(IUnitOfWork uow, IClock clock)
        {
            _database = uow;
            _clock = clock;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
        public List<ReservationViewDTO> GetReservationHistory(string userID)
        {
            var reservationsView = new List<ReservationViewDTO>();
            var reservations = _database.ReservationManager.Get().Where(s => s.ApplicationUser.Id == userID && s.ConfirmReservation.IsConfirm == true);
            foreach (var reservation in reservations)
            {
                var reservationView = new ReservationViewDTO
                {
                    BreakdownDetails = reservation.BreakdownDetails,
                    DesiredDiagnosis = reservation.DesiredDiagnosis,
                    Purpose = reservation.Purpose,
                    DateStart = DateTime.SpecifyKind(reservation.DateStart, DateTimeKind.Utc),
                    DateEnd = DateTime.SpecifyKind(reservation.DateEnd, DateTimeKind.Utc),
                    NameWorker = string.Format("{0} {1}", reservation.Worker.FirstName, reservation.Worker.SurName),
                    Image = _database.ImageManager.Get(reservation.Id).Select(s => s.URL).ToList()
                };
                reservationsView.Add(reservationView);
            }
            return reservationsView;
        }
        public List<ReservationViewDTO> GetReservation(DateTime date)
        {
            var reservationsView = new List<ReservationViewDTO>();
            var reservations = _database.ReservationManager.Get().Where(s => (
                s.DateStart.Day == date.Day&&s.ConfirmReservation.IsConfirm==true));
            foreach (var reservation in reservations)
            {
                var reservationView = new ReservationViewDTO
                {
                    BreakdownDetails = reservation.BreakdownDetails,
                    DesiredDiagnosis = reservation.DesiredDiagnosis,
                    Purpose = reservation.Purpose,
                    DateStart = DateTime.SpecifyKind(reservation.DateStart, DateTimeKind.Utc),
                    DateEnd = DateTime.SpecifyKind(reservation.DateEnd, DateTimeKind.Utc),
                    NameWorker = string.Format("{0} {1}", reservation.Worker.FirstName, reservation.Worker.SurName),
                    Image = _database.ImageManager.Get(reservation.Id).Select(s => s.URL).ToList()
                };
                reservationsView.Add(reservationView);
            }
            return reservationsView;
        }
        public List<ReservationViewDTO> GetReservationToday()
        {
            var reservationsView = new List<ReservationViewDTO>();
            var reservations =  _database.ReservationManager.Get().Where(s => (
                 s.DateStart.Day == _clock.CurentUtcDateTime().Day
                 || s.DateEnd.Day == _clock.CurentUtcDateTime().Day)
                 && s.ConfirmReservation.IsConfirm);
            foreach(var reservation in reservations)
            {
                var reservationView = new ReservationViewDTO
                {
                    BreakdownDetails = reservation.BreakdownDetails,
                    DesiredDiagnosis = reservation.DesiredDiagnosis,
                    Purpose = reservation.Purpose,
                    DateStart = DateTime.SpecifyKind(reservation.DateStart, DateTimeKind.Utc),
                    DateEnd = DateTime.SpecifyKind(reservation.DateEnd, DateTimeKind.Utc),
                    NameWorker = string.Format("{0} {1}", reservation.Worker.FirstName, reservation.Worker.SurName),
                    Image = _database.ImageManager.Get(reservation.Id).Select(s => s.URL).ToList()
                };
                reservationsView.Add(reservationView);
            }
            return reservationsView;
        }

        public async Task<OperationDetails> Create(ReservationDTO model, string curentUserId, string pathFolder) 
        {
            var curentUser = await _database.UserManager.FindByIdAsync(curentUserId);
            var reservation = new Reservation
            {
                ApplicationUser = curentUser,
                Worker = _database.WorkerManager.Get().Find(s => s.Id == model.WorkerId),
                Purpose = model.Purpose,
                BreakdownDetails = model.BreakdownDetails,
                DesiredDiagnosis = model.DesiredDiagnosis,
                
            };
            var isVerify = await verifyCaptcha(model.Captcha);
            if (!isVerify)
            {
                return new OperationDetails(false, "Error captcha", "");
            }
            else if(model.IsEmergency)
            {
                DateTime startTime;
                DateTime endTime;
                var worker = GetEmergencyTime(out startTime, out endTime);
                reservation.DateStart = startTime;
                reservation.DateEnd = endTime;
                reservation.Worker = _database.WorkerManager.Get().Find(s => s.Id == worker);
            }
            else
            {
                if(await verifyTime(model.WorkerId, model.StartTime, model.EndTime))
                {
                    reservation.DateStart = model.StartTime;
                    reservation.DateEnd = model.EndTime;
                }
                else
                    return new OperationDetails(false, "Error date", "");
            }
            _database.ReservationManager.Create(reservation);
            await UploadImage(model.File, reservation, pathFolder);
            var confirmReservation = new ConfirmReservation { Id = reservation.Id, Guid = Guid.NewGuid(), Reservation = reservation, IsConfirm = false, ExpireDate = _clock.CurentUtcDateTime().AddMinutes(5)};
            _database.ConfirmReservationManager.Create(confirmReservation);
            SendEmail.ConfirmReservation(reservation, confirmReservation);
            return new OperationDetails(true, "", "");
        }
        private int GetEmergencyTime(out DateTime dateStart, out DateTime dateEnd)
        {
            var minFreeTime =new Dictionary<int, DateTime>(); 
            var workers = _database.WorkerManager.Get();
            foreach(var worker in workers)
            {
                var workTimeWorker = _database.WorkTimeManager.Get().Where(s => s.Worker.Id == worker.Id&&s.DateEnd>=_clock.CurentUtcDateTime()).ToList();
                if (workTimeWorker.Count!=0)
                {
                    var curentTime = new DateTime(((DateTime.Now.Ticks + TimeSpan.FromMinutes(60).Ticks - 1) / TimeSpan.FromMinutes(60).Ticks) * TimeSpan.FromMinutes(60).Ticks).ToUniversalTime();
                    var minTime = (workTimeWorker.Find(s=> curentTime >= s.DateStart&& curentTime < s.DateEnd)!=null)? curentTime : workTimeWorker.Where(s=>s.DateStart>_clock.CurentUtcDateTime()).Min(s => s.DateStart);
                    while (minTime != null)
                    {
                        var reservationTime = _database.ReservationManager.Get().Find(s => (minTime >= s.DateStart && minTime < s.DateEnd)&& (s.ConfirmReservation.IsConfirm || s.ConfirmReservation.ExpireDate >= _clock.CurentUtcDateTime()));
                        if (reservationTime != null)
                        {
                            if (workTimeWorker.Any(s => reservationTime.DateEnd >= s.DateStart && reservationTime.DateEnd < s.DateEnd))
                                minTime = reservationTime.DateEnd;
                            else
                                minTime = workTimeWorker.Where(s => s.DateStart >= reservationTime.DateEnd).Min(s => s.DateEnd);
                        }
                        else
                        {
                            minFreeTime.Add(worker.Id, minTime);
                            break;
                        }
                    }
                }
            }
            var result = minFreeTime.First(s => s.Value == minFreeTime.Min(k => k.Value));
            dateStart = DateTime.SpecifyKind(result.Value, DateTimeKind.Utc);
            dateEnd= DateTime.SpecifyKind(dateStart.AddHours(1), DateTimeKind.Utc);
            return result.Key;
        }
        private async Task UploadImage(List<ImageDTO> images, Reservation reservation, string pathFolder)
        {
            int i = 1;
            foreach (var image in images)
            {
                string fileName = string.Format("{0}_{1}{2}", reservation.Id, i, image.Extension);
                string path = string.Format("{0}/{1}/{2}",pathFolder, _imagePath, fileName);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    await fs.WriteAsync(image.ImageBytes, 0, image.ImageBytes.Length);
                    _database.ImageManager.Create(new Image { URL = string.Format("{0}/{1}", _imagePath, fileName), Reservation = reservation });
                }
                i++;
            }
        }
        private async Task<bool> verifyCaptcha(string captcha)
        {
            var responce = await ReCaptcha.GetRespons(captcha);
            return ReCaptcha.Validate(responce); ;
        }
        private async Task<bool> verifyTime(int workerId,DateTime dateStart, DateTime dateEnd)
        {
            if (!(dateStart.Date>=_clock.CurentUtcDateTime().Date&&dateStart.Hour>=_clock.CurentUtcDateTime().Hour))
                return false;
            var workTimes = _database.WorkTimeManager.Get().Where(s => s.Worker.Id == workerId ).ToList();
            var reservationTimes = _database.ReservationManager.Get().Where(s => s.Worker.Id == workerId&&(s.ConfirmReservation.IsConfirm||s.ConfirmReservation.ExpireDate>=_clock.CurentUtcDateTime())).ToList();
            if (workTimes.Where(s => dateStart >= s.DateStart && dateStart < s.DateEnd && dateEnd > s.DateStart && dateEnd <= s.DateEnd).Count() != 1)
                return false;
            else if (reservationTimes.Where(s=>((dateStart >= s.DateStart) && (dateStart < s.DateEnd)) || ((dateEnd > s.DateStart) && (dateEnd <= s.DateEnd)) || ((s.DateStart >= dateStart) && (s.DateEnd <= dateEnd))).Count()!=0)
            {
                return false;
            }
            else return true;
        }
        public OperationDetails Confirm(Guid guid)
        {
            var entity = _database.ConfirmReservationManager.Get(guid);
            if(entity!=null
                && _clock.CurentUtcDateTime()<=DateTime.SpecifyKind(entity.ExpireDate, DateTimeKind.Utc)
                && !entity.IsConfirm)
            {
                entity.IsConfirm = true;
                _database.ConfirmReservationManager.Update(entity);
                return new OperationDetails(true, "", "");
            }
            else 
                return new OperationDetails(false, "Error guid or time extire", "");
        }
    }
}
 