using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.IAM.Domain.Model.Queries;

namespace Finanzas_BackEnd.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByEmailQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
}