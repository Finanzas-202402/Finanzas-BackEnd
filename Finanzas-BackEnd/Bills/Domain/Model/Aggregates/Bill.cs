using Finanzas_BackEnd.Bills.Domain.Model.Commands;
using Finanzas_BackEnd.Bills.Domain.Model.ValueObjects;
using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;

namespace Finanzas_BackEnd.Bills.Domain.Model.Aggregates;

public class Bill
{
    public int Id { get; }
    
    public decimal BillValue { get; private set; }
    
    public ECurrency Currency { get; protected set; }
    
    public ERateType RateType { get; protected set; }

    public ERateTime RateTime { get; protected set; }
    
    public ECapitalization Capitalization { get; protected set; }
    
    public decimal RateValue { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    public DateTime ExpirationDate { get; private set; }
    
    public bool Cancelled { get; private set; }
    
    public int UserId { get; set; } // Foreign key
    
    public User User { get; set; } // Navigation property

    public Bill(decimal billValue, ECurrency currency, ERateType rateType, ERateTime rateTime, ECapitalization capitalization, decimal rateValue, DateTime startDate, DateTime endDate, DateTime expirationDate, bool cancelled, int userId)
    {
        BillValue = billValue;
        Currency = currency;
        RateType = rateType;
        RateTime = rateTime;
        Capitalization = capitalization;
        RateValue = rateValue;
        StartDate = startDate;
        EndDate = endDate;
        ExpirationDate = expirationDate;
        Cancelled = cancelled;
        UserId = userId;
    }
    
    public Bill(CreateBillCommand command)
    {
        BillValue = command.BillValue;
        Currency = command.Currency;
        RateType = command.RateType;
        RateTime = command.RateTime;
        Capitalization = command.Capitalization;
        RateValue = command.RateValue;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        ExpirationDate = command.ExpirationDate;
        Cancelled = command.Cancelled;
        UserId = command.UserId;
    }
    
    public Bill(UpdateBillCommand command)
    {
        Id = command.Id;
        BillValue = command.BillValue;
        Currency = command.Currency;
        RateType = command.RateType;
        RateTime = command.RateTime;
        Capitalization = command.Capitalization;
        RateValue = command.RateValue;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        ExpirationDate = command.ExpirationDate;
        Cancelled = command.Cancelled;
        UserId = command.UserId;
    }
}