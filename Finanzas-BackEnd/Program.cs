using Finanzas_BackEnd.Bills.Application.Internal.CommandServices;
using Finanzas_BackEnd.Bills.Application.Internal.QueryServices;
using Finanzas_BackEnd.Bills.Domain.Repositories;
using Finanzas_BackEnd.Bills.Domain.Services;
using Finanzas_BackEnd.Bills.Infrastructure.Persistence.EFC.Repositories;
using Finanzas_BackEnd.IAM.Application.Internal.CommandServices;
using Finanzas_BackEnd.IAM.Application.Internal.OutboundServices;
using Finanzas_BackEnd.IAM.Application.Internal.QueryServices;
using Finanzas_BackEnd.IAM.Domain.Repositories;
using Finanzas_BackEnd.IAM.Domain.Services;
using Finanzas_BackEnd.IAM.Infrastructure.Hashing.BCrypt.Services;
using Finanzas_BackEnd.IAM.Infrastructure.Persistence.EFC.Repositories;
using Finanzas_BackEnd.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Finanzas_BackEnd.IAM.Infrastructure.Tokens.JWT.Configuration;
using Finanzas_BackEnd.IAM.Infrastructure.Tokens.JWT.Services;
using Finanzas_BackEnd.IAM.Interfaces.ACL;
using Finanzas_BackEnd.IAM.Interfaces.ACL.Services;
using Finanzas_BackEnd.Shared.Domain.Repositories;
using Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Finanzas_BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();    
    });

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Finanzas.API",
                Version = "v1",
                Description = "API RESTful para el proyecto de Finanzas",
                TermsOfService = new Uri("https://grupo-1.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Grupo 1",
                    Email = "u20221d126@upc.edu.pe"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
           In = ParameterLocation.Header,
           Description = "Please enter token",
           Name = "Authorization",
           Type = SecuritySchemeType.Http,
           BearerFormat = "JWT",
           Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer", Type = ReferenceType.SecurityScheme
                    } 
                }, 
                Array.Empty<string>()
            }
        });
    });

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Bills Bounded Context Injection Configuration
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillCommandService, BillCommandService>();
builder.Services.AddScoped<IBillQueryService, BillQueryService>();

// IAM Bounded Context Injection Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

// Add authorization middleware to pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();