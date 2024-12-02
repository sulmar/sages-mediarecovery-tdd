using System.Net.NetworkInformation;
using TestApp.TDD;

namespace TestApp.UnitTests;

[TestClass]
public class DiscountCalculatorTests
{
    // W przypadku podania pustego kodu rabat nie będzie udzielany.
    
    // Method_Scenario_ExpectedBehavior
    
    private const decimal OriginalPrice = 100.00M;
    private const decimal NegativePrice = -1.00M; 
    private const string InvalidDiscountCode = "a";
    private const string SingleUseDiscountCode = "XYZ"; 

    // 1. W przypadku podania pustego kodu rabat nie będzie udzielany.
    [TestMethod]
    public void CalculateDiscount_WhenDiscountCodeIsEmpty_ShouldReturnPrice()
    {
        // Arrange
        DiscountCalculator discountCalculator = new DiscountCalculator();

        // Act
        var result = discountCalculator.CalculateDiscount(OriginalPrice, string.Empty);
        
        // Assert
        Assert.AreEqual(100, result);
    }
    
    // 2. Dodaj rabat 10%, który będzie naliczany po podaniu kodu kuponu SAVE10NOW.
    [TestMethod]
    public void CalculateDiscount_WhenDiscountCodeIsSAVE10NOW_ShouldReturnDiscountedPriceBy10Percent()
    {
        // Arrange
        DiscountCalculator discountCalculator = new DiscountCalculator();
        
        // Act
        var result = discountCalculator.CalculateDiscount(OriginalPrice, "SAVE10NOW");
        
        // Assert
        Assert.AreEqual(90.00M, result);
    }
    
    // 3. Dodaj rabat 20%, który będzie naliczany po podaniu kodu kuponu DISCOUNT20OFF.
    [TestMethod]
    public void CalculateDiscount_WhenDiscountCodeIsDISCOUNT20OFF_ShouldReturnDiscountedPriceBy20Percent()
    {
        // Arrange
        DiscountCalculator discountCalculator = new DiscountCalculator();
        
        // Act
        var result = discountCalculator.CalculateDiscount(OriginalPrice, "DISCOUNT20OFF");
        
        // Assert
        Assert.AreEqual(80.00M, result);
    }
    
    // 4. Wywołanie metody CalculateDiscount z ujemną ceną powinno rzucić wyjątkiem ArgumentException z komunikatem "Negatives not allowed".

    [TestMethod]
    public void CalculateDiscount_WhenNegativePrice_ShouldThrowArgumentExceptionWithMessageNegativesNotAllowed()
    {
        // Arrange
        DiscountCalculator discountCalculator = new DiscountCalculator();
        
        // Act
        Action act = () => discountCalculator.CalculateDiscount(NegativePrice, string.Empty);
        
        // Assert
        var result = Assert.ThrowsException<ArgumentException>(act);
        Assert.AreEqual("Negatives not allowed", result.Message);

    }
    
    // 5. W przypadku błędnego kodu powinien być zwracany wyjątek ArgumentException z komunikatem "Invalid discount code"
    [TestMethod]
    public void CalculateDiscount_WhenInvalidDiscountCode_ShouldThrowArgumentExceptionWithMessageInvalidDiscountCode()
    {
        // Arrange
        DiscountCalculator discountCalculator = new DiscountCalculator();
        
        // Act
        Action act = () => discountCalculator.CalculateDiscount(OriginalPrice, InvalidDiscountCode);
        
        // Assert
        var result = Assert.ThrowsException<ArgumentException>(act);
        Assert.AreEqual("Invalid discount code", result.Message);
    }
    
    // 6. Dodaj rabat 50%, który jest naliczany jednorazowo na podstawie kodu z puli kodów.

    [TestMethod]
    public void CalculateDiscount_WhenFirstUseDiscountCodeInPool_ShouldReturnDiscountedPriceBy50Percent()
    {
        // Arrange
        DiscountCalculator discountCalculator = new DiscountCalculator();
        
        // Act
        var result = discountCalculator.CalculateDiscount(OriginalPrice, SingleUseDiscountCode);
        
        // Assert
        Assert.AreEqual(50.00M, result);
        
    }
    
    [TestMethod]
    public void CalculateDiscount_WhenNextUseDiscountCodeInPool_ShouldThrowArgumentExceptionWithMessageDiscountCodeHasBeenUsed()
    {
        // Arrange
        DiscountCalculator discountCalculator = new DiscountCalculator();
        discountCalculator.CalculateDiscount(OriginalPrice, SingleUseDiscountCode);
        
        // Act
        Action act = () => discountCalculator.CalculateDiscount(OriginalPrice, SingleUseDiscountCode);  
        
        // Assert
        var result = Assert.ThrowsException<ArgumentException>(act);
        Assert.AreEqual("Discount code has been used", result.Message);
        
    }
}