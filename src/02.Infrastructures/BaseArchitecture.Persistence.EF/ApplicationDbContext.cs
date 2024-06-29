using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BaseArchitecture.Persistence.EF;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            tracker.AutoDetectChangesEnabled = true;
            tracker.QueryTrackingBehavior =
                QueryTrackingBehavior.TrackAll;
            return tracker;
        }
    }
}