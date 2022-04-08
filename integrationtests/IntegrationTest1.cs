using Xunit;

namespace integrationtests;

public class IntegrationTest1
{
    [Fact]
    public void Integration_Test1()
    {
        Assert.True("" == "", "Integration_Test1 failed");
    }
}