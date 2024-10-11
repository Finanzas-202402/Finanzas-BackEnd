using Finanzas_BackEnd.IAM.Domain.Model.Commands;
using Finanzas_BackEnd.IAM.Interfaces.REST.Resources;

namespace Finanzas_BackEnd.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Email, resource.Username, resource.Password);
    }
}