using VehicleServiceMonitoringSystem.Models;

namespace VehicleServiceMonitoringSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> Users = new();

        public void Add(User user)
        {
            user.Id = Users.Count + 1;
            Users.Add(user);
        }

        public User? GetByUsername(string username)
        {
            return Users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool UsernameExists(string username)
        {
            return Users.Any(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}