using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.IAM.Interfaces.REST.Resources;

namespace Finanzas_BackEnd.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity, string token)
    {
        return new AuthenticatedUserResource(entity.Id, entity.Username, token);
    }
}