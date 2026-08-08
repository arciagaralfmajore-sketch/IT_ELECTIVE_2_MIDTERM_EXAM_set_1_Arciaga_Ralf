using VehicleServiceMonitoringSystem.Models;

namespace VehicleServiceMonitoringSystem.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetByUsername(string username);
        bool UsernameExists(string username);
    }
}