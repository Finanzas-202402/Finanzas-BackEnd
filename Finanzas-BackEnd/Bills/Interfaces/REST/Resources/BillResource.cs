namespace Finanzas_BackEnd.Bills.Interfaces.REST.Resources;

public record BillResource(int Id, decimal BillValue, string Currency, string RateType, string RateTime, 
    string Capitalization, decimal RateValue, DateTime StartDate, DateTime EndDate, DateTime ExpirationDate, bool Cancelled, int UserId);