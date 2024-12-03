using FluentAssertions;
using TestApp.Fundamentals;

namespace TestApp.xUnitTests;

public class ZplCommandBuilderTests
{
    private ZplCommandBuilder builder;

    private const string BeginLabel = "^XA";
    private const string EndLabel = "^XZ";

    public ZplCommandBuilderTests()
    {
        builder = ZplCommandBuilder.CreateLabel(1, 2);
    }
    
    [Fact]
    public void CreateLabel_ValidSize_ShouldReturnStartLabel()
    {
        // Arrange
        
        // Act
        var builder = ZplCommandBuilder.CreateLabel(1, 2);
        
        // Assert
        string result = builder.Build();
        
        // Assert.Equal("^XA^XZ", result);
         result.Should().Be("^XA^XZ");
    }

    [Fact]
    public void SetText_NotEmpty_ShouldReturnLabelWithText()
    {
        // Act
        builder.SetText("Hello World");
        
        // Assert
        string result = builder.Build();
        
        // Assert.Equal($"^XA^FDHello World^FS^XZ", result);
        // result.Should().Be("^XA^FDHello World^FS^XZ"); // Specific test
        
        result.Should()                          // General test
            .StartWith(BeginLabel)
            .And.Contain("^FDHello World^FS")
            .And.EndWith(EndLabel);
    }


    [Fact]
    public void SetText_Empty_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => builder.SetText(string.Empty);
        
        // Assert
        Assert.Throws<ArgumentNullException>(act);
        
    }

    [Fact]
    public void SetPosition_ValidPosition_ShowReturnLabelWithPosition()
    {
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
        // Act
        Action act = () => builder.SetPosition(width, height);
        
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
        
    }

    [Fact]
    public void SetBarcode_NotEmpty_ShowReturnLabelWithBarcode()
    {
        // Act
        builder.SetBarcode("12345678");
        
        // Assert
        string result = builder.Build();
        
        Assert.Equal("^XA^B3N,N,100,Y,N^FD12345678^FS^XZ", result);
        
    }
    
    [Fact]
    public void SetPositionAndBarcode_NotEmpty_ShowReturnLabelWithBarcode()
    {
        // Act
        builder.SetPosition(1, 2).SetBarcode("12345678");
        
        // Assert
        string result = builder.Build();
        
        Assert.Equal("^XA^FO1,2^B3N,N,100,Y,N^FD12345678^FS^XZ", result);
        
    }
}