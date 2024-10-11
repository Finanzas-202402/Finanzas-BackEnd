using Finanzas_BackEnd.Bills.Domain.Model.Commands;
using Finanzas_BackEnd.Bills.Domain.Model.ValueObjects;
using Finanzas_BackEnd.Bills.Interfaces.REST.Resources;
using Humanizer;

namespace Finanzas_BackEnd.Bills.Interfaces.REST.Transform;

public static class CreateBillCommandFromResourceAssembler
{
    public static CreateBillCommand ToCommandFromResource(CreateBillResource resource)
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
        
        return new CreateBillCommand(
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