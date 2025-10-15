using Models;

namespace Services;

public interface IUserService
{
    public Task<List<IUser>> GetAllUsersAsync();
}