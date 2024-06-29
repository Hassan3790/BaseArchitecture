using Framework.Domain;

namespace BaseArchitecture.Persistence.EF
{
    public class EfUnitOfWork(ApplicationDbContext context) : UnitOfWork
    {
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}
