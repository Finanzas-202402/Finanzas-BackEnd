using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;

namespace Finanzas_BackEnd.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}