using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.Shared.Domain.Repositories;

namespace Finanzas_BackEnd.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    bool ExistsByEmail(string email);
}