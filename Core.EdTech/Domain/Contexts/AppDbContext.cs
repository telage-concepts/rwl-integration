using Core.EdTech.Domain.Entities;
using Core.EdTech.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.EdTech.Domain.Contexts
{
  public class AppDbContext : IdentityDbContext<ApplicationUser>
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
      {
        property.SetColumnType("decimal(18, 2)");
      }

      foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType.IsEnum))
      {
        property.SetColumnType("nvarchar(50)");
      }

      foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.Name == "CreatedOn"))
      {
        property.SetDefaultValueSql("(UTC_TIMESTAMP)");
      }

    }

    #region Core
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<DiscountWallet> DiscountWallets { get; set; }
    #endregion
  }
}
