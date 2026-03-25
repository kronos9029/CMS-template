using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VietConstruction.Web.Data.Entities;

namespace VietConstruction.Web.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<CmsPage> Pages => Set<CmsPage>();

    public DbSet<ProjectEntry> Projects => Set<ProjectEntry>();

    public DbSet<PostCategory> Categories => Set<PostCategory>();

    public DbSet<ContentPost> Posts => Set<ContentPost>();

    public DbSet<ContactSubmission> ContactSubmissions => Set<ContactSubmission>();

    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = utcNow;
            }

            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.UpdatedAtUtc = utcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var nullableDateOnlyConverter = new ValueConverter<DateOnly?, DateTime?>(
            value => value.HasValue ? value.Value.ToDateTime(TimeOnly.MinValue) : null,
            value => value.HasValue ? DateOnly.FromDateTime(value.Value) : null);

        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.Id).HasMaxLength(191);
            entity.Property(x => x.UserName).HasMaxLength(191);
            entity.Property(x => x.NormalizedUserName).HasMaxLength(191);
            entity.Property(x => x.Email).HasMaxLength(191);
            entity.Property(x => x.NormalizedEmail).HasMaxLength(191);
            entity.Property(x => x.FullName).HasMaxLength(150).IsRequired();
            entity.Property(x => x.IsActive).HasDefaultValue(true);
        });

        builder.Entity<IdentityRole>(entity =>
        {
            entity.Property(x => x.Id).HasMaxLength(191);
            entity.Property(x => x.Name).HasMaxLength(191);
            entity.Property(x => x.NormalizedName).HasMaxLength(191);
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.Property(x => x.UserId).HasMaxLength(191);
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.Property(x => x.UserId).HasMaxLength(191);
            entity.Property(x => x.RoleId).HasMaxLength(191);
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.Property(x => x.RoleId).HasMaxLength(191);
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.Property(x => x.UserId).HasMaxLength(191);
            entity.Property(x => x.LoginProvider).HasMaxLength(128);
            entity.Property(x => x.ProviderKey).HasMaxLength(128);
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(x => x.UserId).HasMaxLength(191);
            entity.Property(x => x.LoginProvider).HasMaxLength(128);
            entity.Property(x => x.Name).HasMaxLength(128);
        });

        builder.Entity<CmsPage>(entity =>
        {
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Slug).HasMaxLength(180).IsRequired();
            entity.Property(x => x.NavigationTitle).HasMaxLength(120).IsRequired();
            entity.Property(x => x.MetaDescription).HasMaxLength(300).IsRequired();
            entity.Property(x => x.Summary).HasMaxLength(800).IsRequired();
            entity.Property(x => x.HeroTitle).HasMaxLength(220).IsRequired();
            entity.Property(x => x.HeroSummary).HasMaxLength(1000).IsRequired();
            entity.Property(x => x.TemplateKey).HasMaxLength(60).IsRequired();
            entity.Property(x => x.BodyHtml).HasColumnType("longtext");
        });

        builder.Entity<ProjectEntry>(entity =>
        {
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Slug).HasMaxLength(180).IsRequired();
            entity.Property(x => x.Summary).HasMaxLength(800).IsRequired();
            entity.Property(x => x.BodyHtml).HasColumnType("longtext");
            entity.Property(x => x.ClientName).HasMaxLength(180).IsRequired();
            entity.Property(x => x.Location).HasMaxLength(220).IsRequired();
            entity.Property(x => x.Scale).HasMaxLength(220).IsRequired();
            entity.Property(x => x.FeaturedImageUrl).HasMaxLength(400).IsRequired();
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(40);
            entity.Property(x => x.StartedOn).HasConversion(nullableDateOnlyConverter);
            entity.Property(x => x.CompletedOn).HasConversion(nullableDateOnlyConverter);
        });

        builder.Entity<PostCategory>(entity =>
        {
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Slug).HasMaxLength(140).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(400).IsRequired();
            entity.Property(x => x.PostType).HasConversion<string>().HasMaxLength(40);
        });

        builder.Entity<ContentPost>(entity =>
        {
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Title).HasMaxLength(220).IsRequired();
            entity.Property(x => x.Slug).HasMaxLength(180).IsRequired();
            entity.Property(x => x.Summary).HasMaxLength(800).IsRequired();
            entity.Property(x => x.MetaDescription).HasMaxLength(300).IsRequired();
            entity.Property(x => x.FeaturedImageUrl).HasMaxLength(400).IsRequired();
            entity.Property(x => x.BodyHtml).HasColumnType("longtext");
            entity.Property(x => x.PostType).HasConversion<string>().HasMaxLength(40);
            entity.Property(x => x.JobLocation).HasMaxLength(180).IsRequired();
            entity.Property(x => x.ExperienceRequirement).HasMaxLength(180).IsRequired();
            entity.Property(x => x.SalaryRange).HasMaxLength(180).IsRequired();
            entity.Property(x => x.ApplicationDeadline).HasConversion(nullableDateOnlyConverter);
            entity.HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ContactSubmission>(entity =>
        {
            entity.Property(x => x.FullName).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(150).IsRequired();
            entity.Property(x => x.PhoneNumber).HasMaxLength(40).IsRequired();
            entity.Property(x => x.Message).HasColumnType("longtext");
            entity.Property(x => x.ResolutionNote).HasMaxLength(600).IsRequired();
        });

        builder.Entity<SiteSetting>(entity =>
        {
            entity.Property(x => x.CompanyName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Tagline).HasMaxLength(220).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(300).IsRequired();
            entity.Property(x => x.Hotline).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Website).HasMaxLength(150).IsRequired();
            entity.Property(x => x.WorkingHours).HasMaxLength(150).IsRequired();
            entity.Property(x => x.FooterCredentials).HasMaxLength(600).IsRequired();
            entity.Property(x => x.HeroTitle).HasMaxLength(220).IsRequired();
            entity.Property(x => x.HeroSummary).HasMaxLength(1000).IsRequired();
        });
    }
}
