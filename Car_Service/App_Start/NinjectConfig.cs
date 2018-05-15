using System;
using System.Reflection;
using Ninject;
using Car_Service.BLL.Interfaces;
using Car_Service.BLL.Services;
using Ninject.Modules;
using Car_Service.BLL.Infrastructure;

namespace Car_Service.App_Start
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var modules = new INinjectModule[] {
                new ServiceModule(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString) 
            };
            var kernel = new StandardKernel(modules);
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterServices(KernelBase kernel)
        { 
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IWorkerService>().To<WorkerService>();
            kernel.Bind<IReservationService>().To<ReservationService>();
        }
    }
}