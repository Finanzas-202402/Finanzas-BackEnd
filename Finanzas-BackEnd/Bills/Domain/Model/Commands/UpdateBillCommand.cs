using Finanzas_BackEnd.Bills.Domain.Model.ValueObjects;

namespace Finanzas_BackEnd.Bills.Domain.Model.Commands;

public record UpdateBillCommand(int Id, decimal BillValue, ECurrency Currency, ERateType RateType, 
    ERateTime RateTime, ECapitalization Capitalization, decimal RateValue, DateTime StartDate, 
    DateTime EndDate, DateTime ExpirationDate, bool Cancelled, int UserId);