namespace Finanzas_BackEnd.IAM.Domain.Model.Commands;

public record SignUpCommand(string Email, string Username, string Password);