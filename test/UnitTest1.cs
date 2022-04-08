using Xunit;

namespace test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.True(1 == 1, "Test1 failed");
    }

    [Fact]
    public void Test2()
    {
        Assert.False(1 == 2, "Test2 failed");
    }
}