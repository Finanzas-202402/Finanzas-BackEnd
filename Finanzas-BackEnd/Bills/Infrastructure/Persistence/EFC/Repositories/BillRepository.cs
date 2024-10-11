using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Bills.Domain.Repositories;
using Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Finanzas_BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_BackEnd.Bills.Infrastructure.Persistence.EFC.Repositories;

public class BillRepository(AppDbContext context) : BaseRepository<Bill>(context), IBillRepository
{
    public async Task<IEnumerable<Bill>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Bill>().Where(bill => bill.UserId.Equals(userId)).ToListAsync();
    }

    public bool ExistsByUserId(int userId)
    {
        return Context.Set<Bill>().Any(bill => bill.UserId.Equals(userId));
    }

    public async Task<IEnumerable<Bill>> FindByUserEmailAsync(string userEmail)
    {
        return await Context.Set<Bill>().Where(bill => bill.User.Email.Equals(userEmail)).ToListAsync();
    }

    public bool ExistsByUserEmail(string userEmail)
    {
        return Context.Set<Bill>().Any(bill => bill.User.Email.Equals(userEmail));
    }
    
    public async Task<IEnumerable<Bill>> FindByUserIdAndCancelledAsync(int userId, bool cancelled)
    {
        return await Context.Set<Bill>().Where(bill => bill.UserId.Equals(userId) && bill.Cancelled.Equals(cancelled)).ToListAsync();
    }
}