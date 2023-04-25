using AdvancedApi.Domain.Entities;
using AdvancedApi.Domain.Enums;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace AdvancedApi.Infra.Data.Context;

public sealed class AdvancedContext : DbContext
{
    public AdvancedContext(DbContextOptions<AdvancedContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = new Faker<User>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Name, f => f.Person.FullName)
        .RuleFor(u => u.Email, f => f.Person.Email)
        .RuleFor(u => u.Password, f => f.Random.AlphaNumeric(16))
        .RuleFor(u => u.Role, f => f.PickRandom("Admin", "Customer"))
        .RuleFor(u => u.CreatedAt, f => f.Date.Past())
        .RuleFor(u => u.UpdatedAt, f => f.Date.Future())
        .RuleFor(u => u.Status, f => f.PickRandom<EStatus>());

        modelBuilder.Entity<User>()
            .HasData(user.GenerateBetween(1000,1000));
    }
}