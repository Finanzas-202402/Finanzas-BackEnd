using Finanzas_BackEnd.IAM.Domain.Model.Commands;
using Finanzas_BackEnd.IAM.Interfaces.REST.Resources;

namespace Finanzas_BackEnd.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}