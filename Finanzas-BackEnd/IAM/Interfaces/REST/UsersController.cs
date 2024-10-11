using System.Net.Mime;
using Finanzas_BackEnd.IAM.Domain.Model.Queries;
using Finanzas_BackEnd.IAM.Domain.Services;
using Finanzas_BackEnd.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Finanzas_BackEnd.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas_BackEnd.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UsersController(IUserQueryService userQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user is null) return NotFound();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
}