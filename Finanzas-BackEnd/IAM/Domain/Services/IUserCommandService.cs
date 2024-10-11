using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.IAM.Domain.Model.Commands;

namespace Finanzas_BackEnd.IAM.Domain.Services;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
    Task<(User user, string token)> Handle(SignInCommand command);
}