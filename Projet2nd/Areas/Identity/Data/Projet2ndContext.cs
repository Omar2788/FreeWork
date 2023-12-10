using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet2nd.Areas.Identity.Data;
using Projet2nd.Models;

namespace Projet2nd.Areas.Identity.Data;

public class Projet2ndContext : IdentityDbContext<Projet2ndUser>
{
    public Projet2ndContext(DbContextOptions<Projet2ndContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Commande> Commande { get; set; }

    public virtual DbSet<PurchasedServiceViewModel> PurchasedServiceViewModel { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<Projet2ndUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Projet2ndUser> builder)
    {
        builder.Property(x=>x.Name).IsRequired();

       
    }
}