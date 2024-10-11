using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.IAM.Domain.Model.Queries;
using Finanzas_BackEnd.IAM.Domain.Repositories;
using Finanzas_BackEnd.IAM.Domain.Services;

namespace Finanzas_BackEnd.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    public async Task<User?> Handle(GetUserByEmailQuery query)
    {
        return await userRepository.FindByEmailAsync(query.Email);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }
}