using TestApp.Fundamentals;

namespace TestApp.xUnitTests;

public class ZplCommandBuilderTests
{
    [Fact]
    public void CreateLabel_ValidSize_ShowReturnStartLabel()
    {
        // Arrange
        
        // Act
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Assert
        string result = builder.Build();
        
        Assert.Equal("^XA^XZ", result);
    }

    [Fact]
    public void SetText_NotEmpty_ShowReturnLabelWithText()
    {
        // Arrange
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Act
        builder.SetText("Hello World");
        
        // Assert
        string result = builder.Build();
        
        Assert.Equal($"^XA^FDHello World^FS^XZ", result);
    }


    [Fact]
    public void SetText_Empty_ShowThrowArgumentNullException()
    {
        // Arrange
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Act
        Action act = () => builder.SetText(string.Empty);
        
        // Assert
        Assert.Throws<ArgumentNullException>(act);
        
    }

    [Fact]
    public void SetPosition_ValidPosition_ShowReturnLabelWithPosition()
    {
        // Arrange
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Act
        builder.SetPosition(1, 2);
        
        // Assert
        string result = builder.Build();
        
        Assert.Equal($"^XA^FO1,2^XZ", result);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(1, 3)]
    [InlineData(2, 3)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)] 
    public void SetPosition_InvalidPosition_ThrowArgumentOutOfRangeException(int width, int height)
    {
        // Arrange
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Act
        Action act = () => builder.SetPosition(width, height);
        
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
        
    }

    [Fact]
    public void SetBarcode_NotEmpty_ShowReturnLabelWithBarcode()
    {
        // Arrange
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Act
        builder.SetBarcode("12345678");
        
        // Assert
        string result = builder.Build();
        
        Assert.Equal("^XA^B3N,N,100,Y,N^FD12345678^FS^XZ", result);
        
    }
    
    [Fact]
    public void SetPositionAndBarcode_NotEmpty_ShowReturnLabelWithBarcode()
    {
        // Arrange
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Act
        builder.SetPosition(1, 2).SetBarcode("12345678");
        
        // Assert
        string result = builder.Build();
        
        Assert.Equal("^XA^FO1,2^B3N,N,100,Y,N^FD12345678^FS^XZ", result);
        
    }
}