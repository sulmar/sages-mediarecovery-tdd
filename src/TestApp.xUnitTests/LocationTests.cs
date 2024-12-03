using TestApp.Fundamentals;

namespace TestApp.xUnitTests;

public class LocationTests
{
    [Fact]
    public void LocationTest()
    {
        // Arrange
        Location location1 = new Location( 10, 20 );
        Location location2 = new Location( 10, 20);

        var result = location1 == location2;

        Assert.True(result);
    }
}