namespace TestApp.UnitTests;

[TestClass]
public class RentTests
{
    // Method_Scenario_ExpectedBehavior

    // Method_ExpectedBehavior_Scenario

    private Rent rent;
    
    [TestInitialize]
    public void Init()
    {
        //  Arrange
        rent = new Rent { Rentee = new()};
    }

    [TestMethod]
    // [ExpectedException(typeof(ArgumentNullException))]
    public void CanReturn_WhenUserIsEmpty_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => rent.CanReturn(null);
        
        // Assert
        Assert.ThrowsException<ArgumentNullException>(act);

        // Alternatywny zapis
        Assert.ThrowsException<ArgumentNullException>(()=> rent.CanReturn(null));
    }

    [TestMethod]
    public void CanReturn_WhenUserIsAdmin_ShouldReturnTrue()
    {
        // Act
        var result = rent.CanReturn(new User { IsAdmin = true } );

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanReturn_WhenUserIsRentee_ShouldReturnTrue()
    {
        // Arrange
        User rentee = new User();
        rent.Rentee = rentee;
        
        // Act
        var result = rent.CanReturn(rentee);
        
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanReturn_WhenUserIsNotRentee_ShouldReturnFalse()
    {
        // Act
        var result = rent.CanReturn(new User());
        
        // Assert
        Assert.IsFalse(result);
    }
}