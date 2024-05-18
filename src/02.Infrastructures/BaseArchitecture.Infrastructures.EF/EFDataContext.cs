using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BaseArchitecture.Infrastructures.EF;

public class EFDataContext(DbContextOptions<EFDataContext> options) :
    DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;

        modelBuilder
            .ApplyConfigurationsFromAssembly(
                typeof(EFDataContext).Assembly);
    }
    
    public override ChangeTracker ChangeTracker
    {
        get
        {
            var tracker = base.ChangeTracker;
            tracker.LazyLoadingEnabled = false;
            tracker.AutoDetectChangesEnabled = false;
            tracker.QueryTrackingBehavior =
                QueryTrackingBehavior.TrackAll;
            return tracker;
        }
    }
}