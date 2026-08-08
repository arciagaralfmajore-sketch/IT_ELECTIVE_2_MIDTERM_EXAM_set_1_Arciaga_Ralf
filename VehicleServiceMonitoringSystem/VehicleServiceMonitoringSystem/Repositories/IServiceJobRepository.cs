using VehicleServiceMonitoringSystem.Models;

namespace VehicleServiceMonitoringSystem.Repositories
{
    public interface IServiceJobRepository
    {
        List<ServiceJob> GetAll();
        ServiceJob? GetById(int id);
        void Add(ServiceJob serviceJob);
        void Update(ServiceJob serviceJob);
        void Release(int id);
    }
}