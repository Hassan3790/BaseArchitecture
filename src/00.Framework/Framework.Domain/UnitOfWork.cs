namespace Framework.Domain
{
    public interface UnitOfWork
    {
        Task Complete();
    }
}
