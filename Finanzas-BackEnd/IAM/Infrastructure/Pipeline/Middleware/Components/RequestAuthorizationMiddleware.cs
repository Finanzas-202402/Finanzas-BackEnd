using Finanzas_BackEnd.IAM.Application.Internal.OutboundServices;
using Finanzas_BackEnd.IAM.Domain.Model.Queries;
using Finanzas_BackEnd.IAM.Domain.Services;
using Finanzas_BackEnd.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace Finanzas_BackEnd.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService
    )
    {
        Console.WriteLine("Entering InvokeAsync");

        var endpoint = context.Request.HttpContext.GetEndpoint();
        if (endpoint == null)
        {
            Console.WriteLine("Endpoint is null. Skipping Authorization.");
            await next(context);
            return;
        }

        var allowAnonymous = endpoint.Metadata.Any(m =>
            m.GetType() == typeof(AllowAnonymousAttribute));
        Console.WriteLine($"Allow Anonymous is {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping Authorization");
            await next(context);
            return;
        }

        Console.WriteLine("Checking Authorization");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?
            .Split(" ").Last();
        if (token == null) throw new Exception("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);
        if (userId == null) throw new Exception("Invalid token");

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user == null) throw new Exception("User not found");

        Console.WriteLine("Authorization successful");
        context.Items["User"] = user;
        Console.WriteLine("Continuing with Middleware pipeline");
        await next(context);
    }
}
