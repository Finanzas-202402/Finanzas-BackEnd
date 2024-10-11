using Finanzas_BackEnd.Shared.Domain.Repositories;
using Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration;

namespace Finanzas_BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync() => await context.SaveChangesAsync();
}