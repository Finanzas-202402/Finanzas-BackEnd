using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Bills.Domain.Model.Queries;

namespace Finanzas_BackEnd.Bills.Domain.Services;

public interface IBillQueryService
{
    Task<IEnumerable<Bill>> Handle(GetAllBillsQuery query);
    Task<Bill?> Handle(GetBillByIdQuery query);
    Task<IEnumerable<Bill>> Handle(GetBillByUserIdQuery query);
    Task<IEnumerable<Bill>> Handle(GetBillByUserEmailQuery query);
    Task<IEnumerable<Bill>> Handle(GetBillByUserIdAndCancelledQuery query);
}