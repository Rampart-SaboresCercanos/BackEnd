using Microsoft.EntityFrameworkCore;
using BackEnd.IAM.Domain.Model.Aggregates;
using BackEnd.IAM.Domain.Repositories;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace BackEnd.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// User repository implementation. 
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> database context.
/// </param>
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    // inheritedDoc
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    // inheritedDoc
    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }
}