namespace CarShop.Services
{
    public interface IUserService
    {
        bool UserIsMechanic(string userId);

        bool CarIsOwnedByUser(string carId, string userId);
    }
}
