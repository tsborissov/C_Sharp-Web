using CarShop.Data;
using System.Linq;

namespace CarShop.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool UserIsMechanic(string userId)
            => this.data
                .Users
                .Any(u => u.Id == userId && u.IsMechanic);

        public bool CarIsOwnedByUser(string carId, string userId)
            => this.data
                .Cars
                .Any(c => c.Id == carId && c.OwnerId == userId);
    }
}
