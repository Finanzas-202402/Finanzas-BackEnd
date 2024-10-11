using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Shared.Domain.Repositories;

namespace Finanzas_BackEnd.Bills.Domain.Repositories;

public interface IBillRepository : IBaseRepository<Bill>
{
    Task<IEnumerable<Bill>> FindByUserIdAsync(int userId);
    bool ExistsByUserId(int userId);
    Task<IEnumerable<Bill>> FindByUserEmailAsync(string userEmail);
    bool ExistsByUserEmail(string userEmail);
    Task<IEnumerable<Bill>> FindByUserIdAndCancelledAsync(int userId, bool cancelled);
}