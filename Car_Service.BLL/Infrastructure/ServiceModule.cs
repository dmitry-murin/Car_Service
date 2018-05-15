using Car_Service.BLL.Interfaces;
using Car_Service.BLL.Services;
using Car_Service.DAL.Repositories;
using Car_Service.Model.Interfaces;
using Ninject.Modules;

namespace Car_Service.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string _connectionString;
        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(_connectionString);
            Bind<IClock>().To<Clock>();
        }
    }
}
