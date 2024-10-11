using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions.Extensions;

public static class ModelStateExtensions
{
    public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
    {
        return dictionary
            .SelectMany(m => m.Value!.Errors)
            .Select(m => m.ErrorMessage)
            .ToList();
    }
}