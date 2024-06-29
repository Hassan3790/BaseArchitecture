using Framework.Domain;

namespace BaseArchitecture.Persistence.EF
{
    public class EfUnitOfWork(EFDataContext context) : UnitOfWork
    {
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}
