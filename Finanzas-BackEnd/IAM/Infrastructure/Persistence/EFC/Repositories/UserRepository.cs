using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.IAM.Domain.Repositories;
using Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Finanzas_BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_BackEnd.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public bool ExistsByEmail(string email)
    {
        return Context.Set<User>().Any(user => user.Email.Equals(email));
    }
}