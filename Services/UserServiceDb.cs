using Microsoft.Extensions.Logging;

using Models;
using DbRepos;

namespace Services;

public class UserServiceDb : IUserService
{
    private readonly UserDbRepos _repo = null;
    private readonly ILogger<UserServiceDb> _logger = null;


    public UserServiceDb(UserDbRepos repo)
    {
        _repo = repo;
    }
    public UserServiceDb(UserDbRepos repo, ILogger<UserServiceDb> logger) : this(repo)
    {
        _logger = logger;
    }
    public async Task<List<IUser>> GetAllUsersAsync() => await _repo.GetAllUsersAsync();
}