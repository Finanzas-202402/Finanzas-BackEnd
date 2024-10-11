using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.IAM.Domain.Model.Aggregates;
using Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_BackEnd.Shared.Infrastructure.Interfaces.ASP.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
    public DbSet<Bill> Bills { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Place here your entities configuration

        builder.Entity<Bill>().HasKey(c => c.Id);
        builder.Entity<Bill>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bill>().Property(c => c.BillValue).IsRequired();
        builder.Entity<Bill>().Property(c => c.Currency).IsRequired().HasConversion<string>();;
        builder.Entity<Bill>().Property(c => c.RateType).IsRequired().HasConversion<string>();;
        builder.Entity<Bill>().Property(c => c.RateTime).IsRequired().HasConversion<string>();;
        builder.Entity<Bill>().Property(c => c.Capitalization).IsRequired().HasConversion<string>();;
        builder.Entity<Bill>().Property(c => c.RateValue).IsRequired(); 
        builder.Entity<Bill>().Property(c => c.StartDate).IsRequired();
        builder.Entity<Bill>().Property(c => c.ExpirationDate).IsRequired();
        builder.Entity<Bill>().Property(c => c.Cancelled).IsRequired();
        builder.Entity<Bill>().Property(c => c.UserId).IsRequired();
        
        // Category Relationships
        
        builder.Entity<User>()
            .HasMany(u => u.Bills)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        // IAM Context
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}