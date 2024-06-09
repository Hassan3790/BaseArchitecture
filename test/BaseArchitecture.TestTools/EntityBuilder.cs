namespace BaseArchitecture.TestTools
{
    public interface EntityBuilder<T> where T : class
    {
        public T Build();
    }
}
