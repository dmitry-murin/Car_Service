using Car_Service.DAL.EF;
using Car_Service.DAL.Entities;
using Car_Service.DAL.Identity;
using Car_Service.DAL.Interfaces;
using Car_Service.Model.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Service.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IWorkerManager _workerManager;
        private IWorkTimeManager _workTimeManager;
        private IImageManager _imageManager;
        private IReservationManager _reservationManager;
        private IConfirmReservation _confirmReservation;

        private bool _disposed = false;

        public IdentityUnitOfWork(string connectionString)
        {
            _db = new ApplicationContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
            _workerManager = new WorkerManager(_db);
            _workTimeManager = new WorkTimeManager(_db);
            _imageManager = new ImageManager(_db);
            _reservationManager = new ReservationManager(_db);
            _confirmReservation = new ConfirmReservationManager(_db);
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public IWorkerManager WorkerManager
        {
            get { return _workerManager; }
        }

        public IWorkTimeManager WorkTimeManager
        {
            get { return _workTimeManager; }
        }

        public IImageManager ImageManager
        {
            get { return _imageManager; }
        }

        public IReservationManager ReservationManager
        {
            get { return _reservationManager; }
        }
        public IConfirmReservation ConfirmReservationManager
        {
            get { return _confirmReservation; }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _workerManager.Dispose();
                    _workTimeManager.Dispose();
                    _imageManager.Dispose();
                    _reservationManager.Dispose();
                    _confirmReservation.Dispose();
                }
                this._disposed = true;
            }
        }
    }
}
