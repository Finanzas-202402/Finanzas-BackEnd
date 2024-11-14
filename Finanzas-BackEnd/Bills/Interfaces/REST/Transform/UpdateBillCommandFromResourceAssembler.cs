using Finanzas_BackEnd.Bills.Domain.Model.Commands;
using Finanzas_BackEnd.Bills.Domain.Model.ValueObjects;
using Finanzas_BackEnd.Bills.Interfaces.REST.Resources;

namespace Finanzas_BackEnd.Bills.Interfaces.REST.Transform;

public static class UpdateBillCommandFromResourceAssembler
{
    public static UpdateBillCommand ToCommandFromResource(UpdateBillResource resource)
    {
        if (!Enum.TryParse<ECurrency>(resource.Currency, true, out var currency))
        {
            throw new ArgumentException($"Invalid value for Currency: {resource.Currency}");
        }

        if (!Enum.TryParse<ERateType>(resource.RateType, true, out var rateType))
        {
            throw new ArgumentException($"Invalid value for RateType: {resource.RateType}");
        }

        if (!Enum.TryParse<ERateTime>(resource.RateTime, true, out var rateTime))
        {
            throw new ArgumentException($"Invalid value for RateTime: {resource.RateTime}");
        }

        if (!Enum.TryParse<ECapitalization>(resource.Capitalization, true, out var capitalization))
        {
            throw new ArgumentException($"Invalid value for Capitalization: {resource.Capitalization}");
        }   
        
        return new UpdateBillCommand(
            resource.Id,
            resource.Description,
            resource.BillValue,
            currency,
            rateType,
            rateTime,
            capitalization,
            resource.RateValue,
            resource.StartDate,
            resource.EndDate,
            resource.ExpirationDate,
            resource.Cancelled,
            resource.UserId);
    }
}