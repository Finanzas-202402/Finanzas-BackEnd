using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Bills.Domain.Model.Commands;

namespace Finanzas_BackEnd.Bills.Domain.Services;

public interface IBillCommandService
{
    Task<Bill?> Handle(CreateBillCommand command);
    Task<Bill?> Handle(UpdateBillCommand command);
    Task<bool> Handle(DeleteBillCommand command);
}