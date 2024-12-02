using System.Runtime.InteropServices;
using TestApp.TDD;

namespace TestApp.xUnitTests;

public class DiscountCalculatorTests
{
    // W przypadku podania pustego kodu rabat nie będzie udzielany.
    
    // Method_Scenario_ExpectedBehavior
    
    private const decimal OriginalPrice = 100.00M;
    private const decimal NegativePrice = -1.00M; 
    private const string InvalidDiscountCode = "a";
    private const string SingleUseDiscountCode = "XYZ";

    private DiscountPercentageCalculator discountCalculator;
    private IDiscountPercentageFactory discountPercentageFactory;
    
    public DiscountCalculatorTests()
    {
        discountPercentageFactory = new DiscountPercentageFactory();
        discountCalculator = new DiscountPercentageCalculator(discountPercentageFactory);
    }

    // 1. W przypadku podania pustego kodu rabat nie będzie udzielany.
    [Fact]
    public void CalculateDiscount_WhenDiscountCodeIsEmpty_ShouldReturnPrice()
    {
        // Act
        var result = discountCalculator.CalculateDiscount(OriginalPrice, string.Empty);
        
        // Assert
        Assert.Equal(100, result);
    }
    
    [Fact]
    public void CalculateDiscount_WhenDiscountCodeIsValid_ShouldReturnDiscountedPriceByExpectedPercent()
    {
        // Arrange
        discountPercentageFactory.Add("XYZ", 0.1M);
        
        // Act
        var result = discountCalculator.CalculateDiscount(OriginalPrice, "XYZ");
        
        // Assert
        Assert.Equal(90M, result);
    }
    
    // 4. Wywołanie metody CalculateDiscount z ujemną ceną powinno rzucić wyjątkiem ArgumentException z komunikatem "Negatives not allowed".

    [Fact]
    public void CalculateDiscount_WhenNegativePrice_ShouldThrowArgumentExceptionWithMessageNegativesNotAllowed()
    {
        // Act
        Action act = () => discountCalculator.CalculateDiscount(NegativePrice, string.Empty);
        
        // Assert
        var result = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Negatives not allowed", result.Message);

    }
    
    // 5. W przypadku błędnego kodu powinien być zwracany wyjątek ArgumentException z komunikatem "Invalid discount code"
    [Fact]
    public void CalculateDiscount_WhenInvalidDiscountCode_ShouldThrowArgumentExceptionWithMessageInvalidDiscountCode()
    {
        // Act
        Action act = () => discountCalculator.CalculateDiscount(OriginalPrice, InvalidDiscountCode);
        
        // Assert
        var result = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Invalid discount code", result.Message);
    }
    
    // 6. Dodaj rabat 50%, który jest naliczany jednorazowo na podstawie kodu z puli kodów.

    [Fact]
    public void CalculateDiscount_WhenFirstUseDiscountCodeInPool_ShouldReturnDiscountedPriceBy50Percent()
    {
        // Arrange
        discountPercentageFactory = new DiscountPercentageFactoryProxy(new DiscountPercentageFactory()); 
        discountCalculator = new DiscountPercentageCalculator(discountPercentageFactory);
        discountPercentageFactory.Add(SingleUseDiscountCode, 0.5M);
        
        // Act
        var result = discountCalculator.CalculateDiscount(OriginalPrice, SingleUseDiscountCode);
        
        // Assert
        Assert.Equal(50.00M, result);
        
    }
    
    [Fact]
    public void CalculateDiscount_WhenNextUseDiscountCodeInPool_ShouldThrowArgumentExceptionWithMessageDiscountCodeHasBeenUsed()
    {
        // Arrange
        discountPercentageFactory = new DiscountPercentageFactoryProxy(new DiscountPercentageFactory());
        discountCalculator = new DiscountPercentageCalculator(discountPercentageFactory);
        discountPercentageFactory.Add(SingleUseDiscountCode, 0.5M);
        discountCalculator.CalculateDiscount(OriginalPrice, SingleUseDiscountCode);
        
        // Act
        Action act = () => discountCalculator.CalculateDiscount(OriginalPrice, SingleUseDiscountCode);  
        
        // Assert
        var result = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Discount code has been used", result.Message);
        
    }
}