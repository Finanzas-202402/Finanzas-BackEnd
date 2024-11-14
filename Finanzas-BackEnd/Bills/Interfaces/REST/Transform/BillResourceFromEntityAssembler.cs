using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Bills.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace Finanzas_BackEnd.Bills.Interfaces.REST.Transform;

public static class BillResourceFromEntityAssembler
{
    public static BillResource ToResourceFromEntity(Bill bill)
    {
        return new BillResource(
            bill.Id,
            bill.Description,
            bill.BillValue,
            bill.Currency.GetDisplayName(),
            bill.RateType.GetDisplayName(),
            bill.RateTime.GetDisplayName(),
            bill.Capitalization.GetDisplayName(),
            bill.RateValue,
            bill.StartDate,
            bill.EndDate,
            bill.ExpirationDate,
            bill.Cancelled,
            bill.UserId);
    }
}