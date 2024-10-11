using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Bills.Domain.Model.Queries;
using Finanzas_BackEnd.Bills.Domain.Repositories;
using Finanzas_BackEnd.Bills.Domain.Services;

namespace Finanzas_BackEnd.Bills.Application.Internal.QueryServices;

public class BillQueryService(IBillRepository billRepository) : IBillQueryService
{
    public async Task<IEnumerable<Bill>> Handle(GetAllBillsQuery query)
    {
        return await billRepository.ListAsync();
    }

    public async Task<Bill?> Handle(GetBillByIdQuery query)
    {
        return await billRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Bill>> Handle(GetBillByUserIdQuery query)
    {
        return await billRepository.FindByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Bill>> Handle(GetBillByUserEmailQuery query)
    {
        return await billRepository.FindByUserEmailAsync(query.UserEmail);
    }
    
    public async Task<IEnumerable<Bill>> Handle(GetBillByUserIdAndCancelledQuery query)
    {
        return await billRepository.FindByUserIdAndCancelledAsync(query.UserId, query.Cancelled);
    }
}