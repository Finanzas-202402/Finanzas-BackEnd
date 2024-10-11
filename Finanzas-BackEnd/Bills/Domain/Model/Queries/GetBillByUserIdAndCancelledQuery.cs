namespace Finanzas_BackEnd.Bills.Domain.Model.Queries;

public record GetBillByUserIdAndCancelledQuery(int UserId, bool Cancelled);