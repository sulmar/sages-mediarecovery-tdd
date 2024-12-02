using TestApp.Mocking;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TestApp.xUnitTests;

public class TrackingServiceTests
{
    #region Fakes
    private class EmptyFileReader : IFileReader
    {
        public string ReadAllText(string path) => string.Empty;
    }

    private class InvalidFileReader : IFileReader
    {
        public string ReadAllText(string path) => "a";
    }

    private class ValidFileReader : IFileReader
    {
        public string ReadAllText(string path) => new Location(51.01d, 21.01d).ToJson();
    }

    #endregion
    
    [Fact]
    public void Get_JsonIsEmpty_ShouldThrowApplicationExceptionWithMessageErrorParsingTheLocation()
    {
        // Arrange
        IFileReader fileReader = new EmptyFileReader();
        TrackingService trackingService = new TrackingService(fileReader);

        // Act
        Action act = () => trackingService.Get();

        // Assert
        var result = Assert.Throws<ApplicationException>(act);
        Assert.Contains("Error parsing the location", result.Message);
    }

    [Fact]
    public void Get_JsonIsInvalid_ShouldThrowApplicationExceptionWithMessageErrorParsingTheLocation()
    {
        // Arrange
        IFileReader fileReader = new InvalidFileReader();
        TrackingService trackingService = new TrackingService(fileReader);

        // Act
        Action act = () => trackingService.Get();

        // Assert
        var result = Assert.Throws<ApplicationException>(act);
        Assert.Contains("Error parsing the location", result.Message);
    }

    [Fact]
    public void Get_JsonIsValid_ShouldReturnLocation()
    {
        // Arrange
        IFileReader fileReader = new ValidFileReader();
        TrackingService trackingService = new TrackingService(fileReader);

        // Act
        var result = trackingService.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(51.01d, result.Latitude);
        Assert.Equal(21.01d, result.Longitude);
    }
}