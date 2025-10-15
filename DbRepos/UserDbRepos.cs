using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using DbModels;
using DbContext;

namespace DbRepos;

public class UserDbRepos
{
    private ILogger<UserDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    public UserDbRepos(ILogger<UserDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    public async Task<List<IUser>> GetAllUsersAsync()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.CommentsDbM)
            .ToListAsync<IUser>();
    }
}