using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BaseArchitecture.Persistence.EF;

public class ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options) :
    IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .ApplyConfigurationsFromAssembly(
                GetType().Assembly);
    }

    public override ChangeTracker ChangeTracker
    {
        get
        {
            var tracker = base.ChangeTracker;
            tracker.LazyLoadingEnabled = false;
            tracker.AutoDetectChangesEnabled = false;
            tracker.QueryTrackingBehavior =
                QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            return tracker;
        }
    }
}