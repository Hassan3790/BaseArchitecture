using Xunit;

namespace BaseArchitecture.TestTools.Configurations
{
    [CollectionDefinition("SequentialTests", DisableParallelization = true)]
    public class SequentialTestsCollection : ICollectionFixture<TestConfig>
    {
        
    }
}
