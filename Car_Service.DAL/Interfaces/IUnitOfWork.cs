using Car_Service.DAL.Identity;
using Car_Service.DAL.Interfaces;
using System;
using System.Threading.Tasks;


namespace Car_Service.Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IWorkerManager WorkerManager { get; }
        IWorkTimeManager WorkTimeManager { get; }
        IImageManager ImageManager { get; }
        IReservationManager ReservationManager { get; }
        IConfirmReservation ConfirmReservationManager { get; }
        Task SaveAsync();

    }
}