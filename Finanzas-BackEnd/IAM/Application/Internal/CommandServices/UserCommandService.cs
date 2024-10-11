using Finanzas_BackEnd.IAM.Application.Internal.OutboundServices;
using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.IAM.Domain.Model.Commands;
using Finanzas_BackEnd.IAM.Domain.Repositories;
using Finanzas_BackEnd.IAM.Domain.Services;
using Finanzas_BackEnd.Shared.Domain.Repositories;

namespace Finanzas_BackEnd.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IHashingService hashingService
) : IUserCommandService
{
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByEmail(command.Email))
            throw new Exception($"Account with email: {command.Email} already exists.");
        
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Email, command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating the user: {e.Message}");
        }
    }

    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.Email);
        
        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }
}