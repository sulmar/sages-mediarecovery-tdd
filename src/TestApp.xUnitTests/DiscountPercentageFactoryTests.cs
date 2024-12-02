using TestApp.TDD;

namespace TestApp.xUnitTests;

public class DiscountPercentageFactoryTests
{
    [Theory]
    [InlineData("SAVE10NOW", 0.1)]
    [InlineData("DISCOUNT20OFF", 0.2)]
    public void Create_ValidDiscountCode_ShouldReturnExpectedPercentage(string discountCode, decimal expectedPercentage)
    {
        // Arrange
        IDiscountPercentageFactory  factory = new DiscountPercentageFactory();
        factory.Add(discountCode, expectedPercentage);
        
        // Act
        var result = factory.Create(discountCode);
        
        // Assert
        Assert.Equal(expectedPercentage, result);
    }
}